using Notifications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications.Data
{
    public interface IDataRepository
    {

        Task<NotificationSetting> GetSettingAsync(string uid);

        Task AddSettingAsync(NotificationSetting setting);

        Task UpdateSettingAsync(NotificationSetting setting);

        Task DeleteSettingAsync(NotificationSetting setting);

        bool SettingExists(string uid);
    }
}
