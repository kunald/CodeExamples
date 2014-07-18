using System;

namespace Insight123.Contract
{
    public interface IEvent
    {
        Guid AggregateId { get; set; }
        int Version { get; set; }
    }
}
