using System.Net;
using EventStore.ClientAPI;
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
        //private static IPAddress ip = new IPAddress(new byte[] { 127, 0, 0, 1 });
        //private static readonly IPEndPoint IntegrationTestTcpEndPoint = new IPEndPoint(ip, 1113);
        public static void BootstrapStructureMap()
        {

            ObjectFactory.Initialize(x =>
                {
                    //x.For<IEventStoreConnection>().Use(() => EventStoreConnection.Create(IntegrationTestTcpEndPoint));
                    x.For<IDomainRepository>().Singleton().Use<DomainRepository>();
                    //x.For<IEventStorage>().Singleton().Use<InsightEventStore>();
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
