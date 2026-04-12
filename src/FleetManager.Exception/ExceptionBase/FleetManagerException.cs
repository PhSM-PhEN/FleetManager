namespace FleetManager.Exception.ExceptionBase
{
    public abstract class FleetManagerException (string message) : SystemException(message)
    {
        public abstract int StatusCode { get; }
        public abstract List<string> GetErrors();
    }
}
