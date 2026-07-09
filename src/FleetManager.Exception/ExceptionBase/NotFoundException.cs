

using System.Net;

namespace FleetManager.Exception.ExceptionBase
{
    public class NotFoundException(string message) : FleetManagerException(message)
    {
        public override int StatusCode => (int) HttpStatusCode.NotFound;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
