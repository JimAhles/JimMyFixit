using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFixIt.Logging;
using MyFixIt.Persistence;
using System.Threading.Tasks;
using MyFixit.FixitTaskEntity;
namespace MyFixIt.Persistence.Tests
{
    [TestClass]
    public class FixitTaskRepositoryTest
    {
        private string testuser = "Test1";
               
        /// <summary>
        /// the only way i can get async to run FROM the test harness method is by using Task.Run. I've seen some posts that
        /// claim to have other ways of doing it, but they are usually confusing Nunit's async harness. From what i can see,
        /// the next version of c# (version 6) will (of course) address this. So, for now, i know this works, but its a bit clumsy
        /// When the called target method fails in a catch, the catch runs on a different thread. By using 'await all the way down' we
        /// can capture the fact that an exception was thrown, but the exception error message can't be accessed as the thread has 
        /// closed. So, a full test will have to eventually include a read to the log file. 
        /// NOTE: Best practices for logging have the log being written at the level the error occured on, with the handling of the 
        /// event at the highest level
        /// NOTE: Looks like the test web app is not handling throws from async methods.
        /// </summary>
        [TestCategory("Database"), TestMethod]
       
        public void FindTaskByOwnerTest()
        {
            Task.Run(async () =>
            {
                var x = await doit();
                if (x == true)
                    Assert.IsTrue(true);
                else
                    Assert.Fail();
            }).GetAwaiter().GetResult();
             
        }

        private async Task<bool> doit()
        { 
            try
            {
                // Arrange
                var logger = new Logger();
                var ftr = new FixItTaskRepository(logger);
                
                // Act
                //var res = await GetRep(ftr);
                var res = await ftr.FindOpenTasksByOwnerAsync(testuser);
                // Assert
                // as we currently have no way of gauranteeing that this user will be in the db, just pass it if it doesn't blow up!
                // we would need to add some type of database builup and teardown, or use some type of mocking tool
                return true;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                return false;
            }

        }

        
    }
}
