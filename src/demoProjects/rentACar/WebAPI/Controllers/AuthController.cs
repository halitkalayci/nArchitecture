using Application.Features.Auths.Commands.CreateOtp;
using Application.Features.Auths.Commands.Login;
using Application.Features.Auths.Commands.VerifyOtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            var loggedDto = await Mediator.Send(loginCommand);
            return Ok(loggedDto);
        }
        [HttpPost("enable-otp")]
        public async Task<IActionResult> EnableOtp()
        {
            CreateOtpCommand createOtpCommand = new() { UserId = getUserId() };
            var result = await Mediator.Send(createOtpCommand);
            return Ok(result);
        }
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp( [FromBody]string verifyCode)
        {
            VerifyOtpCommand verifyOtpCommand = new() { UserId = getUserId(),ActivationCode=verifyCode };
            var result = await Mediator.Send(verifyOtpCommand);
            return Ok(result);
        }
    }
}
