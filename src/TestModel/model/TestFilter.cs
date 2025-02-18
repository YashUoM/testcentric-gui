// ***********************************************************************
// Copyright (c) Charlie Poole and TestCentric GUI contributors.
// Licensed under the MIT License. See LICENSE file in root directory.
// ***********************************************************************

using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TestCentric.Gui.Model
{
    /// <summary>
    /// Utility class for working with TestFilters.
    /// </summary>
    public class TestFilter
    {
        public TestFilter(string xmlText)
        {
            XmlText = xmlText;
            Xml = XmlHelper.CreateXmlNode(xmlText);
            InnerXml = Xml.InnerXml;
            IsEmpty = string.IsNullOrEmpty(InnerXml);
        }

        public static TestFilter Empty { get; } = new TestFilter("<filter/>");

        public string XmlText { get; }

        public XmlNode Xml { get; }

        public string InnerXml { get; }

        public bool IsEmpty { get; }

        public NUnit.Engine.TestFilter AsNUnitFilter()
        {
            return IsEmpty
                ? NUnit.Engine.TestFilter.Empty
                : new NUnit.Engine.TestFilter(XmlText);
        }

        #region Static Methods to Create TestFilters

        public static TestFilter MakeIdFilter(TestNode test)
        {
            return new TestFilter($"<filter><id>{test.Id}</id></filter>");
        }

        public static TestFilter MakeIdFilter(IList<TestNode> testNodes)
        {
            if (testNodes.Count == 1)
                return MakeIdFilter(testNodes[0]);

            StringBuilder sb = new StringBuilder("<filter><or>");

            foreach (TestNode test in testNodes)
                sb.Append($"<id>{test.Id}</id>");

            sb.Append("</or></filter>");

            return new TestFilter(sb.ToString());
        }

        public static TestFilter MakeCategoryFilter(IList<string> categories)
        {
            var sb = new StringBuilder("<filter>");

            if (categories.Count > 1)
                sb.Append("<or>");

            foreach (string category in categories)
                sb.Append($"<cat>{category}</cat>");

            if (categories.Count > 1)
                sb.Append("</or>");

            sb.Append("</filter>");

            return new TestFilter(sb.ToString());
        }

        public static TestFilter MakeNotFilter(TestFilter filter)
        {
            return new TestFilter($"<filter><not>{filter.InnerXml}</not></filter>");
        }

        public static TestFilter MakeAndFilter(params TestFilter[] filters)
        {
            StringBuilder sb = new StringBuilder("<filter><and>");

            foreach (var filter in filters)
                sb.Append(filter.InnerXml);

            sb.Append("</and></filter>");

            return new TestFilter(sb.ToString());
        }

        #endregion
    }
}
