using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace Notifications.Email
{
    public class EmailScheduler
    {
        public static void Start(StdSchedulerFactory factory)
        {
            /* STEP 1: get a scheduler and Start it. */
            var scheduler = factory.GetScheduler();
            scheduler.Start();//.Wait();

            /* STEP 2: We create HourlyMailJob & DailyMailJob instances using Quartz JobBuilder. */
            IJobDetail dailyJob = JobBuilder.Create<DailyMailJob>().Build();
            IJobDetail hourlyJob = JobBuilder.Create<HourlyMailJob>().Build();

            /* STEP 3: We Create a Trigger and configure it to fire once everyday. */
            ITrigger dailyTrigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                    s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                   )
                .Build();            

            /* STEP 4: We Create a Trigger and configure it to fire once every hour. */
            ITrigger hourlyTrigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                    s.WithIntervalInSeconds(5)
                    .OnEveryDay()
                   )
                .Build();

            /* STEP 5: Finally we schedule our Jobs in the scheduler using the trigger we created above. */
            scheduler.ScheduleJob(dailyJob, dailyTrigger);
            scheduler.ScheduleJob(hourlyJob, hourlyTrigger);
        }
    }
}
