using System;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Ioc;

namespace SmartSchool.Testes.Compartilhado
{
    public abstract class BaseMediatorServiceProvider
    {
        protected ServiceProvider GetServiceProviderComMediatR(params (Type tipo, object instancia)[] servicesInjection)
        {
            var services = new ServiceCollection();

            foreach (var (tipo, instancia) in servicesInjection)
            {
                services.AddSingleton(tipo, instancia);
            }

            services.AddMyMediatR();

            return services.BuildServiceProvider();
        }
    }
}
