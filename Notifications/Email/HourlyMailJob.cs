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
        IMessageStoreClient _cli;
        public HourlyMailJob(IEmailService emailService, NotificationsContext dbContext, IMessageStoreClient client)
        {
            _emailService = emailService;
            _dbContext = dbContext;
            _cli = client;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var message = "Entering Hourly Mail Scheduled Task: " + DateTime.Now;
            Debug.WriteLine(message);

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
                        List<Message> mentionMessages = await _cli.GetMentionsSummary(s.Uid);

                        /* Only send an email if there are messages to notify */
                        if (mentionMessages.Any())
                        {
                            /* Format messages as html */
                            string mailBody = Message.ToHtml(mentionMessages);

                            /* Send email containing messages to user */
                            _emailService.Send(s.Uid, "Mention Message Summary", mailBody);
                        }

                        /* Get messages to email from message store */
                        List<Message> replyMessages = await _cli.GetRepliesSummary(s.Uid);

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
                        List<Message> messages = await _cli.GetMentionsSummary(s.Uid);

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
                        List<Message> messages = await _cli.GetRepliesSummary(s.Uid);

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
