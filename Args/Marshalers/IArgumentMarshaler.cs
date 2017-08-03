namespace Args.Marshalers
{
    using System.Collections.Generic;

    public interface IArgumentMarshaler
    {
        void set(IEnumerator<string> currentArgument);
    }
}
