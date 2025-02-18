// ***********************************************************************
// Copyright (c) Charlie Poole and TestCentric GUI contributors.
// Licensed under the MIT License. See LICENSE file in root directory.
// ***********************************************************************

#if !NETCOREAPP
using System.IO;
using NUnit.Framework;

namespace TestCentric.Engine.Services.ResultWriters
{
    public class XmlTransformResultWriterTests : XmlOutputTest
    {
        [Test, Ignore("Need to create engine in a different way")]
        public void SummaryTransformTest()
        {
            var transformPath = GetLocalPath("TextSummary.xslt");
            StringWriter writer = new StringWriter();
            new XmlTransformResultWriter(new object[] { transformPath }).WriteResultFile(EngineResult.Xml, writer);

            string summary = string.Format(
                "Tests Run: {0}, Passed: {1}, Failed: {2}, Inconclusive: {3}, Skipped: {4}",
                EngineResult.Xml.Attributes["total"].Value,
                EngineResult.Xml.Attributes["passed"].Value,
                EngineResult.Xml.Attributes["failed"].Value,
                EngineResult.Xml.Attributes["inconclusive"].Value,
                EngineResult.Xml.Attributes["skipped"].Value);

            string output = writer.GetStringBuilder().ToString();

            Assert.That(output, Contains.Substring(summary));
        }

        [Test]
        public void XmlTransformResultWriterIgnoresDTDs()
        {
            var transformPath = GetLocalPath("TransformWithDTD.xslt");
            Assert.DoesNotThrow(() => new XmlTransformResultWriter(new object[] { transformPath }));
        }
    }
}
#endif
