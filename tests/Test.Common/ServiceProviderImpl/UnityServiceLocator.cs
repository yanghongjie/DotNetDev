using System;
using System.Collections.Generic;
using Dev.ServiceContainer;
using Microsoft.Practices.Unity;

namespace Test.Common.ServiceProviderImpl
{
    /// <summary>
    /// Unity implementation of the Microsoft CommonServiceLocator.
    /// </summary>
    public class UnityServiceLocator : ServiceLocatorImplBase
    {
        private readonly IUnityContainer container;

        public UnityServiceLocator(IUnityContainer container)
        {
            this.container = container;
            container.RegisterInstance<IServiceLocator>(this, new ExternallyControlledLifetimeManager());
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return container.Resolve(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return container.ResolveAll(serviceType);
        }
    }
}