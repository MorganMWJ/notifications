using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        private readonly IDataRepository _repo;

        public UserNotificationsController(IDataRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public string Hello()
        {
            return "Hello Notification World";
        }

        // GET api/<controller>/{uid}
        [HttpGet("{uid}")]
        public async Task<IActionResult> GetSettingById(string uid)
        {
            NotificationSetting setting = await _repo.GetSettingAsync(uid);
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
            NotificationSetting previousSetting = await _repo.GetSettingAsync(setting.Uid);
            if (previousSetting != null)
            {
                return BadRequest($"Settings for user {setting.Uid} already exist");
            }

            await _repo.AddSettingAsync(setting);
            return CreatedAtAction(nameof(GetSettingById), new { uid = setting.Uid }, setting);
        }

        // PUT api/<controller>/{uid}
        [HttpPut("{uid}")]
        public async Task<IActionResult> UpdateSetting(string uid, [FromBody]NotificationSetting setting)
        {
            if (!uid.Equals(setting.Uid))
            {
                return BadRequest();
            }

            //NotificationSetting previousSetting = await _repo.GetSettingAsync(uid);
            //if (previousSetting == null)
            //{
            //    return NotFound();
            //}

            //await _repo.UpdateSettingAsync(setting);

            //return Ok();
            
            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.UpdateSettingAsync(setting);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repo.SettingExists(setting.Uid))
                    {
                        return BadRequest();
                    }
                    throw;
                }
            }
            return Ok();
        }

        // DELETE api/<controller>/{uid}
        [HttpDelete("{uid}")]
        public async Task<IActionResult> DeleteSetting(string uid)
        {
            var setting = await _repo.GetSettingAsync(uid);
            if (setting == null)
            {
                return NotFound();
            }

            await _repo.DeleteSettingAsync(setting);

            return Ok();
        }


    }
}
