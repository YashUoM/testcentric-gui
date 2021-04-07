// ***********************************************************************
// Copyright (c) Charlie Poole and TestCentric GUI contributors.
// Licensed under the MIT License. See LICENSE file in root directory.
// ***********************************************************************

using System;

namespace TestCentric.Engine.Communication.Messages
{
#if !NETSTANDARD1_6
    [Serializable]
#endif
    public class ProgressMessage : TestEngineMessage
    {
        public ProgressMessage(string report)
        {
            Report = report;
        }

        public string Report { get; }
    }
}
