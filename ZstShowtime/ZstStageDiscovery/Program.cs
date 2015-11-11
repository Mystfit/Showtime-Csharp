using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Zeroconf;
using System.Net;

namespace ZstStageDiscovery
{
    class Program
    {
        static void Main(string[] args)
        {
            bool foundStage = false;
            string stageAddress = "";
            int stagePort = -1;
            ServiceBrowser browser = new ServiceBrowser();
            browser.ServiceAdded += delegate (object o, ServiceBrowseEventArgs eventArgs)
            {
                eventArgs.Service.Resolved += delegate (object o1, ServiceResolvedEventArgs resolvedArgs)
                {
                    if(resolvedArgs.Service.HostEntry.AddressList.Length > 0)
                    {
                        stageAddress = resolvedArgs.Service.HostEntry.AddressList[0].ToString();
                        stagePort = resolvedArgs.Service.Port;
                        Console.WriteLine("Address is: {0}:{1}", stageAddress, stagePort);
                        foundStage = true;
                    }
                };
                eventArgs.Service.Resolve();
                Console.WriteLine("Found Service: {0}", eventArgs.Service.Name);
            };

            browser.Browse("_zeromq._tcp", "local");
            int retries = 3;
            while (retries-- > 0 || !foundStage)
            {
                System.Threading.Thread.Sleep(1000);
                System.Console.WriteLine("Searching...");
            }
            System.Console.WriteLine("Done!");
        }
    }
}
