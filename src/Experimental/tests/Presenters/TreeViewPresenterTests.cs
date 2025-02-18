// ***********************************************************************
// Copyright (c) Charlie Poole and TestCentric GUI contributors.
// Licensed under the MIT License. See LICENSE file in root directory.
// ***********************************************************************

using System.Windows.Forms;
using NUnit.Framework;
using NSubstitute;

namespace TestCentric.Gui.Presenters
{
    using Views;
    using Model;
    using Elements;

    public class TreeViewPresenterTests
    {
        private ITestTreeView _view;
        private ITestModel _model;
        private TreeViewPresenter _presenter;

        [SetUp]
        public void CreatePresenter()
        {
            _view = Substitute.For<ITestTreeView>();
            _model = Substitute.For<ITestModel>();
            _model.Settings.Returns(new TestCentric.TestUtilities.Fakes.UserSettings());

            _presenter = new TreeViewPresenter(_view, _model);

            // Make it look like the view loaded
            _view.Load += Raise.Event<System.EventHandler>(_view, new System.EventArgs());
        }

        [TearDown]
        public void RemovePresenter()
        {
            //if (_presenter != null)
            //    _presenter.Dispose();

            _presenter = null;
        }

        //[Test]
        //public void WhenTestRunStarts_ResultsAreCleared()
        //{
        //    _settings.RunStarting += Raise.Event<TestEventHandler>(new TestEventArgs(TestAction.RunStarting, "Dummy", 1234));

        //    _view.Received().ClearResults();
        //}

        static object[] resultData = new object[] {
            new object[] { ResultState.Success, TestTreeView.SuccessIndex },
            new object[] { ResultState.Ignored, TestTreeView.WarningIndex },
            new object[] { ResultState.Failure, TestTreeView.FailureIndex },
            new object[] { ResultState.Inconclusive, TestTreeView.InconclusiveIndex },
            new object[] { ResultState.Skipped, TestTreeView.SkippedIndex },
            new object[] { ResultState.NotRunnable, TestTreeView.FailureIndex },
            new object[] { ResultState.Error, TestTreeView.FailureIndex },
            new object[] { ResultState.Cancelled, TestTreeView.FailureIndex }
        };

        [TestCaseSource("resultData")]
        public void WhenTestCaseCompletes_NodeShowsProperResult(ResultState resultState, int expectedIndex)
        {
            _model.IsPackageLoaded.Returns(true);
            _model.HasTests.Returns(true);
            _view.DisplayFormat.SelectedItem.Returns("NUNIT_TREE");

            var result = resultState.Status.ToString();
            var label = resultState.Label;

            var testNode = new TestNode("<test-run id='1'><test-case id='123'/></test-run>");
            var resultNode = new ResultNode(string.IsNullOrEmpty(label)
                ? string.Format("<test-case id='123' result='{0}'/>", result)
                : string.Format("<test-case id='123' result='{0}' label='{1}'/>", result, label));
            _model.Tests.Returns(testNode);

            //var treeNode = _adapter.MakeTreeNode(result);
            //_adapter.NodeIndex[suiteResult.Id] = treeNode;
            _model.Events.TestLoaded += Raise.Event<TestNodeEventHandler>(new TestNodeEventArgs(testNode));
            _model.Events.TestFinished += Raise.Event<TestResultEventHandler>(new TestResultEventArgs(resultNode));

            _view.Tree.Received().SetImageIndex(Arg.Compat.Any<TreeNode>(), expectedIndex);
        }

        [Test]
        public void WhenDisplayFormatChanges_TreeIsReloaded()
        {
            TestNode testNode = new TestNode(XmlHelper.CreateXmlNode("<test-run id='1'><test-suite id='42'/></test-run>"));
            _model.Tests.Returns(testNode);
            _view.DisplayFormat.SelectedItem.Returns("NUNIT_TREE");
            _view.DisplayFormat.SelectionChanged += Raise.Event<CommandHandler>();

            _view.Tree.Received().Add(Arg.Compat.Is<TreeNode>((tn) => ((TestNode)tn.Tag).Id == "42"));
        }
    }
}
