using Application.Behavior.Auth;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Auth;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<Guid> RegisterAsync([FromBody] RegisterDto dto)
        {
            RegisterCommand command = dto.Adapt<RegisterCommand>();
            return await mediator.Send(command);
        }

        [HttpPost("login")]
        public async Task<LoginResponse> LoginAsync([FromBody] LoginDto dto)
        {
            LoginCommand command = dto.Adapt<LoginCommand>();
            return await mediator.Send(command);
        }
    }
}