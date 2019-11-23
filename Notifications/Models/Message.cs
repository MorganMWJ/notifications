using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Notifications.Models
{
    /**
     * NOT CORRECT REVISIT!
     */
    public class Message
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public DateTime TimeCreated { get; set; }

        [Required]
        public DateTime TimeEdited { get; set; }

        public string Uid { get; set; }

        public int ParentMessageId { get; set; }

        public List<Message> ChildMessages { get; set; }

        internal static string ToHtml(List<Message> messages)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<html><body>");

            foreach(Message m in messages)
            {
                sb.Append($"<h1>Message {m.Id}:</h1>");
                sb.Append($"<p><strong>User ID: </strong>{m.Uid}</p>");
                sb.Append($"<p><strong>Time Created: </strong>{m.TimeCreated}</p>");
                sb.Append($"<p><strong>Time Edited: </strong>{m.TimeEdited}</p>");
                sb.Append($"<p><strong>Body: </strong><br />{m.Body}</p>");
                sb.Append("<hr />");
            }

            sb.Append("</html></body>");

            return sb.ToString();
        }
    }
}
