﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Tweetbook.Data;
using Tweetbook.Filters;
using Tweetbook.Options;
using Tweetbook.Services;

namespace Tweetbook.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);


            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DataContext>();

            services.AddScoped<IIdentityService, IdentityService>();


            services.AddMvc(options =>
                options.Filters.Add<ValidationFilter>()
                )
                .AddFluentValidation(mvcConfiguration => mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>()) //AbstractValidator içeren herşeyi bulur
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
            services.AddAuthentication(q =>
            {
                q.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                q.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                q.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }
        ).AddJwtBearer(x =>
        {
            x.SaveToken = true;
            x.TokenValidationParameters = tokenValidationParameters;
        });

            services.AddSingleton(tokenValidationParameters);





            services.AddScoped<IPostServices, PostService>();
            //services.AddSingleton<IPostServices, CosmosPostService>();
        }
    }
}
