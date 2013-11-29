using System;

namespace Insight.Cqrs.Events
{
    public interface IEvent
    {
        Guid Id { get; }
    }
}
