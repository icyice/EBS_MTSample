using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MassTransit;
using MassTransit.Log4NetIntegration;
using Messages;
using log4net.Config;

namespace ConsolePublisher
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            
            Console.WriteLine("start publishing");
            XmlConfigurator.Configure();
            //initialize bus 
            Bus.Initialize(config =>
            {
                config.UseMsmq();
                config.VerifyMsmqConfiguration();
                config.UseMulticastSubscriptionClient();
                config.UseControlBus();
             
                config.ReceiveFrom("msmq://localhost/test_queue");
                config.UseLog4Net();

            });
            //Bus.Instance.WriteIntrospectionToConsole();
           
            //publish updating all products message
            var msg1 = new ProductChangeMessage();

            Bus.Instance.Publish(new ProductChangeMessage());
            Console.WriteLine("Message sent 1");

            //publish updating some products message
            var msg2 = new ProductChangeMessage() { Ids ="1,2,3,4" };
            Bus.Instance.Publish(msg2);
            Console.WriteLine("Message sent 2");


           
            Console.Read();

           
        }
    }
}
