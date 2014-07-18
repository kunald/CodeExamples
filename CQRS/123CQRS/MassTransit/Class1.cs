using MassTransit.Services.Routing.Configuration;
using System;

namespace MassTransit
{
    public class Class1
    {
        public Class1()
        {

            Bus.Initialize(sbc =>
            {
                sbc.UseMsmq();
                sbc.VerifyMsmqConfiguration();
                sbc.UseMulticastSubscriptionClient();
                sbc.ReceiveFrom("msmq://localhost/insighttest");
                sbc.ConfigureService<RoutingConfigurator>(
                    BusServiceLayer.Session,
                    rc => rc.Route<YourMessage>().To("msmq://localhost/insighttest")
                    );
            });
            Bus.Instance.Probe();
            Bus.Instance.WriteIntrospectionToConsole();
            String read;
            while (!String.IsNullOrEmpty(read = Console.ReadLine()))
            {
                Bus.Instance.Publish(new YourMessage { Text = read });
            }
        }

    }

    public class YourMessage
    {
        public string Text { get; set; }
    }
}
