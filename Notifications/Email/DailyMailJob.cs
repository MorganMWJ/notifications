﻿using Notifications.Data;
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

        public async Task Execute(IJobExecutionContext context)
        {
            /* Log exectution of scheduled task */
            var message = "Entering Daily Mailing Scheduled Task: " + DateTime.Now;
            Debug.WriteLine(message);

            /* For all user settings with Daily Setting, Send Daily Summary */
            List<string> users = GetUserList();

            foreach (string uid in users)
            {
                /* Get messages to email from message store */
                MessageStoreClient cli = new MessageStoreClient();
                List<Message> messages = await cli.GetMockSummaryDONTUSE(uid);

                //Task<List<Message>> task = cli.GetDailySummary(uid);
                //Task.WaitAll(task);
                //List<Message> messages = task.Result;

                if (messages.Count > 0)
                {
                    /* Format messages as html */
                    string mailBody = Message.ToHtml(messages);

                    /* Send email containing messages to user */
                    _emailService.Send(uid, "Daily Message Summary", mailBody);
                }
                else
                {
                    /* Do not send an email if there are no messages */
                }
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

    }
}
