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
                //sbc.VerifyMsmqConfiguration();
                //sbc.UseMulticastSubscriptionClient();
                sbc.ReceiveFrom("msmq://localhost/insight_queue");
                sbc.Subscribe(subs => subs.Handler<YourMessage>(msg => Console.WriteLine(msg.Text)));
            });
            Bus.Instance.Probe();
            Bus.Instance.WriteIntrospectionToConsole();
            Bus.Instance.Publish(new YourMessage { Text = "Hi" });

        }

    }

    public class YourMessage
    {
        public string Text { get; set; }
    }
}
