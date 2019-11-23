using Quartz;
using System;
using System.Diagnostics;

namespace Notifications.Email
{
    public class HourlyMailJob : IJob
    {

        public void Execute(IJobExecutionContext context)
        {
            //test code
            var message = "Test of quartz" + DateTime.Now;
            Debug.WriteLine(message);

        }
    }
}
