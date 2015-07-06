using System;
using System.Collections.Generic;

namespace Dev.ServiceContainer
{
    /// <summary>
    /// The generic Service Locator interface. This interface is used
    /// to retrieve services (instances identified by type and optional
    /// name) from a container.
    /// project address：http://commonservicelocator.codeplex.com/
    /// </summary>
    public interface IServiceLocator : IServiceProvider
    {
        object GetInstance(Type serviceType);
        object GetInstance(Type serviceType, string key);
        IEnumerable<object> GetAllInstances(Type serviceType);
        TService GetInstance<TService>();
        TService GetInstance<TService>(string key);
        IEnumerable<TService> GetAllInstances<TService>();
    }
}