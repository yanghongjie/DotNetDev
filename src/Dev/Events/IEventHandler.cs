using Dev.Domain;
using Dev.Bus;
using Dev.Bus.Attributes;

namespace Dev.Events
{
    /// <summary>
    /// 事件处理器接口
    /// </summary>
    /// <typeparam name="TEvent">事件.</typeparam>
    public interface IEventHandler<in TEvent> : IHandler<TEvent>
         where TEvent : class, IEvent
    {
    }
}