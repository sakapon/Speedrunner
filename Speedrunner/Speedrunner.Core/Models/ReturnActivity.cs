using System;
using System.Runtime.Serialization;

namespace TasSample.Models
{
    public class ReturnActivity : Activity
    {
        public override void ExecuteActivity()
        {
            throw new ReturnException();
        }
    }

    [Serializable]
    public class ReturnException : Exception
    {
        public ReturnException() { }
        public ReturnException(string message) : base(message) { }
        public ReturnException(string message, Exception inner) : base(message, inner) { }
        protected ReturnException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
