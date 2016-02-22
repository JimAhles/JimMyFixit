using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage.Blob;
using MyFixit.FixitTaskEntity;
using MyFixIt.Logging;
using Autofac;

namespace MyFixit.TaskQueueJob
{
    public class Functions
    {
        private static ILogger logger;
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage(
                    [QueueTrigger("fixits")]  FixItTask blobInfo,
       
        [Blob("testblob/fixitlog")]TextWriter log, 
        CancellationToken token)
        {
            log.WriteLine("at the beginning of processqueuemessage");
            using (var scope = Program.Container.BeginLifetimeScope())
            {
                logger = scope.Resolve<ILogger>();
                try
                {
                    TaskQueue tq = new TaskQueue(Program.Container);
                    tq.RunAsync(token, blobInfo);
                    logger.Information("everthing is ok.");
                    log.WriteLine("asdfasdf");
                }
                catch (Exception ex)
                {
                    log.WriteLine(ex.Message);
                    logger.Error(ex, "", null);
                }
            }
        }
    }
}
