using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmithDotPizza.Authentication;
using SmithDotPizza.Dtos;
using SmithDotPizza.Services;

namespace SmithDotPizza.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class RedirectController : ControllerBase
    {
        private RedirectService RedirectService { get; }

        public RedirectController(RedirectService redirectService)
        {
            RedirectService = redirectService;
        }

        [Route("{key}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetRedirect(string key)
        {
            var url = await RedirectService.GetRedirect(key);
            return url.Map<ActionResult>(RedirectPermanent)
                .OrElse(NotFound());
        }

        [Route("{key}")]
        [HttpPut]
        public async Task<ActionResult> SetRedirect(string key, [FromBody] RedirectCreationDto redirect)
        {
            var result = await RedirectService.SetRedirect(key, redirect.Target);
            return result.Map<ActionResult>(RedirectCreated)
                .OrElse(Conflict());
        }

        [Route("urls")]
        [HttpPost]
        public async Task<ActionResult> CreateRedirect([FromBody] RedirectCreationDto redirect)
        {
            var result = await RedirectService.SetRedirect(redirect.Target);
            return result.Map<ActionResult>(RedirectCreated)
                .OrElse(Conflict());
        }

        private static ObjectResult RedirectCreated(string key)
        {
            var dto = new RedirectCreatedDto
            {
                Location = key
            };
            return new ObjectResult(dto)
            {
                StatusCode = (int) HttpStatusCode.Created
            };
        }
    }
}