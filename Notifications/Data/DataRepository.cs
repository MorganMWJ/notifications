using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notifications.Models;

namespace Notifications.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly NotificationsContext _context;

        public DataRepository(NotificationsContext context)
        {
            _context = context;
        }

        public async Task AddSettingAsync(NotificationSetting setting)
        {
            _context.NotificationSetting.Add(setting);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSettingAsync(NotificationSetting setting)
        {
            _context.NotificationSetting.Remove(setting);
            await _context.SaveChangesAsync();
        }

        public async Task<NotificationSetting> GetSettingAsync(string uid)
        {
            return await _context.NotificationSetting.FindAsync(uid);
        }

        public bool SettingExists(string uid)
        {
            return _context.NotificationSetting.Any(s => s.Uid.Equals(uid));
        }

        public async Task UpdateSettingAsync(NotificationSetting setting)
        {
            _context.NotificationSetting.Update(setting);
            await _context.SaveChangesAsync();
        }
    }
}
