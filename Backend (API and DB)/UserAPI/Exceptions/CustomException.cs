namespace UserAPI.Exceptions
{
    public class CustomException
    {
    }
    public class ContextException : Exception {
        public ContextException() : base("Context is Empty")
        {

        }
        public ContextException(string message) : base(message)
        {

        } 
        
    }

    public class InternException : Exception
    {
        public InternException() : base("Not approved yet")
        {

        }
        public InternException(string message) : base(message)
        {

        }

    }
}
