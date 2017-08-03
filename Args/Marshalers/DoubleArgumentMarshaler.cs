namespace Args.Marshalers
{
    using Exceptions;
    using System;
    using System.Collections.Generic;

    public class DoubleArgumentMarshaler : IArgumentMarshaler
    {
        public double doubleValue = 0;

        public void set(IEnumerator<string> currentArgument)
        {
            string parameter = null;
            try
            {
                currentArgument.MoveNext();
                parameter = currentArgument.Current;

                doubleValue = double.Parse(parameter);
            }
            catch(InvalidOperationException)
            {
                throw new ArgsException(ErrorCodes.MISSING_DOUBLE);
            }
            catch(ArgumentNullException)
            {
                throw new ArgsException(ErrorCodes.MISSING_DOUBLE);
            }
            catch(FormatException)
            {
                throw new ArgsException(ErrorCodes.INVALID_DOUBLE, parameter);
            }
            catch(OverflowException)
            {
                throw new ArgsException(ErrorCodes.INVALID_DOUBLE, parameter);
            }
        }

        public static double getValue(IArgumentMarshaler am)
        {
            if(am != null && am.GetType() == typeof(DoubleArgumentMarshaler))
            {
                return ((DoubleArgumentMarshaler)am).doubleValue;
            }

            return 0;            
        }
    }
}
