using Masiv.Api.Configurations;
using CleanArch.Infra.Data.Context;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Core.Filter;
using FluentValidation.AspNetCore;
using CleanArchitecture.WebUI.Filters;
using System.IO;
using Microsoft.Extensions.FileProviders;
using CleanArch.Infra.Ioc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.WebUI.Services;
using Microsoft.OpenApi.Models;
using System;
using WebApi.Middleware;
using Application.Courses.Commands;


namespace Masiv.Providers.Api
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


            services.AddControllers(options => { 
            
            }
             // handle exceptions thrown by an action
           );


            //services.AddStackExchangeRedisCache(option =>
            //{
            //    option.Configuration = "localhost:6379";
            //});

            services.AddDbContext<ApplicationDBContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("AplicationDBContextDev"));
                options.EnableSensitiveDataLogging(true);
            });


            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            //add before the MVC
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]))
                    
                };
            });
            services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(Application.Auth.Commands.PostLoginCommand).GetTypeInfo().Assembly);

            services.AddCors();
            services.AddSwaggerGen();
            services.RegisterAutoMapper();

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                        RequestPath = "/Resources",
                        EnableDefaultFiles = true
            });
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrEmpty(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Masiv.Elevator");
                c.RoutePrefix = string.Empty;
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);

            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
                options.Filters.Add<ApiExceptionFilterAttribute>();

            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<PostLoginCommandValidator>();
            });



        }
    }

}
