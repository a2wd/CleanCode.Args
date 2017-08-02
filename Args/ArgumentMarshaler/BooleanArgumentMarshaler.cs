namespace Args.ArgumentMarshaler
{
    using System;

    public class BooleanArgumentMarshaler : IArgumentMarshaler
    {
        private bool booleanValue = false;

        public void set(int currentArgument)
        {
            throw new NotImplementedException();
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
