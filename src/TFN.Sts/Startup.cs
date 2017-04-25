using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using IdentityServer4.Configuration;
using IdentityServer4.Validation;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TFN.Infrastructure.Modules.Logging;
using TFN.Mvc.Constants;
using TFN.Mvc.Extensions;
using TFN.Resolution;
using TFN.Sts.UI;

namespace TFN.Sts
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        private IHostingEnvironment HostingEnvironment { get; set; }
        private X509Certificate2 PrimarySigningCredentials { get; set; }
        private X509Certificate2 SecondarySigningCredentials { get; set; }

        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsLocal())
            {
                builder.AddUserSecrets("tfn-local");

                loggerFactory.AddConsole(LogLevel.Trace);
                loggerFactory.AddDebug(LogLevel.Trace);
            }

            if (!env.IsLocal())
            {
                loggerFactory.AddAppendBlob(
                    Configuration["Logging:StorageAccountConnectionString"],
                    LogLevel.Information);

                loggerFactory.AddEmail(
                    Configuration["Logging:Email:SupportEmail"],
                    Configuration["Logging:Email:Username"],
                    Configuration["Logging:Email:Username"],
                    Configuration["Configuration:Email:Password"],
                    Configuration["Logging:Email:Host"],
                    Convert.ToInt32(Configuration["Logging:Email:Port"]),
                     env.EnvironmentName,
                     LogLevel.Error);
            }

            Configuration = builder.Build();
            HostingEnvironment = env;

            var logger = loggerFactory.CreateLogger<Startup>();
            logger.LogInformation("The Feedback Network Sts application configuration is starting.");
        }



        public void ConfigureServices(IServiceCollection services)
        {
            Resolver.RegisterDbContext(services, Configuration);
            Resolver.RegisterTypes(services, Configuration);
            Resolver.RegisterAuthorizationPolicies(services);

            if (HostingEnvironment.IsLocal())
            {
                PrimarySigningCredentials = new X509Certificate2(Path.Combine(HostingEnvironment.ContentRootPath, "TFNCertificate.pfx"), Configuration["CertificateThumbprints:CertificateKey"]);
                SecondarySigningCredentials = PrimarySigningCredentials;
            }
            else if (HostingEnvironment.IsDevelopment())
            {
                var certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                certStore.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                var certCollection = certStore.Certificates.Find(
                    X509FindType.FindBySubjectName,
                    Configuration["Certificate:Name"],
                    false);


                if (certCollection.Count > 1)
                {
                    var certList = certCollection.Cast<X509Certificate2>().ToList();

                    certList.Sort((x, y) => -1 * x.NotAfter.CompareTo(y.NotAfter));

                    PrimarySigningCredentials = certList.ElementAt(0);
                    SecondarySigningCredentials = certList.ElementAt(1);

                }
                else
                {
                    throw new InvalidOperationException($"Could not retrieve signing credentials {certCollection.Count}");
                }
                certStore.Dispose();
            }

            services
                .AddIdentityServer(options =>
                {
                    options.UserInteraction = new UserInteractionOptions
                    {
                        LoginUrl = $"/{RoutePaths.SignInUrl}",
                        LogoutUrl = $"/{RoutePaths.SignOutUrl}",
                        ErrorUrl = $"/{RoutePaths.ErrorUrl}"
                    };

                    options.Events = new EventsOptions
                    {
                        RaiseSuccessEvents = true,
                        RaiseErrorEvents = true,
                        RaiseFailureEvents = true,
                        RaiseInformationEvents = true
                    };
                })
                .AddSigningCredential(PrimarySigningCredentials)
                .AddValidationKeys(new X509SecurityKey(SecondarySigningCredentials));

            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                })
                .AddRazorOptions(razor =>
                {
                    razor.ViewLocationExpanders.Add(new ViewLocationExpander());
                });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", corsBuilder =>
                {
                    corsBuilder.AllowAnyHeader();
                    corsBuilder.AllowAnyMethod();
                    corsBuilder.AllowAnyOrigin();
                    corsBuilder.AllowCredentials();
                });
            });

            if (!HostingEnvironment.IsLocal())
            {
                TelemetryConfiguration.Active.DisableTelemetry = false;
                services.AddApplicationInsightsTelemetry(options =>
                {
                    options.EnableAdaptiveSampling = false;
                    options.InstrumentationKey = Configuration["ApplicationInsights:TfnStsInstrumentKey"];
                    options.DeveloperMode = HostingEnvironment.IsDevelopment() || HostingEnvironment.IsLocal();
                    options.EnableDebugLogger = true;

                });

            }
            else
            {
                TelemetryConfiguration.Active.DisableTelemetry = true;
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<Startup>();
            logger.LogInformation($"Signing Certificate \n Expiration: {PrimarySigningCredentials.NotAfter} \n Thumbprint: {PrimarySigningCredentials.Thumbprint} \n SignatureAlgorithm : {PrimarySigningCredentials.SignatureAlgorithm}");
            logger.LogInformation($"Verification Key \n Expiration: {SecondarySigningCredentials.NotAfter} \n Thumbprint: {SecondarySigningCredentials.Thumbprint} \n SignatureAlgorithm : {SecondarySigningCredentials.SignatureAlgorithm}");

            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            logger.LogInformation("Tfn.Sts startup is complete.");
        }
    }
}
