using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    }
}
