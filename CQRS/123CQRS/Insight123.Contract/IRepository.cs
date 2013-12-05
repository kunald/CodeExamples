using System;

namespace Insight123.Contract
{
    public interface IDomainRepository
    {
        void Save<TAggregate>(TAggregate aggregateRoot, int expectedVersion)
            where TAggregate : class,IEventProvider, new();
        TAggregate GetById<TAggregate>(Guid id)
            where TAggregate : class,IEventProvider, new();
    }
}
