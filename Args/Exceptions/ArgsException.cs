namespace Args.Exceptions
{
    using System;

    public class ArgsException : Exception
    {
        private char errorArgumentId = '\0';
        private string errorParameter = null;
        private ErrorCodes errorCode = ErrorCodes.OK;

        public ArgsException() { }

        public ArgsException(string message) : base(message) { }
    }
}
