using System;
using System.Web.Http;
using PasswordGenerator;

namespace PasswordGeneratorWeb.Controllers
{
    public class PasswordController : ApiController
    {
        private static readonly Authentication _accessTokenManager =  new Authentication(new AccessTokenGenerator(), new AccessTokenRepository(TimeSpan.FromSeconds(30)));

        [HttpPost]
        public IHttpActionResult Generate([FromBody]string userId)
        {
            var accessToken = _accessTokenManager.RequestAccessToken(userId);
            return Ok(new { Password = accessToken });
        }

        [HttpGet]
        public IHttpActionResult Verify(string userId, string password)
        {
            var isValid = _accessTokenManager.Verify(userId, password);
            return Ok(new { IsValid = isValid });
        }
    }
}
