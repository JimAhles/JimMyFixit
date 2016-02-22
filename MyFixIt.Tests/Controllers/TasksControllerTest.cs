using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using Autofac.Util;
using Autofac.Integration.Mvc;
using MyFixIt.Logging;
using MyFixIt.Persistence;
using MyFixIt.Controllers;

namespace MyFixIt.Tests.Controllers
{

    

    [TestClass]
    public class TasksControllerTest
    {
        private IContainer GetAutoFacContainer()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();
            builder.RegisterType<FixItTaskRepository>().As<IFixItTaskRepository>();
            builder.RegisterType<PhotoService>().As<IPhotoService>().SingleInstance();
            builder.RegisterType<FixItQueueManager>().As<IFixItQueueManager>();

            return builder.Build();
            
        }


        /// <summary>
        /// simple test with autofac generated objects for controller class create
        /// There are two create methods, the second one uses the aspnet.identity object User, need to find a way to mock that
        /// </summary>
        [TestCategory("Controller"), TestMethod]
        public void CreateTest()
        {
            try
            {
                // Arrange
                var container = GetAutoFacContainer();
                using (var scope = container.BeginLifetimeScope())
                {
                    var tc = new TasksController(scope.Resolve<IFixItTaskRepository>(), scope.Resolve<IPhotoService>(), scope.Resolve<IFixItQueueManager>(),new Logger());

                    // Act
                    var ac = tc.Create();

                    Assert.IsTrue(ac != null);
                }

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
