using System.Net;

namespace FleetManager.Exception.ExceptionBase
{
    public class InvalidLoginException : FleetManagerException
    {
        public InvalidLoginException() : base(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID)
        {
            
        }
        public override int StatusCode => (int) HttpStatusCode.Unauthorized;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
