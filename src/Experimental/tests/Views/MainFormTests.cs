// ***********************************************************************
// Copyright (c) Charlie Poole and TestCentric GUI contributors.
// Licensed under the MIT License. See LICENSE file in root directory.
// ***********************************************************************

using System.Linq;
using NUnit.Framework;

namespace TestCentric.Gui.Views
{
    using Elements;

    [Platform(Exclude = "Linux", Reason = "Uninitialized form causes an error in Travis-CI")]
    public class MainFormTests
    {
        private IMainView _view;

        [SetUp]
        public void CreateView()
        {
            _view = new MainForm();
        }

        //[Test]
        //public void SelectedRuntimeTest()
        //{
        //    var selectedRuntime = _view.SelectedRuntime as CheckedToolStripMenuGroup;

        //    Assert.NotNull(selectedRuntime, "SelectedRuntime not set properly");
        //    Assert.NotNull(selectedRuntime.TopMenu, "TopMenu is not set");
        //    Assert.That(selectedRuntime.MenuItems.Select((p) => p.Text), Is.EqualTo(new string[] { "Default" }));
        //    Assert.That(selectedRuntime.MenuItems.Select((p) => p.Tag), Is.EqualTo(new string[] { "DEFAULT" }));
        //}
    }
}
