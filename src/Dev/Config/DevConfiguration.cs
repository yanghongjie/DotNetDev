using System;
using Dev.Bus;
using Dev.ServiceContainer;

namespace Dev.Config
{
    public abstract class DevConfiguration 
    {
        protected DevConfiguration()
        {

        }

        protected abstract IServiceLocator CreateServiceLocator();
        public ICommandBus CommandBus
        {
            get { return (ICommandBus)CreateServiceLocator().GetInstance(typeof(ICommandBus)); }
        }
        public IEventBus EventBus
        {
            get { return (IEventBus)CreateServiceLocator().GetInstance(typeof(IEventBus)); }
        }
    }
}