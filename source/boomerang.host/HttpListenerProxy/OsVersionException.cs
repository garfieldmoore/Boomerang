namespace Rainbow.Testing.Boomerang.Host.HttpListenerProxy
{
    using System;
    using System.Runtime.Serialization;

    public class OsVersionException : Exception
    {
        public OsVersionException()
            : base()
        {

        }

        public OsVersionException(string message)
            : base(message)
        {

        }

        public OsVersionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected OsVersionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}