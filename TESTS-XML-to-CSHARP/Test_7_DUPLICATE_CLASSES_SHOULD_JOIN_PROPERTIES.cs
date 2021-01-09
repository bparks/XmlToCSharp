
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xml2CSharp;
using Xml2CSharp.ClassWriters;

namespace TESTS_XML_to_CSHARP
{

    [TestClass]
    public class Test_7_DUPLICATE_CLASSES_SHOULD_JOIN_PROPERTIES
    {

        [TestMethod]
        public void Run()
        {
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_7_DUPLICATE_CLASSES_SHOULD_JOIN_PROPERTIES_INPUT.txt";
            string resultPath = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_7_DUPLICATE_CLASSES_SHOULD_JOIN_PROPERTIES_OUTPUT.txt";
            string input = File.ReadAllText(path);
            string errorMessage = string.Empty;
            string returnVal = new Xml2CodeConverter(new CSharpClassWriter(null)).Convert(input, out errorMessage);
            string resultsCompare = File.ReadAllText(resultPath);
            Assert.AreEqual(resultsCompare.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""), returnVal.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""));
        }
    }
}
