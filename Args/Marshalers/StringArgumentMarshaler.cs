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
            try
            {
                currentArgument.MoveNext();
                stringValue = currentArgument.Current;
            }
            catch(InvalidOperationException e)
            {
                throw new ArgsException(ErrorCodes.MISSING_STRING);
            }
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
