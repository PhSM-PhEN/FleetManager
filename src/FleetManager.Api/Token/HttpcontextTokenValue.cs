using FleetManager.Domain.Security.Token;

namespace FleetManager.Api.Token
{
    public class HttpcontextTokenValue(IHttpContextAccessor httpContextAccessor) : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public string TokenOnRequest()
        {
            var autorization = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

            return autorization["Bearer ".Length..].Trim();
        }
    }
    
}
