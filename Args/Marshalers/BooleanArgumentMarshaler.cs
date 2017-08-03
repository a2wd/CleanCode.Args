namespace Args.Marshalers
{
    using System;
    using System.Collections.Generic;

    public class BooleanArgumentMarshaler : IArgumentMarshaler
    {
        private bool booleanValue = false;

        public void set(IEnumerator<string> currentArgument)
        {
            booleanValue = true;
        }

        public static bool getValue(IArgumentMarshaler am)
        {
            if(am != null && am.GetType() == typeof(BooleanArgumentMarshaler))
            {
                return ((BooleanArgumentMarshaler)am).booleanValue;
            }

            return false;
        }
    }
}
