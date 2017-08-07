namespace Args.Tests
{
    using Exceptions;
    using NUnit.Framework;

    [TestFixture]
    public class ArgsExceptionShould
    {
        [Test]
        public void CorrectlyReturnAnExceptionForAnUnexpectedArgument()
        {
            var e = new ArgsException(ErrorCodes.UNEXPECTED_ARGUMENT, 'x', null);

            Assert.AreEqual("Argument -x unexpected.", e.errorMessage());
        }

        [Test]
        public void CorrectlyReturnAnExceptionForAMissingStringParameter()
        {
            var e = new ArgsException(ErrorCodes.MISSING_STRING, 'x', null);

            Assert.AreEqual("Could not find a string parameter for -x.", e.errorMessage());
        }

        [Test]
        public void CorrectlyReturnAnExceptionForAMissingIntegerParameter()
        {
            var e = new ArgsException(ErrorCodes.MISSING_INTEGER, 'x', null);

            Assert.AreEqual("Could not find an integer parameter for -x.", e.errorMessage());
        }

        [Test]
        public void CorrectlyReturnAnExceptionForAnInvalidIntegerParameter()
        {
            var e = new ArgsException(ErrorCodes.INVALID_INTEGER, 'x', "Forty two");

            Assert.AreEqual("Argument -x expects an integer but was 'Forty two'.", e.errorMessage());
        }

        [Test]
        public void CorrectlyReturnAnExceptionForAMissingDoubleParameter()
        {
            var e = new ArgsException(ErrorCodes.MISSING_DOUBLE, 'x', null);

            Assert.AreEqual("Could not find a double parameter for -x.", e.errorMessage());
        }

        [Test]
        public void CorrectlyReturnAnExceptionForAnInvalidDoubleParameter()
        {
            var e = new ArgsException(ErrorCodes.INVALID_DOUBLE, 'x', "Forty two");

            Assert.AreEqual("Argument -x expects a double but was 'Forty two'.", e.errorMessage());
        }
    }
}
