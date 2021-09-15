using ApplicationCoreIdentityAzureADB2C.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ApplicationCoreIdentityAzureADB2C.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [RequiredScope(requiredScope)]
    public class MembersController : ControllerBase
    {
        const string requiredScope = "welcome.read";

        private readonly IHttpContextAccessor _contextAccessor;

        public MembersController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        [Route(template: "welcome")]
        public Message Welcome()
        {
            var identity = (ClaimsIdentity)_contextAccessor.HttpContext.User.Identity;

            List<Claim> claims = identity.Claims.ToList();

            var message = new Message
            {
                Id = Guid.NewGuid(),
                Name = $"{_contextAccessor.HttpContext.User.Identity.Name} " +
                    $"({claims.FirstOrDefault(x => x.Type == "emails").Value.Split(',').FirstOrDefault()} / " +
                    $"{claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value})",
                Text = $"Welcome to the Api {DateTime.Now}"
            };

            return message;
        }
    }
}
