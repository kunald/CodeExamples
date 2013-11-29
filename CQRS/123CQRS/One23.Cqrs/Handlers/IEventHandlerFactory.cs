using Insight.Cqrs.EventHandlers;
using Insight.Cqrs.Events;
using System.Collections.Generic;

namespace Insight.Cqrs.Handlers
{
    public interface IEventHandlerFactory
    {
        IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : Event;
    }
}
