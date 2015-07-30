using System;
using System.Collections.Generic;
using Dev.Properties;

namespace Dev.ServiceContainer
{
    /// <summary>
    ///     This class provides the ambient container for this application. If your
    ///     framework defines such an ambient container, use ServiceLocator.Current
    ///     to get it.
    /// </summary>
    public static class ServiceLocator
    {
        #region ServiceLocator

        private static ServiceLocatorProvider currentProvider;

        /// <summary>
        ///     The current ambient container.
        /// </summary>
        private static IServiceLocator Current
        {
            get
            {
                if (!IsLocationProviderSet)
                    throw new InvalidOperationException(Resources.ServiceLocationProviderNotSetMessage);

                return currentProvider();
            }
        }

        private static bool IsLocationProviderSet
        {
            get { return currentProvider != null; }
        }

        /// <summary>
        ///     Set the delegate that is used to retrieve the current container.
        /// </summary>
        /// <param name="newProvider">
        ///     Delegate that, when called, will return
        ///     the current ambient container.
        /// </param>
        public static void SetLocatorProvider(ServiceLocatorProvider newProvider)
        {
            currentProvider = newProvider;
        }

        #endregion

        #region Method

        public static object Get(Type type)
        {
            return Current.GetInstance(type);
        }

        public static object Get(Type type, string key)
        {
            return Current.GetInstance(type, key);
        }

        public static object Get<T>()
        {
            return Current.GetInstance<T>();
        }

        public static object Get<T>(string key)
        {
            return Current.GetInstance<T>(key);
        }

        public static IEnumerable<object> GetAll(Type type)
        {
            return Current.GetAllInstances(type);
        }

        public static IEnumerable<T> GetAll<T>()
        {
            return Current.GetAllInstances<T>();
        }

        #endregion
    }
}