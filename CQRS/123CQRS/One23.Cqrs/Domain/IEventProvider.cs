using Insight.Cqrs.Events;
using System.Collections.Generic;

namespace Insight.Cqrs.Domain
{
    public interface IEventProvider
    {
        void LoadsFromHistory(IEnumerable<Event> history);
        IEnumerable<Event> GetUncommittedChanges();
    }
}
