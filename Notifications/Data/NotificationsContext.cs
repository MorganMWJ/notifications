using Microsoft.EntityFrameworkCore;

namespace Notifications.Data
{
    public class NotificationsContext : DbContext
    {
        public NotificationsContext(DbContextOptions<NotificationsContext> options)
            : base(options)
        {
        }

        public NotificationsContext()
        {
        }

        public DbSet<Models.NotificationSetting> NotificationSetting { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
