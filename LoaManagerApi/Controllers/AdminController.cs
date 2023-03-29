using LoaManagerApi.Models;
using LoaManagerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoaManagerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService) 
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> Get()
        {
            var admins = await _adminService.GetAsync();

            if (admins is null)
            {
                return NotFound();
            }

            return admins;
        }

        [HttpGet("{type}")]
        public async Task<ActionResult<Admin>> Get(int type)
        { 
            var admin = await _adminService.GetAsync(type);

            if (admin is null)
            {
                return NotFound();
            }

            return admin;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Admin newAdmin)
        { 
            await _adminService.CreateAsync(newAdmin);

            return CreatedAtAction(nameof(Get), new { type = newAdmin.Type }, newAdmin);
        }

        [HttpPut("{type}")]
        public async Task<ActionResult> Put(int type, Admin updatedAdmin)
        {
            var admin = await _adminService.GetAsync(type);

            if (admin is null)
            {
                return NotFound();
            }

            updatedAdmin.Id = admin.Id;

            await _adminService.UpdateAsync(type, updatedAdmin);

            return NoContent();
        }

        [HttpDelete("{type}")]
        public async Task<ActionResult> Delete(int type)
        {
            var admin = await _adminService.GetAsync(type);

            if (admin is null)
            {
                return NotFound();
            }

            await _adminService.RemoveAsync(type);

            return NoContent();
        }
    }
}
