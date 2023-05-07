using Application.Interfaces;
using Application.Services;
using CleanArch.Application.Interfaces.Auths;
using CleanArch.Application.Services.Auths;
using Domain.Interfaces;
using Infra.Data.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Ioc
{
    public static class ApplicationDependencycontainer
    {
        public static void RegisterServices(IServiceCollection services)
        {

            services.AddScoped<IElevatorCallStepService, ElevatorCallStepService>();
            services.AddScoped<IElevatorService, ElevatorService>();
            services.AddScoped<IFloorService, FloorService>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
        }

    }
}
