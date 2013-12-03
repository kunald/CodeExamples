namespace Insight123.Contract
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : IEvent;
    }


    //var bus = new EventBus();

    //        var storage = new EventStore(eventBus);
    //        var rep = new Repository<InventoryItem>(storage);
    //        var commands = new InventoryCommandHandlers(rep);
}
