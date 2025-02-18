// ***********************************************************************
// Copyright (c) Charlie Poole and TestCentric GUI contributors.
// Licensed under the MIT License. See LICENSE file in root directory.
// ***********************************************************************

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestCentric.Gui.Controls
{
    /// <summary>
    /// A special type of label which can display a tooltip-like
    /// window to show the full extent of any text which doesn't 
    /// fit. The window may be placed directly over the label
    /// or immediately beneath it and will expand to fit in
    /// a horizontal, vertical or both directions as needed.
    /// </summary>
    public class ExpandingLabel : Label
    {
        #region Instance Variables

        /// <summary>
        /// Our window for displaying expanded text
        /// </summary>
        private TipWindow tipWindow;

        /// <summary>
        /// Direction of expansion
        /// </summary>
        private TipWindow.ExpansionStyle expansion = TipWindow.ExpansionStyle.Horizontal;

        /// <summary>
        /// True if tipWindow may overlay the label
        /// </summary>
        private bool overlay = true;

        /// <summary>
        /// Time in milliseconds that the tip window
        /// will remain displayed.
        /// </summary>
        private int autoCloseDelay = 0;

        /// <summary>
        /// Time in milliseconds that the window stays
        /// open after the mouse leaves the control.
        /// </summary>
        private int mouseLeaveDelay = 300;

        /// <summary>
        /// If true, a context menu with Copy is displayed which
        /// allows copying contents to the clipboard.
        /// </summary>
        private bool copySupported = false;

        #endregion

        #region Properties

        [Browsable(false)]
        public bool IsExpanded
        {
            get { return tipWindow != null && tipWindow.Visible; }
        }

        [Category("Behavior"), DefaultValue(TipWindow.ExpansionStyle.Horizontal)]
        public TipWindow.ExpansionStyle Expansion
        {
            get { return expansion; }
            set { expansion = value; }
        }

        [Category("Behavior"), DefaultValue(true)]
        [Description("Indicates whether the tip window should overlay the label")]
        public bool Overlay
        {
            get { return overlay; }
            set { overlay = value; }
        }

        /// <summary>
        /// Time in milliseconds that the tip window
        /// will remain displayed.
        /// </summary>
        [Category("Behavior"), DefaultValue(0)]
        [Description("Time in milliseconds that the tip is displayed. Zero indicates no automatic timeout.")]
        public int AutoCloseDelay
        {
            get { return autoCloseDelay; }
            set { autoCloseDelay = value; }
        }

        /// <summary>
        /// Time in milliseconds that the window stays
        /// open after the mouse leaves the control.
        /// Reentering the control resets this.
        /// </summary>
        [Category("Behavior"), DefaultValue(300)]
        [Description("Time in milliseconds that the tip is displayed after the mouse levaes the control")]
        public int MouseLeaveDelay
        {
            get { return mouseLeaveDelay; }
            set { mouseLeaveDelay = value; }
        }

        [Category("Behavior"), DefaultValue(false)]
        [Description("If true, displays a context menu with Copy")]
        public bool CopySupported
        {
            get { return copySupported; }
            set
            {
                copySupported = value;
                if (copySupported)
                    base.ContextMenuStrip = null;
            }
        }

        /// <summary>
        /// Override Text property to set up copy menu if
        /// the value is non-empty.
        /// </summary>
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;

                if (copySupported)
                {
                    if (value == null || value == string.Empty)
                    {
                        if (this.ContextMenuStrip != null)
                        {
                            this.ContextMenuStrip.Dispose();
                            this.ContextMenuStrip = null;
                        }
                    }
                    else
                    {
                        this.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
                        ToolStripMenuItem copyMenuItem = new ToolStripMenuItem("Copy");
                        copyMenuItem.Click += new EventHandler(CopyToClipboard);
                        this.ContextMenuStrip.Items.Add(copyMenuItem);
                    }
                }
            }
        }

        #endregion

        #region Public Methods

        public void Expand()
        {
            if (!IsExpanded)
            {
                tipWindow = new TipWindow(this);
                tipWindow.Closed += new EventHandler(tipWindow_Closed);
                tipWindow.Expansion = this.Expansion;
                tipWindow.Overlay = this.Overlay;
                tipWindow.AutoCloseDelay = this.AutoCloseDelay;
                tipWindow.MouseLeaveDelay = this.MouseLeaveDelay;
                tipWindow.WantClicks = this.CopySupported;
                tipWindow.Show();
            }
        }

        public void Unexpand()
        {
            if (IsExpanded)
            {
                tipWindow.Close();
            }
        }

        #endregion

        #region Event Handlers

        private void tipWindow_Closed(object sender, EventArgs e)
        {
            tipWindow = null;
        }

        protected override void OnMouseHover(System.EventArgs e)
        {
            if (IsExpandable) Expand();
        }

        private bool IsExpandable
        {
            get
            {
                Graphics g = Graphics.FromHwnd(Handle);
                SizeF sizeNeeded = g.MeasureString(Text, Font);
                return
                    Width < (int)sizeNeeded.Width ||
                    Height < (int)sizeNeeded.Height;
            }
        }

        /// <summary>
        /// Copy contents to clipboard
        /// </summary>
        private void CopyToClipboard(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.Text);
        }

        #endregion
    }
}
