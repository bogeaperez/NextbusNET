using System;
using System.Runtime.Serialization;

namespace NextbusNET
{
    public class NextbusException : Exception
    {
        public bool ShouldRetry { get; internal set; }

        public NextbusException()
        {
        }

        public NextbusException(string message)
            : base(message)
        {
        }

        public NextbusException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }

}