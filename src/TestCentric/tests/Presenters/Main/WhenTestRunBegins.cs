// ***********************************************************************
// Copyright (c) Charlie Poole and TestCentric GUI contributors.
// Licensed under the MIT License. See LICENSE file in root directory.
// ***********************************************************************

using NSubstitute;
using NUnit.Framework;

namespace TestCentric.Gui.Presenters.Main
{
    public class WhenTestRunBegins : MainPresenterTestBase
    {
        [SetUp]
        protected void SimulateTestRunStarting()
        {
            ClearAllReceivedCalls();

            _model.HasTests.Returns(true);
            _model.IsTestRunning.Returns(true);
            _model.TestPackage.Returns(new NUnit.Engine.TestPackage("dummy.dll"));
            FireRunStartingEvent(1234);
        }

#if NYI // Add after implementation of project or package saving
        [TestCase("NewProjectCommand", false)]
        [TestCase("OpenProjectCommand", false)]
        [TestCase("SaveCommand", false)]
        [TestCase("SaveAsCommand", false)
#endif

        [TestCase("OpenCommand", false)]
        [TestCase("CloseCommand", false)]
        [TestCase("AddTestFilesCommand", false)]
        [TestCase("ReloadTestsCommand", false)]
        [TestCase("RecentFilesMenu", false)]
        [TestCase("ExitCommand", true)]
        [TestCase("RunAllMenuCommand", false)]
        [TestCase("RunSelectedMenuCommand", false)]
        [TestCase("RunFailedMenuCommand", false)]
        [TestCase("StopRunMenuCommand", true)]
        [TestCase("ForceStopMenuCommand", true)]
        [TestCase("TestParametersMenuCommand", false)]
        [TestCase("SaveResultsCommand", false)]
        [TestCase("RunAllToolbarCommand", false)]
        [TestCase("RunSelectedToolbarCommand", false)]
        [TestCase("DebugAllToolbarCommand", false)]
        [TestCase("DebugSelectedToolbarCommand", false)]
        [TestCase("TestParametersToolbarCommand", false)]
        [TestCase("StopRunButton", true)]
        [TestCase("ForceStopButton", true)]
        public void CheckCommandEnabled(string propName, bool enabled)
        {
            ViewElement(propName).Received().Enabled = enabled;
        }

        [TestCase("RunSummaryButton", false)]
        [TestCase("StopRunButton", true)]
        [TestCase("ForceStopButton", false)]
        public void CheckElementVisibility(string propName, bool visible)
        {
            ViewElement(propName).Received().Visible = visible;
        }
    }
}
