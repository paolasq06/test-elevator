using CleanArch.Infra.Data.Context;
using CleanArchitecture.Application.Common.Interfaces;
using Domain.Models.Elevator;
using Masiv.Providers.Api;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masiv.Elevator.Application.Integration.Test
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;
        private static string _currentUserId;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTest()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();
            _configuration = builder.Build();

            var services = new ServiceCollection();
            var startup = new Startup(_configuration);
            startup.ConfigureServices(services);
            services.AddLogging();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "Elevator.Api"));
            services.AddTransient(provider =>
            Mock.Of<ICurrentUserService>(s => s.UserId == _currentUserId));
            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" },
                WithReseed = true
            };
            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();


            EnsureDatabase();
            await SeedData();

        }

        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDBContext>();

            context.Database.Migrate();
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDBContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public static async Task SeedData()
        {
            //await AddAsync(new Domain.Models.Elevator.Elevator
            //{
            //    Name = "Main elevator",
            //    Status = true,
            //    Speed = 1,
            //    DoorStatus = 0,
            //    CurrentFloor = null
            //});


            //await AddAsync(new Floor
            //{
            //    Name = "Main elevator",
            //    Status = true,
            //    ElevatorId = 1,
            //    Number = 1
            //}); 
        }

        public static async Task ResetState()
        {
            
            await _checkpoint.Reset(_configuration.GetConnectionString("AplicationDBContextDev"));
            _currentUserId = null;
        }

    }
}
