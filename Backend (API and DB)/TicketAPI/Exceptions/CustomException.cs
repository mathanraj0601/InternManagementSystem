namespace TicketAPI.Exceptions
{
    public class CustomException
    {
    }

    public class ContextException : Exception
    {
        public ContextException() : base("Context is Empty")
        {
        }
        public ContextException(string message) : base(message)
        {
        }
    }

    public class TicketNotFoundException : Exception
    {
        public TicketNotFoundException() : base("Ticket Not Found")
        {
        }
        public TicketNotFoundException(string message) : base(message)
        {
        }
    }
}
