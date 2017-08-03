namespace Args.Marshalers
{
    using Exceptions;
    using System;
    using System.Collections.Generic;

    public class StringArrayArgumentMarshaler : IArgumentMarshaler
    {
        private string[] stringArrayValue = new string[] { };

        public void set(IEnumerator<string> currentArgument)
        {
            string parameter = string.Empty;
            try
            {
                currentArgument.MoveNext();
                parameter = currentArgument.Current;
                stringArrayValue = parameter.Split(new string[] { "," }, StringSplitOptions.None);
            }
            catch(InvalidOperationException)
            {
                throw new ArgsException(ErrorCodes.MISSING_STRING);
            }
            catch(ArgumentException)
            {
                throw new ArgsException(ErrorCodes.INVALID_ARGUMENT_FORMAT, parameter);
            }
        }

        public static string[] getValue(IArgumentMarshaler am)
        {
            if(am != null && am.GetType() == typeof(StringArrayArgumentMarshaler))
            {
                return ((StringArrayArgumentMarshaler)am).stringArrayValue;
            }

            return new string[] { };
        }
    }
}
