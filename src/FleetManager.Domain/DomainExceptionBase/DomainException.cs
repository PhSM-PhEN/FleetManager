
namespace FleetManager.Domain.DomainExceptionBase
{
    public abstract class DomainException : System.Exception
    {  
            protected DomainException(string message) : base(message) { }

    }
}
