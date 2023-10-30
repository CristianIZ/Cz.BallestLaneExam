using CZ.Business.Services;
using CZ.Business.UseCases.Interfaces;
using CZ.DTO.PublicationManagement;
using CZ.SQLContext.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CZ.API.Controllers
{
    public class SocialMediaExample : Controller
    {
        public readonly IAccountServices _accountServices;
        public readonly IAddPublicationUC _addPublicationUC;

        public SocialMediaExample(IAccountServices accountServices,
            IAddPublicationUC addPublicationUC)
        {
            this._accountServices = accountServices;
            this._addPublicationUC = addPublicationUC;
        }

        // POST: SocialMediaExample/Create
        [HttpPost("publicate-text")]
        [Authorize]
        public async Task<IActionResult> CreatePublicationText(PublicationTextDto publicationDto)
        {
            await this.CheckValidToken();
            var publication = await this._addPublicationUC.CreatePublicationText(publicationDto);
            return Ok(publication);
        }

        // POST: SocialMediaExample/Edit/5
        [HttpPost("publicate-Image")]
        [Authorize]
        public async Task<IActionResult> CreatePublicationImage(PublicationImageDto publicationImageDto)
        {
            await this.CheckValidToken();

            return Ok();
        }

        private async Task CheckValidToken()
        {
            var token = this.Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
            var isValidToken = await _accountServices.ValidateToken(this.HttpContext.User.Identity as ClaimsIdentity, token);

            if (!isValidToken)
                throw new Exception("Token is not valid");
        }
    }
}
