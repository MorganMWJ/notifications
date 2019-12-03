using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notifications.Models
{
    [Table("notification_setting")]
    public class NotificationSetting
    {
        [Column("uid")]
        [Key]
        public String Uid { get; set; }

        [Required]
        [Column("daily")]
        public bool Daily { get; set; }

        [Required]
        [Column("mentions")]
        public bool Mentions { get; set; }

        [Required]
        [Column("replies")]
        public bool Replies { get; set; }

        [Required]
        [Column("notification_interval")]
        public int NotificationInterval { get; set; }

        [Required]
        [Column("last_updated")]
        public DateTime LastUpdated { get; set; }

        /**
         * If the hour of the day is equal to notification interval then true,
         * false otherwise.
         */
        public bool IsTimeToNotify()
        {
            /* difference between now and when last updated (in hours) */
            DateTime currentTime = DateTime.Now;
            TimeSpan difference = currentTime - LastUpdated;
            int hoursDiff = difference.Hours;

            /* If difference in hours is a multiple of user's set interval then it is time to notify the user */
            return hoursDiff % NotificationInterval == 0;
        }
    }
}
