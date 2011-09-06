using System;
using NUnit.Framework;

namespace NerdDinner.Tests
{
    public static class UnitTestHelper
    {
        public static TException AssertThrows<TException>(Action action) where TException : Exception {
            try {
                action();
            }
            catch (TException e) {
                return e;
            }
            Assert.Fail();
            return null;
        }
    }
}
