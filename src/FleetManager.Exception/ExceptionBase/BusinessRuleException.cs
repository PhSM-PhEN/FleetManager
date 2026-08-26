using System.Net;

namespace FleetManager.Exception.ExceptionBase
{
    public class BusinessRuleException(string message) : FleetManagerException(message)
    {
        public override int StatusCode => (int)HttpStatusCode.Conflict;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
