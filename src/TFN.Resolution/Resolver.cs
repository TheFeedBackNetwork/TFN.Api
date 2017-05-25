﻿using System;
using System.Net;
using System.Net.Mail;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TFN.Api.Authorization.Handlers;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Services;
using TFN.Domain.Services.Credits;
using TFN.Domain.Services.Cryptography;
using TFN.Domain.Services.IdentityServer.Services;
using TFN.Domain.Services.IdentityServer.Validators;
using TFN.Domain.Services.Posts;
using TFN.Domain.Services.TransientUsers;
using TFN.Domain.Services.UserAccounts;
using TFN.Infrastructure.Components;
using TFN.Infrastructure.Components.Storage;
using TFN.Infrastructure.Interfaces.Components;
using TFN.Infrastructure.Repositories.ApplicationClientAggregate.InMemory;
using TFN.Infrastructure.Repositories.CommentAggregate.InMemory;
using TFN.Infrastructure.Repositories.CreditAggregate.InMemory;
using TFN.Infrastructure.Repositories.PostAggregate.InMemory;
using TFN.Infrastructure.Repositories.TrackAggregate.InMemory;
using TFN.Infrastructure.Repositories.TransientUserAggregate.InMemory;
using TFN.Infrastructure.Repositories.UserAccountAggregate.InMemory;
using TFN.Infrastructure.Modules.Email;
using TFN.Infrastructure.Repositories.ResourceAggregate.InMemory;
using TFN.Mvc.Extensions;
using IBlobStorageComponent = TFN.Domain.Interfaces.Components.IBlobStorageComponent;
using IS3StorageComponent = TFN.Domain.Interfaces.Components.IS3StorageComponent;

namespace TFN.Resolution
{
    public static class Resolver
    {
        public static void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
        {
            
        }

        public static void RegisterTypes(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IResourceRepository, ResourceInMemoryRepository>();
            services.AddTransient<IResourceStore, ResourceInMemoryRepository>();
            services.AddTransient<IUserRepository, UserInMemoryRepository>();
            services.AddTransient<ITransientUserRepository, TransientUserInMemoryRepository>();
            services.AddTransient<IClientRepository, ClientInMemoryRepository>();
            services.AddTransient<IPostRepository, PostInMemoryRepository>();
            services.AddTransient<ICommentRepository, CommentInMemoryRepository>();
            services.AddTransient<IClientStore, ClientInMemoryRepository>();
            services.AddTransient<ITrackRepository, TrackInMemoryRepository>();
            services.AddTransient<ICreditRepository, CreditInMemoryRepository>();
            //services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICorsPolicyService, CorsPolicyService>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IPasswordService, PasswordService>();
            services.AddTransient<ITrackProcessingService, TrackProcessingService>();
            services.AddTransient<ITrackStorageService, TrackStorageService>();
            services.AddTransient<IKeyService, KeyService>();
            services.AddTransient<ITransientUserService, TransientUserService>();
            services.AddTransient<ICreditService, CreditService>();
            services.AddTransient<IPostService, PostService>();

            //validators
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            //components
            services.AddTransient<IS3StorageComponent, S3StorageComponent>();
            services.AddTransient<IBlobStorageComponent, BlobStorageComponent>();
            services.AddScoped<IQueryCursorComponent, QueryCursorComponent>();

            //email
            services.AddEmailService<EmailService>(options =>
            {
                options.Sender = new MailAddress(configuration["Messaging:Email:sender"]);
                options.Credentials = new NetworkCredential(configuration["Messaging:Email:Username"], configuration["Messaging:Email:Password"]);
                options.SmtpHost = configuration["Messaging:Email:SmtpHost"];
                options.SmtpPort = Convert.ToInt32(configuration["Messaging:Email:SmtpPort"]);
            });

            services.AddAccountEmailService<AccountEmailService>(options =>
            {
                options.KeyBaseUrl = configuration["Messaging:KeyBaseUrl"];
            });

            services.AddSingleton<IConfiguration>(configuration);
        }


        public static void RegisterAuthorizationPolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options
                    .AddScopePolicy("posts.read")
                    .AddScopePolicy("posts.write")
                    .AddScopePolicy("posts.edit")
                    .AddScopePolicy("posts.delete")
                    .AddScopePolicy("tracks.read")
                    .AddScopePolicy("tracks.write")
                    .AddScopePolicy("tracks.delete")
                    .AddScopePolicy("credits.write")
                    .AddScopePolicy("credits.delete")
                    .AddScopePolicy("credits.read")
                    .AddScopePolicy("users.read")
                    .AddScopePolicy("ip.read");
            });

            services.AddTransient<IAuthorizationHandler, PostAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, CommentAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, ScoreAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, CreditsAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, TrackAuthorizationHandler>();
        }
    }
}