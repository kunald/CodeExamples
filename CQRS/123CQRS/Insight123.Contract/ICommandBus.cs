namespace Insight123.Contract
{
    public interface ICommandBus
    {
        void Send<T>(T command) where T : ICommand;
    }
}
