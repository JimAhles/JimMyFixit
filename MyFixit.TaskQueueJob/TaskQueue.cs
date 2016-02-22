using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFixIt.Logging;
using MyFixIt.Persistence;
using MyFixit.FixitTaskEntity;
using System.Net;
using System.Threading;
using Autofac;

namespace MyFixit.TaskQueueJob
{
    public class TaskQueue
    {
        private IContainer container;
        private ILogger logger;
        CancellationTokenSource tokenSource = new CancellationTokenSource();

        public TaskQueue(IContainer container)
        {
            this.container = container;
            
        }

        
        public async Task RunAsync()
        {
            using (var scope = container.BeginLifetimeScope())
            {
                logger = container.Resolve<ILogger>();
                logger.Information("web job called from function");

                try
                {
                    await RunAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "error in web job.");
                }
            }
        }

        

        public void RunAsync(CancellationToken token, FixItTask fixit)
        {
            using (var scope = container.BeginLifetimeScope())
            {
                IFixItQueueManager queueManager = scope.Resolve<IFixItQueueManager>();
                logger = container.Resolve<ILogger>();
                try
                {
                    queueManager.ProcessMessagesAsync(token,fixit);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Exception in worker role Run loop.");
                }
            }
        }

        /// <summary>
        /// setting dependencies in web job program.cs as logger is needed in functions. Not sure which is best.
        /// </summary>
        /// <returns></returns>
        public bool OnStart()
        {
            bool ret = false;
            try
            {
                // Set the maximum number of concurrent connections 
                ServicePointManager.DefaultConnectionLimit = 12;

                var builder = new ContainerBuilder();
                builder.RegisterType<Logger>().As<ILogger>().SingleInstance();
                builder.RegisterType<FixItTaskRepository>().As<IFixItTaskRepository>();
                builder.RegisterType<FixItQueueManager>().As<IFixItQueueManager>();
                container = builder.Build();

                logger = container.Resolve<ILogger>();
                ret = true;
            }
            catch(Exception ex)
            {

            }
            finally
            {
                
            }
            return ret;
        }

        public  void OnStop()
        {
            tokenSource.Cancel();
            tokenSource.Token.WaitHandle.WaitOne();
        }
    }
}
