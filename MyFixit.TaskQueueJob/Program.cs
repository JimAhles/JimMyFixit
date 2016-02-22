using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Autofac;
using MyFixIt.Logging;
using MyFixIt.Persistence;

namespace MyFixit.TaskQueueJob
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    public class Program
    {
        public static IContainer Container { get; set; }

        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            JobHost host;
            Container = GetAutoFacContainer();
            using (var scope = Program.Container.BeginLifetimeScope())
            {
                var logger = scope.Resolve<ILogger>();
                logger.Information("in web job main");
                var config = new JobHostConfiguration();
                config.Queues.MaxPollingInterval = TimeSpan.FromSeconds(30);
                config.Queues.MaxDequeueCount = 3;
                host = new JobHost();
                logger.Information("testtest");
                // The following code ensures that the WebJob will be running continuously
                host.RunAndBlock();
            }
        }

        private static IContainer GetAutoFacContainer()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<LoggerLog4Net>().As<ILogger>().SingleInstance();
            builder.RegisterType<FixItTaskRepository>().As<IFixItTaskRepository>();
            builder.RegisterType<FixItQueueManager>().As<IFixItQueueManager>();

            return builder.Build();

        }
    }
}
