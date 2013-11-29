using Insight.Cqrs.Commands;
namespace insight.Cqrs.Messaging
{
    public interface ICommandBus
    {
        void Send<T>(T command) where T : Command;
    }
}
