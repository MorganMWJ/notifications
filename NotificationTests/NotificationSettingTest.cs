using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notifications.Models;

namespace NotificationTests
{
    [TestClass]
    class NotificationSettingTest
    {
        [TestMethod]
        public void TestIsTimeToNotify()
        {
            //DateTime year,month, day, hour, min, sec
            DateTime lastUpdated = new DateTime(2019, 11, 29, 13, 30, 3);
            int interval = 5;

            DateTime now = DateTime.Now;
            int hourDiff = (now - lastUpdated).Hours;

            bool expec = hourDiff % interval == 0;

            NotificationSetting ns = new NotificationSetting() { Uid="mwj7", Daily=false, Mentions=false, Replies=false, NotificationInterval=interval, LastUpdated=lastUpdated };

            Assert.AreEqual(expec, ns.IsTimeToNotify());
        }
    }
}
