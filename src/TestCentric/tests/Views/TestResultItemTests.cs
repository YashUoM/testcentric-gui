// ***********************************************************************
// Copyright (c) Charlie Poole and TestCentric GUI contributors.
// Licensed under the MIT License. See LICENSE file in root directory.
// ***********************************************************************

using NUnit.Framework;

namespace TestCentric.Gui.Views
{
    [TestFixture]
    public class TestResultItemTests
    {
        [TestCase("message", "stack trace string")]
        [TestCase("message", null)]
        [TestCase(null, "stack trace string")]
        public void ToStringTest(string message, string stackTrace)
        {
            var item = new TestResultItem("dummy test name", message, stackTrace);
            Assert.IsNotNull(item.ToString());
        }
    }
}
