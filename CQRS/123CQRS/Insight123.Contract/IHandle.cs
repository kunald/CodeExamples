namespace Insight123.Contract
{
    public interface IHandle<TEvent> where TEvent : IEvent
    {
        void Handle(TEvent e);
    }
}
