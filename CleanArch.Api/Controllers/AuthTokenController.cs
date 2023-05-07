using Application.Auth.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Masiv.Api.Controllers
{
    [Route("api/auth-token")]
    [ApiController]
    public class AuthTokenController : ApiControllerBase
    {
        public AuthTokenController()
        {
        }


        [HttpPost]
        public async Task<IActionResult> Authentication([FromBody] PostLoginCommand command)
        {

            return Ok(await Mediator.Send(command));
        }



    }
}
