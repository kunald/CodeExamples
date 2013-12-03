using System;

namespace Insight123.Contract
{
    public interface IRepository<T> where T : IEvent
    {
        void Save<TAggregate>(TAggregate aggregateRoot, int expectedVersion)
            where TAggregate : class, IEventProvider<T>, new();
        TAggregate GetById<TAggregate>(Guid id)
            where TAggregate : class, IEventProvider<T>, new();
    }
}
