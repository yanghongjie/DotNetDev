using System;
using System.Collections.Generic;
using Dev.ServiceContainer;
using Ninject;

namespace Test.Common.ServiceProviderImpl
{
    /// <summary>
    /// Ninject implementation of the Microsoft CommonServiceLocator.
    /// </summary>
    public class NinjectServiceLocator : ServiceLocatorImplBase
    {
        private readonly IKernel kernel;

        public NinjectServiceLocator(IKernel kernel)
        {
            this.kernel = kernel;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return key == null ? kernel.Get(serviceType) : kernel.Get(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}