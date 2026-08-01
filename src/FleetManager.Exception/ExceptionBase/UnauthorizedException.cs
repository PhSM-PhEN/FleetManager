using System.Net;

namespace FleetManager.Exception.ExceptionBase
{
    public class UnauthorizedException(string message) : FleetManagerException(message)
    {
        public override int StatusCode => (int)HttpStatusCode.Unauthorized;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
