using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications.Data
{
    public class NotificationsContext : DbContext
    {
        public NotificationsContext (DbContextOptions<NotificationsContext> options)
            : base(options)
        {
        }

        public DbSet<models.NotificationSetting> NotificationSetting { get; set; }
    }
}
