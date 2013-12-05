using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using EventStore.ClientAPI;
using Insight123.Contract;
using Insight123.Base.Exceptions;

namespace Insight123.Base
{
    public class DomainRepository : IDomainRepository
    {
        private readonly IEventStorage _storage;
        private static object _lockStorage = new object();
        private IEventStoreConnection _connection;
        private static IPAddress ip = new IPAddress(new byte[] { 127, 0, 0, 1 });
        private static readonly IPEndPoint IntegrationTestTcpEndPoint = new IPEndPoint(ip, 1113);

        //public DomainRepository(IEventStorage storage)
        //{
        //    _connection.Connect();
        //    //new InsightEventStore(_connection);
        //    _storage = storage;
        //}

        public DomainRepository(IEventBus eventBus)
        {
            _connection = EventStoreConnection.Create(IntegrationTestTcpEndPoint);
            _connection.Connect();
            _storage = new InsightEventStore(_connection, eventBus);
        }

        public void Save<TAggregate>(TAggregate aggregateRoot, int expectedVersion) where TAggregate : class,IEventProvider, new()
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
            };
        }

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : class,IEventProvider, new()
        {
            IEnumerable<IEvent> events = _storage.GetAllEvents<TAggregate>(id);
            var obj = new TAggregate();
            obj.LoadsFromHistory(events);
            return obj; ;
        }
    }
}
