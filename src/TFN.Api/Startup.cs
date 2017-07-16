using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TFN.Api.Filters.ActionFilters;
using TFN.Api.Models.Factories;
using TFN.Api.Models.Interfaces;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Caching.Aggregate;
using TFN.Infrastructure.Architecture.Caching.Base;
using TFN.Infrastructure.Architecture.Repositories.Document;
using TFN.Infrastructure.Interfaces.Modules;
using TFN.Infrastructure.Modules.Logging;
using TFN.Infrastructure.Repositories.CreditsAggregate.Document;
using TFN.Infrastructure.Repositories.UserAccountAggregate.Document;
using TFN.Mvc.Extensions;
using TFN.Resolution;

namespace TFN.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; set; }

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

                loggerFactory
                    .AddConsole()
                    .AddDebug();
            }

            if (!env.IsLocal())
            {
                loggerFactory.AddAzureWebAppDiagnostics();

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
            logger.LogInformation("TFN.Api application is starting.");
        }

        
        public void ConfigureServices(IServiceCollection services)
        {
            Resolver.RegisterDbContext(services, Configuration);
            Resolver.RegisterTypes(services, Configuration);
            Resolver.RegisterAuthorizationPolicies(services);

            services.AddTransient<ICommentResponseModelFactory, CommentResponseModelFactory>();
            services.AddTransient<ICommentSummaryResponseModelFactory, CommentSummaryResponseModelFactory>();
            services.AddTransient<ICreditsResponseModelFactory, CreditsResponseModelFactory>();
            services.AddTransient<IPostResponseModelFactory, PostResponseModelFactory>();
            services.AddTransient<IPostSummaryResponseModelFactory, PostSummaryResponseModelFactory>();
            services.AddTransient<IResourceAuthorizationResponseModelFactory, ResourceAuthorizationResponseModelFactory>();
            services.AddTransient<ITrackResponseModelFactory, TrackResponseModelFactory>();
            services.AddTransient<IUsersResponseModelFactory, UsersResponseModelFactory>();

            services
                .AddOptions()
                .Configure<RedisSettings>(Configuration.GetSection("Redis"));

            services.AddSingleton<IAggregateCache<Comment>, AggregateCache<Comment>>();
            services.AddSingleton<IAggregateCache<Credits>, AggregateCache<Credits>>();
            services.AddSingleton<IAggregateCache<Like>, AggregateCache<Like>>();
            services.AddSingleton<IAggregateCache<Listen>, AggregateCache<Listen>>();
            services.AddSingleton<IAggregateCache<Post>, AggregateCache<Post>>();
            services.AddSingleton<IAggregateCache<Track>, AggregateCache<Track>>();
            services.AddSingleton<IAggregateCache<Score>, AggregateCache<Score>>();


            services.AddMvc()
                .AddMvcOptions(options =>
                {
                    options.Filters.Add(typeof(ValidateModelFilterAttribute));
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

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
                    options.InstrumentationKey = Configuration["ApplicationInsights:TfnApiInstrumentKey"];
                    options.DeveloperMode = HostingEnvironment.IsDevelopment() || HostingEnvironment.IsLocal();
                    options.EnableDebugLogger = true;

                });

            }
            else
            {
                TelemetryConfiguration.Active.DisableTelemetry = true;
            }

        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceScopeFactory scopeFactory)
        {
            app.UseCors("CorsPolicy");

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                Authority = Configuration["Authorization:Authority"],
                Audience = Configuration["Authorization:Audience"],
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                RequireHttpsMetadata = false,

                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.PreferredUserName,
                    RoleClaimType = JwtClaimTypes.Role,
                }
            });

            app.Map("/api", api =>
            {
                api.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller}/{action}");
                });
            });
            

            var logger = loggerFactory.CreateLogger<Startup>();
            logger.LogInformation("Tfn.Api startup is complete.");
        }
    }
}
