using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestControlApi.Auth;
using GuestControlApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GuestControlApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            MongoDbContext.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
            MongoDbContext.DatabaseName = Configuration.GetSection("MongoConnection:Database").Value;
            MongoDbContext.IsSSL = Convert.ToBoolean(this.Configuration.GetSection("MongoConnection:IsSSL").Value);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.GetSection("TokenConfigurations:Issuer").Value,
                        ValidAudience = Configuration.GetSection("TokenConfigurations:Audience").Value,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = JwtModels.Create(Configuration.GetSection("TokenConfigurations:Secret").Value)
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .AllowAnyOrigin());

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
