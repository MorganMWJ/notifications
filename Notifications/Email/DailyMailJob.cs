using Notifications.Data;
using Notifications.Models;
using Notifications.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications.Email
{
    public class DailyMailJob : IJob
    {
        IEmailService _emailService;
        NotificationsContext _dbContext;
        public DailyMailJob(IEmailService emailService, NotificationsContext dbContext)
        {
            _emailService = emailService;
            _dbContext = dbContext;
        }

        public void Execute(IJobExecutionContext context)
        {
            /* Log exectution of scheduled task */
            var message = "Entering Daily Mailing Scheduled Task" + DateTime.Now;
            Debug.WriteLine(message);

            /* For all user settings with Daily Setting, Send Daily Summary */
            List<string> users = GetUserList();

            foreach (string uid in users)
            {
                /* Get messages to email from message store */
                MessageStoreClient cli = new MessageStoreClient();
                Task<List<Message>> task = cli.GetMockSummary(uid);
                Task.WaitAll(task);

                /* Send email containing messages to user */
                _emailService.Send(uid, "Daily Message Summary", task.Result.ToString());
            }

        }

        /**
         * Gather a lit of UIDs of the users that want a daily summary email.
         */
        private List<string> GetUserList()
        {
            List<string> usersWantingDailySummaries = new List<string>();

            /* Get all user settigns */
            List<NotificationSetting> settings = _dbContext.NotificationSetting.ToList();
            /* Note the users wanting a daily summary. */
            foreach (NotificationSetting s in settings)
            {
                if (s.Daily)
                {
                    usersWantingDailySummaries.Add(s.Uid);
                }
            }
            return usersWantingDailySummaries;
        }

        //private void SendEmail(string uid, List<Message> messagesInBody)
        //{
        //    using (var msg = new MailMessage("siarad@aber.ac.uk", $"{uid}@aber.ac.uk"))
        //    {
        //        msg.Subject = "Siarad: Daily Message Summary";
        //        msg.Body = messagesInBody.ToString(); //NEED TO CHANGE THIS TO PROPER OUTPUT STRING
        //        using (SmtpClient sc = new SmtpClient())
        //        {
        //            sc.EnableSsl = true;
        //            sc.Host = "smtp.office365.com ";
        //            sc.Port = 587;
        //            sc.Credentials = new NetworkCredential("mwj7@aber.ac.uk", "qh76T423");
        //            sc.Send(msg);
        //        }
        //    }
        //}

    }
}
