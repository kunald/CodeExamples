using Insight.Cqrs.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insight.Cqrs.EventHandlers
{
    public interface IEventHandler<TEvent> where TEvent : Event
    {
        void Handle(TEvent handle);
    }
}
