using System.Runtime.Serialization;

namespace ContactServiceSolution.Service.CustomException
{
    using System;

    [Serializable]
    public class ContactNotFoundException : Exception
    {
        public ContactNotFoundException()
        {
        }
        public ContactNotFoundException(string message)
            : base(message)
        {
        }
        public ContactNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
        protected ContactNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class ContactAlreadyExistException : Exception
    {
        public ContactAlreadyExistException()
        {
        }
        public ContactAlreadyExistException(string message)
            : base(message)
        {
        }
        public ContactAlreadyExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
        protected ContactAlreadyExistException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class ContactRecordsNotFound : Exception
    {
        public ContactRecordsNotFound()
        {
        }
        public ContactRecordsNotFound(string message)
            : base(message)
        {
        }
        public ContactRecordsNotFound(string message, Exception inner)
            : base(message, inner)
        {
        }
        protected ContactRecordsNotFound(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
