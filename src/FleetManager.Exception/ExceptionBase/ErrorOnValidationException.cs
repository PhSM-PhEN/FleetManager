using System.Net;

namespace FleetManager.Exception.ExceptionBase
{
    public class ErrorOnValidationException : FleetManagerException
    {
        private readonly List<string> _errors;

        public ErrorOnValidationException(List<string> errorMessage) : base(string.Empty)
        {
            _errors = errorMessage;
        }

        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public override List<string> GetErrors()
        {
            return _errors;
        }
    }
}
