// ***********************************************************************
// Copyright (c) Charlie Poole and TestCentric GUI contributors.
// Licensed under the MIT License. See LICENSE file in root directory.
// ***********************************************************************

using NSubstitute;
using NUnit.Framework;

namespace TestCentric.Gui.Presenters.Main
{
    using Model;

    public class WhenTestsAreReloaded : MainPresenterTestBase
    {
        [SetUp]
        public void SimulateTestReload()
        {
            ClearAllReceivedCalls();

            _model.HasTests.Returns(true);
            _model.IsTestRunning.Returns(false);

            TestNode testNode = new TestNode("<test-suite id='1'/>");
            _model.Tests.Returns(testNode);
            _model.TestPackage.Returns(new NUnit.Engine.TestPackage("dummy.dll"));

            FireTestReloadedEvent(testNode);
        }

#if NYI // Add after implementation of project or package saving
        [TestCase("NewProjectCommand", true)]
        [TestCase("OpenProjectCommand", true)]
        [TestCase("SaveCommand", true)]
        [TestCase("SaveAsCommand", true)
#endif

        [TestCase("OpenCommand", true)]
        [TestCase("CloseCommand", true)]
        [TestCase("AddTestFilesCommand", true)]
        [TestCase("ReloadTestsCommand", true)]
        [TestCase("RecentFilesMenu", true)]
        [TestCase("ExitCommand", true)]
        [TestCase("RunAllMenuCommand", true)]
        [TestCase("RunSelectedMenuCommand", true)]
        [TestCase("RunFailedMenuCommand", false)]
        [TestCase("StopRunMenuCommand", false)]
        [TestCase("ForceStopMenuCommand", false)]
        [TestCase("TestParametersMenuCommand", true)]
        [TestCase("SaveResultsCommand", false)]
        [TestCase("RunAllToolbarCommand", true)]
        [TestCase("RunSelectedToolbarCommand", true)]
        [TestCase("DebugAllToolbarCommand", true)]
        [TestCase("DebugSelectedToolbarCommand", true)]
        [TestCase("TestParametersToolbarCommand", true)]
        [TestCase("StopRunButton", false)]
        [TestCase("ForceStopButton", false)]
        public void CheckCommandEnabled(string propName, bool enabled)
        {
            ViewElement(propName).Received().Enabled = enabled;
        }

        [TestCase("StopRunButton", true)]
        [TestCase("ForceStopButton", false)]
        public void CheckElementVisibility(string propName, bool visible)
        {
            ViewElement(propName).Received().Visible = visible;
        }
    }
}
