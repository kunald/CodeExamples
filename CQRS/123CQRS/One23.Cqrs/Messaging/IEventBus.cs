using Insight.Cqrs.Events;
namespace insight.Cqrs.Messaging
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : Event;
    }


    //var bus = new EventBus();

    //        var storage = new EventStore(eventBus);
    //        var rep = new Repository<InventoryItem>(storage);
    //        var commands = new InventoryCommandHandlers(rep);
}
