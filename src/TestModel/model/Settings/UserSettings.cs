// ***********************************************************************
// Copyright (c) Charlie Poole and TestCentric GUI contributors.
// Licensed under the MIT License. See LICENSE file in root directory.
// ***********************************************************************

namespace TestCentric.Gui.Model.Settings
{
    public class UserSettings : SettingsGroup
    {
        public UserSettings(ISettings settings, string prefix=null)
            : base(settings, prefix) { }

        public GuiSettings Gui
        {
            get { return new GuiSettings(_settingsService, GroupPrefix); }
        }

        public EngineSettings Engine
        {
            get { return new EngineSettings(_settingsService, GroupPrefix); }
        }
    }
}
