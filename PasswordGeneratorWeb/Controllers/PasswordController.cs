using System.Web;
using System.Web.Http;
using PasswordGenerator;

namespace PasswordGeneratorWeb.Controllers
{
    public class PasswordController : ApiController
    {
        private readonly Authentication _accessTokenManager;

        public PasswordController()
        {
            _accessTokenManager = HttpContext.Current.Application["Authentication"] as Authentication;
        }

        [HttpGet]
        public IHttpActionResult Generate(string userId)
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
