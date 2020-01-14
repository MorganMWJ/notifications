using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notifications.Models;
using Notifications.Services;
using System;
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
            //MessageStoreClient cli = new MessageStoreClient();
            //string uid = "mwj7";
            //Task<List<Message>> task = cli.GetMentionsSummary(uid);
            //Task.WaitAll(task);

            //List<Message> messages = task.Result;

            //Assert.AreEqual(1, messages.Count);
            //Assert.AreEqual(2, messages[0].Id); 
        }

       
        private List<Message> GetMockSummaryDONTUSE(string uid)
        {
            List<Message> mockMessagesToReturn = new List<Message>();
            mockMessagesToReturn.Add(new Message() { Id = 1, Body = "First message body", IsDeleted = false, TimeCreated = DateTime.Now, TimeEdited = DateTime.Now, OwnerUid = uid });
            mockMessagesToReturn.Add(new Message() { Id = 2, Body = "Second message body", IsDeleted = false, TimeCreated = DateTime.Now, TimeEdited = DateTime.Now, OwnerUid = uid });
            mockMessagesToReturn.Add(new Message() { Id = 3, Body = "Third message body", IsDeleted = true, TimeCreated = DateTime.Now, TimeEdited = DateTime.Now, OwnerUid = uid });
            mockMessagesToReturn.Add(new Message() { Id = 4, Body = "Fourth message body", IsDeleted = false, TimeCreated = DateTime.Now, TimeEdited = DateTime.Now, OwnerUid = uid });
            return mockMessagesToReturn;
        }
    }
}

