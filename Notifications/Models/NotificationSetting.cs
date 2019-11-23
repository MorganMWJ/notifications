using System;
using System.ComponentModel.DataAnnotations;

namespace Notifications.Models
{
    public class NotificationSetting
    {
        [Key]
        public String Uid { get; set; }

        [Required]
        public bool Daily { get; set; }

        [Required]
        public bool Mentions { get; set; }

        [Required]
        public bool Replies { get; set; }

        [Required]
        public int NotificationInterval { get; set; }
    }
}
