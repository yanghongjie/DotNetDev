using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Dev.ServiceContainer;

namespace Test.Common.ServiceProviderImpl
{
    /// <summary>
    /// Autofac implementation of the Microsoft CommonServiceLocator.
    /// </summary>
    public class AutofacServiceLocator : ServiceLocatorImplBase
    {
        private readonly IComponentContext container;

        public AutofacServiceLocator(IComponentContext container)
        {
            this.container = container;
        }
        
        protected override object DoGetInstance(Type serviceType, string key)
        {
            return key != null ? container.ResolveNamed(key, serviceType) : container.Resolve(serviceType);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            object instance = container.Resolve(enumerableType);
            return ((IEnumerable)instance).Cast<object>();
        }
    }
}