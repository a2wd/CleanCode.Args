namespace Args.Tests
{
    using Exceptions;
    using NUnit.Framework;
    using System;

    [TestFixture]
    class ArgsShould
    {
        [Test]
        public void BeInitializableWithNoSchemaOrArguments()
        {
            try
            {
                new Args("", new string[] { "-x" });
                throw new Exception();
            }
            catch(ArgsException e)
            {
                Assert.AreEqual(ErrorCodes.UNEXPECTED_ARGUMENT, e.getErrorCode());
                Assert.AreEqual('x', e.getErrorArgumentId());
            }
        }
    }
}
