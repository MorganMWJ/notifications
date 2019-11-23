using Microsoft.AspNetCore.Mvc;
using Notifications.Data;
using Notifications.Email;
using Notifications.Models;
using Quartz;
using System.Threading.Tasks;

namespace Notifications
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserNotificationsController : Controller
    {

        private NotificationsContext _context;

        public UserNotificationsController(NotificationsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public string Hello()
        {
            return "Hello Notification World";
        }

        // GET api/<controller>/5
        [HttpGet("{uid}")]
        public async Task<IActionResult> GetSettingById(string uid)
        {
            NotificationSetting setting = await _context.NotificationSetting.FindAsync(uid);
            if (setting == null)
            {
                return NotFound();
            }

            return Ok(setting);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateSetting([FromBody]NotificationSetting setting)
        {
            NotificationSetting previousSetting = await _context.NotificationSetting.FindAsync(setting.Uid);
            if (previousSetting != null)
            {
                return BadRequest($"Settings for user {setting.Uid} already exist");
            }

            _context.NotificationSetting.Add(setting);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSettingById), new { uid = setting.Uid }, setting);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSetting(string uid, [FromBody]NotificationSetting setting)
        {
            if (uid != setting.Uid)
            {
                return BadRequest();
            }

            NotificationSetting previousSetting = await _context.NotificationSetting.FindAsync(uid);
            if (previousSetting == null)
            {
                return NotFound();
            }

            _context.Update(setting);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetting(string uid)
        {
            var setting = await _context.NotificationSetting.FindAsync(uid);
            if (setting == null)
            {
                return NotFound();
            }

            _context.NotificationSetting.Remove(setting);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
