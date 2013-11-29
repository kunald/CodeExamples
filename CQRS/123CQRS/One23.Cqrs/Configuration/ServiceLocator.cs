using insight.Cqrs.Messaging;
using Insight.Cqrs.Handlers;
using Insight.Cqrs.ReadOnlyStorage;
using Insight.Cqrs.Storage;
using StructureMap;

namespace Insight.Cqrs.Configuration
{
    public sealed class ServiceLocator
    {
        private static readonly ICommandBus _commandBus;
        private static readonly IReadRepository _readModel;
        private static readonly bool IsInitialized;
        private static readonly object LockThis = new object();

        static ServiceLocator()
        {
            if (IsInitialized) return;
            lock (LockThis)
            {
                ContainerBootstrapper.BootstrapStructureMap();
                _commandBus = ObjectFactory.GetInstance<ICommandBus>();
                _readModel = ObjectFactory.GetInstance<IReadRepository>();
                IsInitialized = true;
            }
        }


        public static ICommandBus CommandBus
        {
            get { return _commandBus; }
        }

        public static IReadRepository ReportDatabase
        {
            get { return _readModel; }
        }
    }


    static class ContainerBootstrapper
    {
        public static void BootstrapStructureMap()
        {

            ObjectFactory.Initialize(x =>
            {
                x.For(typeof(IRepository<>)).Singleton().Use(typeof(Repository<>));
                x.For<IEventStorage>().Singleton().Use<InMemoryEventStorage>();
                x.For<IEventBus>().Use<EventBus>();
                x.For<ICommandHandlerFactory>().Use<StructureMapCommandHandlerFactory>();
                x.For<IEventHandlerFactory>().Use<StructureMapEventHandlerFactory>();
                x.For<ICommandBus>().Use<CommandBus>();
                x.For<IEventBus>().Use<EventBus>();
                x.For<IReadRepository>().Use<PartReadRepo>();
            });
        }
    }
}
