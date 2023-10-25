namespace LogAPI.Exceptions
{
    public class CustomException : Exception
    {
    }

    public class ContextException : Exception
    {
        public ContextException() :base("Context is empty "){ 
           
        }
        public ContextException(string message) : base(message)
        {
        }
    }

    public class LogException : Exception
    {
        public LogException() : base("Log is empty ")
        {
        }
        public LogException(string message) : base(message)
        {
        }
    }
}
