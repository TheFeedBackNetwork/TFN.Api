using System;
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
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.Entities.IdentityServer;
using TFN.Domain.Services.Credits;
using TFN.Domain.Services.Cryptography;
using TFN.Domain.Services.IdentityServer.Services;
using TFN.Domain.Services.IdentityServer.Stores;
using TFN.Domain.Services.IdentityServer.Validators;
using TFN.Domain.Services.Posts;
using TFN.Domain.Services.TransientUsers;
using TFN.Domain.Services.UserAccounts;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Document;
using TFN.Infrastructure.Components;
using TFN.Infrastructure.Components.Storage;
using TFN.Infrastructure.Interfaces.Components;
using TFN.Infrastructure.Modules.Email;
using TFN.Infrastructure.Repositories.ApplicationClientAggregate.Document;
using TFN.Infrastructure.Repositories.CommentAggregate.Document;
using TFN.Infrastructure.Repositories.CreditsAggregate.Document;
using TFN.Infrastructure.Repositories.LikeAggregate.Document;
using TFN.Infrastructure.Repositories.ListenAggregate.Document;
using TFN.Infrastructure.Repositories.PostAggregate.Document;
using TFN.Infrastructure.Repositories.ProductApiResourceAggregate.Document;
using TFN.Infrastructure.Repositories.ScoreAggregate.Document;
using TFN.Infrastructure.Repositories.TrackAggregate.Document;
using TFN.Infrastructure.Repositories.TransientUserAccountAggregate.Document;
using TFN.Infrastructure.Repositories.UserAccountAggregate.Document;
using TFN.Infrastructure.Repositories.UserIdentityResourceAggregate.Document;
using TFN.Mvc.Extensions;

namespace TFN.Resolution
{
    public static class Resolver
    {
        public static void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDocumentDbContext<DocumentContext>(options =>
            {
                options.DatabaseName = configuration["DocumentDb:DatabaseName"];
                options.DatabaseKey = configuration["DocumentDb:Key"];
                options.DatabaseUri = new Uri(configuration["DocumentDb:EndpointUri"]);
            });
        }

        public static void RegisterTypes(IServiceCollection services, IConfiguration configuration)
        {
            //aggregate mappers
            services.AddTransient<IAggregateMapper<ApplicationClient, ApplicationClientDocumentModel, Guid>, ApplicationClientDocumentMapper>();
            services.AddTransient<IAggregateMapper<Comment, CommentDocumentModel, Guid>,CommentDocumentMapper>();
            services.AddTransient<IAggregateMapper<Credits, CreditsDocumentModel, Guid>,CreditsDocumentMapper>();
            services.AddTransient<IAggregateMapper<Like, LikeDocumentModel, Guid>,LikeDocumentMapper>();
            services.AddTransient<IAggregateMapper<Listen, ListenDocumentModel, Guid>,ListenDocumentMapper>();
            services.AddTransient<IAggregateMapper<Post, PostDocumentModel, Guid>,PostDocumentMapper>();
            services.AddTransient<IAggregateMapper<ProductApiResource, ProductApiResourceDocumentModel, Guid>,ProductApiResourceDocumentMapper>();
            services.AddTransient<IAggregateMapper<Score, ScoreDocumentModel, Guid>,ScoreDocumentMapper>();
            services.AddTransient<IAggregateMapper<Track, TrackDocumentModel, Guid>,TrackDocumentMapper>();
            services.AddTransient<IAggregateMapper<TransientUserAccount, TransientUserAccountDocumentModel, Guid>,TransientUserAccountDocumentMapper>();
            services.AddTransient<IAggregateMapper<UserAccount, UserAccountDocumentModel, Guid>, UserAccountDocumentMapper>();
            services.AddTransient<IAggregateMapper<UserIdentityResource, UserIdentityResourceDocumentModel, Guid>,UserIdentityResourceDocumentMapper>();

            //repositories
            services.AddTransient<IApplicationClientRepository, ApplicationClientDocumentRepository>();
            services.AddTransient<ICommentRepository, CommentDocumentRepository>();
            services.AddTransient<ICreditRepository, CreditsDocumentRepository>();
            services.AddTransient<ILikeRepository, LikeDocumentRepository>();
            services.AddTransient<IListenRepository, ListenDocumentRepository>();
            services.AddTransient<IPostRepository, PostDocumentRepository>();
            services.AddTransient<IProductApiResourceRepository, ProductApiResourceDocumentRepository>();
            services.AddTransient<IScoreRepository, ScoreDocumentRepository>();
            services.AddTransient<ITrackRepository, TrackDocumentRepository>();
            services.AddTransient<ITransientUserAccountRepository, TransientUserAccountDocumentRepository>();
            services.AddTransient<IUserAccountRepository, UserAccountDocumentRepository>();
            services.AddTransient<IUserIdentityResourceRepository, UserIdentityResourceDocumentRepository>();

            //services
            services.AddTransient<IClientStore, ClientStore>();
            services.AddTransient<IResourceStore, ResourceStore>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICorsPolicyService, CorsPolicyService>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IPasswordService, PasswordService>();
            services.AddTransient<IKeyService, KeyService>();
            services.AddTransient<ITransientUserService, TransientUserService>();
            services.AddTransient<ICreditService, CreditService>();
            services.AddTransient<IPostService, PostService>();

            //validators
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

            //components
            services.AddTransient<ITrackProcessingComponent, TrackProcessingComponent>();
            services.AddTransient<ITrackStorageComponent, TrackStorageComponent>();
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
                    .AddScopePolicy("credits.edit")
                    .AddScopePolicy("credits.delete")
                    .AddScopePolicy("credits.read")
                    .AddScopePolicy("users.read")
                    .AddScopePolicy("ip.read");
            });

            services.AddTransient<IAuthorizationHandler, PostAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, CommentAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, ScoreAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, LikeAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, CreditsAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, TrackAuthorizationHandler>();
        }
    }
}