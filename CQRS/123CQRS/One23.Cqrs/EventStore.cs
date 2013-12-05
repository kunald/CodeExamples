using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Insight123.Base.Exceptions;
using Insight123.Contract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Insight123.Base
{
    public class InsightEventStore : IEventStorage
    {
        IEventStoreConnection _eventStoreConnection { get; set; }
        public Func<Type, Guid, string> _aggregateIdToStreamName { get; set; }
        private const int WritePageSize = 500;
        private const int ReadPageSize = 500;
        private const string EventClrTypeHeader = "EventClrTypeName";
        private static readonly JsonSerializerSettings SerializerSettings;
        private readonly IEventBus _eventBus;

        static InsightEventStore()
        {
            SerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };
        }

        public InsightEventStore(IEventStoreConnection eventStoreConnection, IEventBus eventBus)
            : this(eventStoreConnection, (t, g) => string.Format("{0}-{1}", char.ToLower(t.Name[0]) + t.Name.Substring(1), g.ToString("N")))
        {
            _eventBus = eventBus;
        }

        public InsightEventStore(IEventStoreConnection eventStoreConnection, Func<Type, Guid, string> aggregateIdToStreamName)
        {
            _eventStoreConnection = eventStoreConnection;
            _aggregateIdToStreamName = aggregateIdToStreamName;
        }

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : class,IEventProvider, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEvent> GetAllEvents<TAggregate>(Guid aggregateId) where TAggregate : class,IEventProvider, new()
        {
            return GetEventsFromVersion<TAggregate>(aggregateId, int.MaxValue);
        }

        public IEnumerable<IEvent> GetEventsFromVersion<TAggregate>(Guid aggregateId, int version) where TAggregate : class,IEventProvider, new()
        {
            if (version <= 0)
                throw new InvalidOperationException("Cannot get version <= 0");

            var streamName = _aggregateIdToStreamName(typeof(TAggregate), aggregateId);
            var aggregate = ConstructAggregate<TAggregate>();

            var sliceStart = 0;
            var sliceCount = sliceStart + ReadPageSize <= version
                                ? ReadPageSize
                                : version - sliceStart + 1;

            StreamEventsSlice currentSlice = _eventStoreConnection.ReadStreamEventsForward(streamName, sliceStart, sliceCount, false);

            if (currentSlice.Status == SliceReadStatus.StreamNotFound)
                throw new AggregateNotFoundException("Aggregate not found");

            if (currentSlice.Status == SliceReadStatus.StreamDeleted)
                throw new AggregateDeletedException("Aggregate has been deleted");
            List<IEvent> events = new List<IEvent>();
            sliceStart = currentSlice.NextEventNumber;
            do
            {
                foreach (var evnt in currentSlice.Events)
                {
                    events.Add(DeserializeEvent(evnt.OriginalEvent.Metadata, evnt.OriginalEvent.Data) as IEvent);
                }
            } while (version >= currentSlice.NextEventNumber && !currentSlice.IsEndOfStream);

            //if (aggregate.Version != version && version < Int32.MaxValue)
            //    throw new AggregateVersionException(id, typeof(TAggregate), aggregate.Version, version);

            return events;
        }

        public void Save(IEventProvider eventProvider)
        {

            var commitHeaders = new Dictionary<string, object>
            {
                {"CommitId", eventProvider.Id},
                {"AggregateClrTypeName", eventProvider.GetType().AssemblyQualifiedName}
            };

            var streamName = _aggregateIdToStreamName(eventProvider.GetType(), eventProvider.Id);
            var newEvents = eventProvider.GetUncommittedChanges().Cast<object>().ToList();
            var originalVersion = eventProvider.Version - newEvents.Count;
            var expectedVersion = originalVersion == 0 ? ExpectedVersion.NoStream : originalVersion;
            var eventsToSave = newEvents.Select(e => ToEventData(Guid.NewGuid(), e, commitHeaders)).ToList();

            if (eventsToSave.Count < WritePageSize)
            {
                _eventStoreConnection.AppendToStream(streamName, eventProvider.Version, eventsToSave);

            }
            else
            {
                var transaction = _eventStoreConnection.StartTransaction(streamName, expectedVersion);

                var position = 0;
                while (position < eventsToSave.Count)
                {
                    var pageEvents = eventsToSave.Skip(position).Take(WritePageSize);
                    transaction.Write(pageEvents);
                    position += WritePageSize;
                }

                transaction.Commit();
            }
            foreach (var @event in newEvents)
            {
                var desEvent = ChangeTo(@event, @event.GetType());
                _eventBus.Publish(desEvent);
            }
            //eventProvider.ClearUncommittedEvents();
        }

        public static dynamic ChangeTo(dynamic source, Type dest)
        {

            return System.Convert.ChangeType(source, dest);
        }

        private static TAggregate ConstructAggregate<TAggregate>()
        {
            return (TAggregate)Activator.CreateInstance(typeof(TAggregate), true);
        }

        private static EventData ToEventData(Guid eventId, object evnt, IDictionary<string, object> headers)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evnt, SerializerSettings));

            var eventHeaders = new Dictionary<string, object>(headers)
            {
                {
                    EventClrTypeHeader, evnt.GetType().AssemblyQualifiedName
                }
            };
            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventHeaders, SerializerSettings));
            var typeName = evnt.GetType().Name;

            return new EventData(eventId, typeName, true, data, metadata);
        }

        private static object DeserializeEvent(byte[] metadata, byte[] data)
        {
            var eventClrTypeName = JObject.Parse(Encoding.UTF8.GetString(metadata)).Property(EventClrTypeHeader).Value;
            return JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), Type.GetType((string)eventClrTypeName));
        }
    }
}
