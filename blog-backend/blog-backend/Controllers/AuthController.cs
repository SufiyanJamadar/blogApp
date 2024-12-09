using blog_backend.Data;
using blog_backend.Dto;
using blog_backend.Entity;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace blog_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepository<User> _repository;

        public AuthController(IRepository<User> repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IResult> Login([FromBody] AuthDto model)
        {
            var user=(await _repository.GetAll(x=>x.Email==model.Email)).FirstOrDefault();
            if(user is not null && user.Password==model.Password)
            {
                var claimsPriciple = new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new[] { new Claim(ClaimTypes.Name,model.Email) },
                        BearerTokenDefaults.AuthenticationScheme));
                return Results.SignIn(claimsPriciple);
                
            }
            else
            {
                return Results.BadRequest();
            }
        }
    }
}
