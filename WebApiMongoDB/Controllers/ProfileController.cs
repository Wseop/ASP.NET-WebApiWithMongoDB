using Microsoft.AspNetCore.Mvc;
using WebApiMongoDB.Models;
using WebApiMongoDB.Services;

namespace WebApiMongoDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _profileService;

        public ProfileController(ProfileService profileService)
        { 
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> Get()
        {
            var profiles = await _profileService.GetAsync();

            if (profiles is null)
            {
                return NotFound();
            }

            return profiles;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Profile>> Get(string name)
        {
            var profile = await _profileService.GetAsync(name);

            if (profile is null) 
            {
                return NotFound();
            }

            return profile;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Profile newProfile)
        { 
            await _profileService.CreateAsync(newProfile);

            return CreatedAtAction(nameof(Get), new { name = newProfile.Name }, newProfile);
        }

        [HttpPut("{name}")]
        public async Task<ActionResult> Put(string name, Profile updatedProfile)
        {
            var profile = await _profileService.GetAsync(name);

            if (profile is null)
            {
                return NotFound();
            }

            updatedProfile.Id = profile.Id;

            await _profileService.UpdateAsync(name, updatedProfile);

            return NoContent();
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> Delete(string name)
        {
            var profile = await _profileService.GetAsync(name);

            if (profile is null)
            {
                return NotFound();
            }

            await _profileService.RemoveAsync(name);

            return NoContent();
        }
    }
}
