using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MassTransit;
using MassTransit.Log4NetIntegration;
using Messages;
using log4net.Config;

namespace ConsoleSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
           
        
            Console.WriteLine("set up a subscriber");

            XmlConfigurator.Configure();

            Bus.Initialize(config =>
            {
                config.UseMsmq();
                config.VerifyMsmqConfiguration();
                config.UseMulticastSubscriptionClient();
                config.UseControlBus();
               
                config.ReceiveFrom("msmq://localhost/test_end");
                //config.Subscribe(x=>x.Consumer<Consumer>());
                config.Subscribe(s => s.Instance(new Consumer()).Permanent());
                config.UseLog4Net();
                //config.Subscribe(subs =>
                //{
                //    subs.Handler<ProductChangeMessage>(msg => Console.WriteLine("fff"));
                //    //subs.Handler<string>(msg => Console.WriteLine(msg));
                //    //Console.Read();
                //});
            });

            //var bus = ServiceBusFactory.New(sbc =>
            //                                    {
            //                                        sbc.UseMsmq();
            //                                        sbc.UseMulticastSubscriptionClient();
            //                                        sbc.ReceiveFrom("msmq://localhost/test_end");
            //                                        sbc.UseLog4Net();
            //                                        sbc.Subscribe(x=>x.Handler<ProductChangeMessage>(msg=>Console.WriteLine(msg)));
            //                                    });
           
            Bus.Instance.WriteIntrospectionToConsole();
            //Bus.Instance.WriteIntrospectionToConsole();
           
            Console.Read();
        }

     
    }

    public class Consumer : Consumes<ProductChangeMessage>.All
    {
        public void Consume(ProductChangeMessage message)
        {
            Console.WriteLine("start consuming message");

            if (string.IsNullOrEmpty(message.Ids))
            {
                Console.WriteLine("update all products");

            }
            else
            {
                Console.WriteLine("update products for " + message.Ids);
            }
        }
    }
}
