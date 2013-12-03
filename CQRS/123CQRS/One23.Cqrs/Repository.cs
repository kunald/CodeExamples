using System;
using System.Linq;
using System.Collections.Generic;
using Insight123.Contract;
using Insight123.Base.Exceptions;

namespace Insight123.Base
{
    public class Repository<TEvent> : IRepository<TEvent> where TEvent : IEvent
    {
        private readonly IEventStorage<TEvent> _storage;
        private static object _lockStorage = new object();

        public Repository(IEventStorage<TEvent> storage)
        {
            _storage = storage;
        }

        public void Save<TAggregate>(TAggregate aggregateRoot, int expectedVersion) where TAggregate : class, IEventProvider<TEvent>, new()
        {
            if (aggregateRoot.GetUncommittedChanges().Any())
            {
                lock (_lockStorage)
                {
                    if (expectedVersion != -1)
                    {
                        var item = GetById<TAggregate>(aggregateRoot.Id);
                        if (item.Version != expectedVersion)
                        {
                            throw new ConcurrencyException(string.Format("Aggregate {0} has been previously modified",
                                                                         item.Id));
                        }
                    }

                    _storage.Save(aggregateRoot);
                }
            }
        }

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IEventProvider<TEvent>, new()
        {
            IEnumerable<TEvent> events = _storage.GetEvents(id);
            var obj = new TAggregate();
            obj.LoadsFromHistory(events);
            return obj;
        }
    }
}
