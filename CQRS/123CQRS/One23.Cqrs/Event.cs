using System;
using Insight123.Contract;

namespace Insight123.Base
{
    [Serializable]
    public class Event : IEvent
    {
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
    }
}
