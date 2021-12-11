using MHA.Data.Context;
using MHA.Data.Contracts;
using MHA.Tools.DataProtect;
using MHA.Tools.Jwt;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.Reflection;

namespace MHA.API.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void RegisterDI(this IServiceCollection services)
        {
            services.AddScoped<IClientInstancer, ClientInstancer>();
            services.AddScoped<IDataProtect, DataProtect>();
            services.AddScoped<IJwtHandler, JwtHandler>();

            var assemblyData = AppDomain.CurrentDomain.Load("MHA.Data");
            var assemblyCore = AppDomain.CurrentDomain.Load("MHA.Core");
            var assemblyCoreContracts = AppDomain.CurrentDomain.Load("MHA.Core.Contracts");

            services.RegisterAssemblyPublicNonGenericClasses(new Assembly[] { assemblyData, assemblyCore, assemblyCoreContracts })
                    .Where(c => c.Name.EndsWith("Command"))
                    .AsPublicImplementedInterfaces();

            services.RegisterAssemblyPublicNonGenericClasses(new Assembly[] { assemblyData, assemblyCore, assemblyCoreContracts })
                    .Where(c => c.Name.EndsWith("Invoker"))
                    .AsPublicImplementedInterfaces();

            services.RegisterAssemblyPublicNonGenericClasses(new Assembly[] { assemblyData, assemblyCore, assemblyCoreContracts })
                    .Where(c => c.Name.EndsWith("Service"))
                    .AsPublicImplementedInterfaces();
        }
    }
}
