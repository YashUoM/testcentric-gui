﻿// ***********************************************************************
// Copyright (c) Charlie Poole and TestCentric GUI contributors.
// Licensed under the MIT License. See LICENSE file in root directory.
// ***********************************************************************

namespace TestCentric.Gui.Views
{
    using Elements;

    public interface ITestPropertiesView : IView
    {
        event CommandHandler DisplayHiddenPropertiesChanged;

        bool Visible { get; set; }
        string Header { get; set; }
        IViewElement TestPanel { get; }
        IViewElement ResultPanel { get; }

        string TestType { get; set; }
        string FullName { get; set; }
        string Description { get; set; }
        string Categories { get; set; }
        string TestCount { get; set; }
        string RunState { get; set; }
        string SkipReason { get; set; }
        bool DisplayHiddenProperties { get; }
        string Properties { get; set; }
        string Outcome { get; set; }
        string ElapsedTime { get; set; }
        string AssertCount { get; set; }
        string Assertions { get; set; }
        string Output { get; set; }
    }
}
