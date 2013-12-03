using System;
using System.Collections.Generic;

namespace Insight123.Contract
{
    public interface IEventProvider<TEvent>
    {
        Guid Id { get; }
        int Version { get; }
        void LoadsFromHistory(IEnumerable<TEvent> history);
        IEnumerable<TEvent> GetUncommittedChanges();
    }
}
