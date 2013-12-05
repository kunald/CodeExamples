using System;
using System.Collections.Generic;

namespace Insight123.Contract
{
    public interface IEventProvider
    {
        Guid Id { get; }
        int Version { get; }
        void LoadsFromHistory(IEnumerable<IEvent> history);
        IEnumerable<IEvent> GetUncommittedChanges();
    }
}
