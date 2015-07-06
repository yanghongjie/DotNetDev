using Autofac;
using Dev.ServiceContainer;
using Microsoft.Practices.Unity;
using Ninject;
using Test.Common.ServiceProviderImpl;
using Test.Dev.Components;
using UnityServiceLocator = Test.Common.ServiceProviderImpl.UnityServiceLocator;

namespace Test.Dev.Config
{
    public static class ContainerConfig
    {
        public static void UnityConfig()
        {
            var container = new UnityContainer()
                .RegisterType<ILogger, AdvancedLogger>()
                //.RegisterType<ILogger, SimpleLogger>(typeof(SimpleLogger).FullName)
                .RegisterType<ILogger, AdvancedLogger>(typeof(AdvancedLogger).FullName);

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
        }

        public static void AutoFacConfig()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AdvancedLogger>().As<ILogger>();
            //builder.RegisterType<SimpleLogger>().As<ILogger>();
            var container = builder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
        }

        public static void NinjectConfig()
        {
            var container = new StandardKernel();
            container.Bind<ILogger>().To<AdvancedLogger>();
            //container.Bind<ILogger>().To<SimpleLogger>();

            ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(container));
        }
    }
}