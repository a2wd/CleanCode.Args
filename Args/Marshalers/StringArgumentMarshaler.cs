namespace Args.Marshalers
{
    using Exceptions;
    using System;
    using System.Collections.Generic;

    public class StringArgumentMarshaler : IArgumentMarshaler
    {
        private string stringValue = string.Empty;

        public void set(IEnumerator<string> currentArgument)
        {
            if (currentArgument.MoveNext() == false)
            {
                throw new ArgsException(ErrorCodes.MISSING_STRING);
            }

            stringValue = currentArgument.Current;
        }

        public static string getValue(IArgumentMarshaler am)
        {
            if(am != null && am.GetType() == typeof(StringArgumentMarshaler))
            {
                return ((StringArgumentMarshaler)am).stringValue;
            }

            return string.Empty;
        }
    }
}
