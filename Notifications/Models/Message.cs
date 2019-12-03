using Newtonsoft.Json;
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
        [JsonProperty("id", Order = 1)]
        public int Id { get; set; }

        [Required]
        [JsonProperty("body", Order = 2)]
        public string Body { get; set; }

        [JsonProperty("ownerUid", Order = 3)]
        public string OwnerUid { get; set; }

        [Required]
        [JsonProperty("isDeleted", Order = 4)]
        public bool IsDeleted { get; set; }

        [Required]
        [JsonProperty("timeCreated", Order = 5)]
        public DateTime TimeCreated { get; set; }

        [Required]
        [JsonProperty("timeEdited", Order = 6)]
        public DateTime TimeEdited { get; set; }

        [JsonProperty("groupId", Order = 7)]
        public int GroupId { get; set; }

        [JsonProperty("messageCollection", Order = 8)]
        public List<Message> MessageCollection { get; set; }

        internal static string ToHtml(List<Message> messages)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<html><body>");
            sb.Append("<h1>New Messages:</h1>");
            foreach(Message m in messages)
            {
                sb.Append($"<h4>Message {m.Id}:</h4>");
                sb.Append($"<p><strong>User ID: </strong>{m.OwnerUid}</p>");
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
