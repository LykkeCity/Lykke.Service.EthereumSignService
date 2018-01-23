﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Log;
using Lykke.Service.EthereumSignService.Core.Services;
using Lykke.Service.EthereumSignService.Core.Settings.ServiceSettings;
using Lykke.Service.EthereumSignService.Services;
using Lykke.SettingsReader;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.EthereumSignService.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<EthereumSignServiceSettings> _settings;
        private readonly ILog _log;
        // NOTE: you can remove it if you don't need to use IServiceCollection extensions to register service specific dependencies
        private readonly IServiceCollection _services;

        public ServiceModule(IReloadingManager<EthereumSignServiceSettings> settings, ILog log)
        {
            _settings = settings;
            _log = log;

            _services = new ServiceCollection();
        }

        protected override void Load(ContainerBuilder builder)
        {
            // TODO: Do not register entire settings in container, pass necessary settings to services which requires them
            // ex:
            //  builder.RegisterType<QuotesPublisher>()
            //      .As<IQuotesPublisher>()
            //      .WithParameter(TypedParameter.From(_settings.CurrentValue.QuotesPublication))

            builder.RegisterInstance(_log)
                .As<ILog>()
                .SingleInstance();

            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>();

            // TODO: Add your dependencies here
            builder.RegisterType<Services.SignService>()
                .As<ISignService>().SingleInstance();

            builder.RegisterType<ValidationService>()
                .As<IValidationService>().SingleInstance();

            builder.RegisterType<WalletCreationService>()
                .As<IWalletCreationService>().SingleInstance();

            builder.Populate(_services);
        }
    }
}