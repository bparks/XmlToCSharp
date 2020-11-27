
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
    public class Test_2_SETTINGS
    {
        [TestMethod]
        public void Run_RemoveXMLAttributes()
        {
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_2_SETTINGS_INPUT.txt";
            string resultPath = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_2_SETTINGS_OUTPUT_RemoveXMLAttributes.txt";
            string input = File.ReadAllText(path);
            string errorMessage = string.Empty;

            Dictionary<string, object> settings = new Dictionary<string, object>();
            settings.Add("RemoveXMLAttributes", "true");

            string returnVal = new Xml2CodeConverter(new CSharpClassWriter(settings)).Convert(input, out errorMessage);

            string resultsCompare = File.ReadAllText(resultPath);

            Assert.AreEqual(resultsCompare.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""), returnVal.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""));
        }

        [TestMethod]
        public void Run_UsePascalCase()
        {
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_2_SETTINGS_INPUT.txt";
            string resultPath = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_2_SETTINGS_OUTPUT_UsePascalCase.txt";
            string input = File.ReadAllText(path);
            string errorMessage = string.Empty;

            Dictionary<string, object> settings = new Dictionary<string, object>();
            settings.Add("UsePascalCase", "true");
            string returnVal = new Xml2CodeConverter(new CSharpClassWriter(settings)).Convert(input, out errorMessage);

            string resultsCompare = File.ReadAllText(resultPath);

            Assert.AreEqual(resultsCompare.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""), returnVal.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""));
        }

        [TestMethod]
        public void Run_UseFields()
        {
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_2_SETTINGS_INPUT.txt";
            string resultPath = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_2_SETTINGS_OUTPUT_UseFields.txt";
            string input = File.ReadAllText(path);
            string errorMessage = string.Empty;

            Dictionary<string, object> settings = new Dictionary<string, object>();
            settings.Add("UseFields", "true");
            string returnVal = new Xml2CodeConverter(new CSharpClassWriter(settings)).Convert(input, out errorMessage);

            string resultsCompare = File.ReadAllText(resultPath);

            Assert.AreEqual(resultsCompare.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""), returnVal.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""));
        }

        [TestMethod]
        public void Run_AddNamespaceAttributes()
        {
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_2_SETTINGS_INPUT.txt";
            string resultPath = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_2_SETTINGS_OUTPUT_AddNamespaceAttributes.txt";
            string input = File.ReadAllText(path);
            string errorMessage = string.Empty;

            Dictionary<string, object> settings = new Dictionary<string, object>();
            settings.Add("AddNamespaceAttributes", "true");
            string returnVal = new Xml2CodeConverter(new CSharpClassWriter(settings)).Convert(input, out errorMessage);

            string resultsCompare = File.ReadAllText(resultPath);

            Assert.AreEqual(resultsCompare.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""), returnVal.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""));
        }
    }
}
