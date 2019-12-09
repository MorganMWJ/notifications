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
    public class HourlyMailJob : IJob
    {
        IEmailService _emailService;
        NotificationsContext _dbContext;
        public HourlyMailJob(IEmailService emailService, NotificationsContext dbContext)
        {
            _emailService = emailService;
            _dbContext = dbContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var message = "Entering Hourly Mail Scheduled Task: " + DateTime.Now;
            Debug.WriteLine(message);

            /* client Service */
            MessageStoreClient cli = new MessageStoreClient();

            /* Get all user settings */
            List<NotificationSetting> settings = _dbContext.NotificationSetting.ToList();

            /* Email messages each user wants based on the user settings */
            foreach (NotificationSetting s in settings)
            {
                if (s.IsTimeToNotify())
                {
                    if (s.Mentions && s.Replies) //Both
                    {
                        /* Get messages to email from message store */
                        List<Message> mentionMessages = await cli.GetMentionsSummary(s.Uid);
                        //Task<List<Message>> task = cli.GetMentionsSummary(s.Uid);
                        //Task.WaitAll(task);

                        /* Only send an email if there are messages to notify */
                        if (mentionMessages.Any())
                        {
                            /* Format messages as html */
                            string mailBody = Message.ToHtml(mentionMessages);

                            /* Send email containing messages to user */
                            _emailService.Send(s.Uid, "Mention Message Summary", mailBody);
                        }

                        /* Get messages to email from message store */
                        List<Message> replyMessages = await cli.GetRepliesSummary(s.Uid);
                        //task = cli.GetRepliesSummary(s.Uid);
                        //Task.WaitAll(task);

                        if (replyMessages.Any())
                        {
                            /* Format messages as html */
                            string mailBody = Message.ToHtml(replyMessages);

                            /* Send email containing messages to user */
                            _emailService.Send(s.Uid, "Reply Message Summary", mailBody);
                        }
                    }
                    else if (s.Mentions) //Just Mentions
                    {
                        /* Get messages to email from message store */
                        List<Message> messages = await cli.GetMentionsSummary(s.Uid);
                        //Task<List<Message>> task = cli.GetMentionsSummary(s.Uid);
                        //Task.WaitAll(task);

                        if (messages.Any())
                        {
                            /* Format messages as html */
                            string mailBody = Message.ToHtml(messages);

                            /* Send email containing messages to user */
                            _emailService.Send(s.Uid, "Mention Message Summary", mailBody);
                        }
                    }
                    else if (s.Replies) //Just Replies
                    {
                        /* Get messages to email from message store */
                        List<Message> messages = await cli.GetRepliesSummary(s.Uid);
                        //Task<List<Message>> task = cli.GetRepliesSummary(s.Uid);
                        //Task.WaitAll(task);

                        if (messages.Any())
                        {
                            /* Format messages as html */
                            string mailBody = Message.ToHtml(messages);

                            /* Send email containing messages to user */
                            _emailService.Send(s.Uid, "Reply Message Summary", mailBody);
                        }
                    } 
                }
            }
        }
    }
}
