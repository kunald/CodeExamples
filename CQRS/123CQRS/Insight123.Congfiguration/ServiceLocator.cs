using Insight123.Base;
using Insight123.Base.Handlers;
using Insight123.Contract;
using Insight123.Reporting;
using StructureMap;

namespace Insight123.Configuration
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
                x.For(typeof(IEventStorage<>)).Singleton().Use(typeof(InMemoryEventStorage<>));
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
