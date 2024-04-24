using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaCycle.Application.Interfaces;
using PruebaTecnicaCycle.Domain.Dtos.Identity;

namespace PruebaTecnicaCycle.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IAuthService _authService;

        public IdentityController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] AuthRequest authRequest)
        {
            if (authRequest == null) return BadRequest();

            var result = await _authService.Login(authRequest);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost, Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequest registrationRequest)
        {
            if (registrationRequest == null) return BadRequest();

            var result = await _authService.RegisterUser(registrationRequest);

            if (result == null) return BadRequest();

            return Ok(result);
        }
    }
}