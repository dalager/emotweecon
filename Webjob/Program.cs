using System.Collections;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Queue.Protocol;

namespace EmoTweeCon.Webjob
{
    // To learn more about Microsoft Azure WebJobs, please see http://go.microsoft.com/fwlink/?LinkID=401557
    public class Program
    {
        public static void Main()
        {
            var jobhostconfig = new JobHostConfiguration();

            #region jobhostconfig

            jobhostconfig.Queues.BatchSize = 3;
            // hvor mange messages skal behandles parallelt, pr job (1-32, default 16)
            jobhostconfig.Queues.MaxDequeueCount = 1; // antal retries 
            //  jobhostconfig.Queues.MaxPollingInterval = TimeSpan.FromSeconds(3);
            // hvor lang tid skal der gå mellem polls mod en tom kø (2s-1min, default 1 min)



            jobhostconfig.ServiceBusConnectionString =
                CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

            #endregion

            var host = new JobHost(jobhostconfig);
            host.RunAndBlock();
        }
    }
}