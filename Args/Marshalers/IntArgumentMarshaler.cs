namespace Args.Marshalers
{
    using Exceptions;
    using System;
    using System.Collections.Generic;

    public class IntArgumentMarshaler : IArgumentMarshaler
    {
        private int intValue = 0;

        public void set(IEnumerator<string> currentArgument)
        {
            string parameter = null;

            try
            {
                if(currentArgument.MoveNext() == false)
                {
                    throw new ArgsException(ErrorCodes.MISSING_INTEGER);
                }

                parameter = currentArgument.Current;
                intValue = int.Parse(parameter);
            }
            catch(ArgumentNullException e)
            {
                throw new ArgsException(ErrorCodes.MISSING_INTEGER);
            }
            catch(FormatException e)
            {
                throw new ArgsException(ErrorCodes.INVALID_INTEGER);
            }
            catch(OverflowException e)
            {
                throw new ArgsException(ErrorCodes.INVALID_INTEGER, parameter);
            }
        }

        public static int getValue(IArgumentMarshaler am)
        {
            if(am != null && am.GetType() == typeof(IntArgumentMarshaler))
            {
                return ((IntArgumentMarshaler)am).intValue;
            }

            return 0;
        }
    }
}
