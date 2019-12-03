using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notifications.Models;
using Notifications.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationTests
{
    [TestClass]
    public class MessageStoreClientTest
    {
        [TestMethod]
        public void TestGetMentionsSummary()
        {
            MessageStoreClient cli = new MessageStoreClient();
            string uid = "mwj7";
            Task<List<Message>> task = cli.GetMentionsSummary(uid);
            Task.WaitAll(task);

            List<Message> messages = task.Result;

            Assert.AreEqual(1, messages.Count);
            Assert.AreEqual(2, messages[0].Id); 
        }
    }
}

