namespace Args.Tests
{
    using Exceptions;
    using NUnit.Framework;
    using System;

    [TestFixture]
    class ArgsShould
    {
        [Test]
        public void BeInitialisableWithNoSchemaOrArgs()
        {
            var args = new Args(string.Empty, new string[] { });

            Assert.NotNull(args);
        }

        [Test]
        public void ThrowAnErrorWhenInitializedWithNoSchemaAndArgs()
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

        [Test]
        public void ThrowAnErrorWhenInitializedWithNoSchemaAndMultipleArgs()
        {
            try
            {
                new Args("", new string[] { "-x", "-y" });
                throw new Exception();
            }
            catch(ArgsException e)
            {
                Assert.AreEqual(ErrorCodes.UNEXPECTED_ARGUMENT, e.getErrorCode());
                Assert.AreEqual('x', e.getErrorArgumentId());
            }
        }

        [Test]
        public void ThrowAnErrorWhenInitialisedWithASchemaWithoutLetters()
        {
            try
            {
                new Args("*", new string[] {});
                throw new Exception();
            }
            catch(ArgsException e)
            {
                Assert.AreEqual(ErrorCodes.INVALID_ARGUMENT_NAME, e.getErrorCode());
                Assert.AreEqual('*', e.getErrorArgumentId());
            }
        }

        [Test]
        public void ThrowAnErrorWhenInitialisedWithInvalidSchemaTypes()
        {
            try
            {
                new Args("f~", new string[] {});
                throw new Exception();
            }
            catch(ArgsException e)
            {
                Assert.AreEqual(ErrorCodes.INVALID_ARGUMENT_FORMAT, e.getErrorCode());
                Assert.AreEqual('f', e.getErrorArgumentId());
            }
        }

        [Test]
        public void ReturnTheCorrectValueWhenPassedABoolean()
        {
            var args = new Args("x", new string[] { "-x" });

            Assert.AreEqual(true, args.getBoolean('x'));
        }

        [Test]
        public void ReturnTheCorrectValueWhenPassedAString()
        {
            var stringValue = "test-value";
            var args = new Args("x*", new string[] { "-x", stringValue });

            Assert.AreEqual(stringValue, args.getString('x'));
        }

        [Test]
        public void ThrowAnErrorWhenInitialisedWithAStringAndNoCorrespondingArgValue()
        {
            try
            {
                new Args("x*", new string[] { "-x" });
                throw new Exception();
            }
            catch (ArgsException e)
            {
                Assert.AreEqual(ErrorCodes.MISSING_STRING, e.getErrorCode());
                Assert.AreEqual('x', e.getErrorArgumentId());
            }
        }

        [Test]
        public void BeInitialisableWithSpacesInTheSchema()
        {
            var args = new Args("x, y", new string[] { "-xy" });

            Assert.True(args.has('x'));
            Assert.True(args.has('y'));

            Assert.AreEqual(true, args.getBoolean('x'));
            Assert.AreEqual(true, args.getBoolean('y'));
        }

        [Test]
        public void ReturnTheCorrectValueWhenPassedAnInteger()
        {
            var intValue = 42;
            var args = new Args("x#", new string[] { "-x", intValue.ToString()});

            Assert.AreEqual(intValue, args.getInt('x'));
        }

        [Test]
        public void ThrowAnErrorWhenInitialisedWithAnInvalidIntegerValue()
        {
            string invalidIntegerParameter = "Forty-two";
            try
            {
                new Args("x#", new string[] { "-x", invalidIntegerParameter });
                throw new Exception();
            }
            catch (ArgsException e)
            {
                Assert.AreEqual(ErrorCodes.INVALID_INTEGER, e.getErrorCode());
                Assert.AreEqual('x', e.getErrorArgumentId());
                Assert.AreEqual(invalidIntegerParameter, e.getErrorParameter());
            }
        }

        [Test]
        public void ThrowAnErrorWhenInitialisedWithAMissingIntegerValue()
        {
            try
            {
                new Args("x#", new string[] { "-x" });
                throw new Exception();
            }
            catch (ArgsException e)
            {
                Assert.AreEqual(ErrorCodes.MISSING_INTEGER, e.getErrorCode());
                Assert.AreEqual('x', e.getErrorArgumentId());
            }
        }

        [Test]
        public void ReturnTheCorrectValueWhenPassedADouble()
        {
            var doubleValue = 3.1419d;
            var args = new Args("x##", new string[] { "-x", doubleValue.ToString()});

            Assert.AreEqual(doubleValue, args.getDouble('x'));
        }

        [Test]
        public void ThrowAnErrorWhenInitialisedWithAInvalidDoubleValue()
        {
            string invalidDoubleParameter = "Three point something";
            try
            {
                new Args("x##", new string[] { "-x", invalidDoubleParameter });
                throw new Exception();
            }
            catch (ArgsException e)
            {
                Assert.AreEqual(ErrorCodes.INVALID_DOUBLE, e.getErrorCode());
                Assert.AreEqual('x', e.getErrorArgumentId());
                Assert.AreEqual(invalidDoubleParameter, e.getErrorParameter());
            }
        }

        [Test]
        public void ThrowAnErrorWhenInitialisedWithAMissingDoubleValue()
        {
            try
            {
                new Args("x##", new string[] { "-x" });
                throw new Exception();
            }
            catch (ArgsException e)
            {
                Assert.AreEqual(ErrorCodes.MISSING_DOUBLE, e.getErrorCode());
                Assert.AreEqual('x', e.getErrorArgumentId());
            }
        }
    }
}
