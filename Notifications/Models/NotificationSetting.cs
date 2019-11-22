using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications.models
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
