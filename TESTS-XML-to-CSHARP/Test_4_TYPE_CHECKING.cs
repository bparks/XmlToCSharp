
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
    public class Test_4_TYPE_CHECKING
    {

        [TestMethod]
        public void CheckBasicTypes()
        {
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_TYPE_CHECKING_CheckBasicTypes_INPUT.txt";
            string resultPath = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_TYPE_CHECKING_CheckBasicTypes_OUTPUT.txt";
            string input = File.ReadAllText(path);
            string errorMessage = string.Empty;
            string returnVal = new Xml2CodeConverter(new CSharpClassWriter(null)).Convert(input, out errorMessage);
            string resultsCompare = File.ReadAllText(resultPath);
            Assert.AreEqual(resultsCompare.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""), returnVal.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""));
        }

        [TestMethod]
        public void CheckBasicTypesWithSettings()
        {
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_TYPE_CHECKING_CheckBasicTypesWithSettings_INPUT.txt";
            string resultPath = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_TYPE_CHECKING_CheckBasicTypesWithSettings_OUTPUT.txt";
            string input = File.ReadAllText(path);
            string errorMessage = string.Empty;
            var settings = new Dictionary<string, object>();
            settings.Add("UsePascalCase", "true");
            string returnVal = new Xml2CodeConverter(new CSharpClassWriter(settings)).Convert(input, out errorMessage);
            string resultsCompare = File.ReadAllText(resultPath);
            Assert.AreEqual(resultsCompare.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""), returnVal.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""));
        }

        [TestMethod]
        public void CheckAttributes()
        {
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_TYPE_CHECKING_CheckAttributes_INPUT.txt";
            string resultPath = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_TYPE_CHECKING_CheckAttributes_OUTPUT.txt";
            string input = File.ReadAllText(path);
            string errorMessage = string.Empty;
            var settings = new Dictionary<string, object>();
            //settings.Add("UsePascalCase", "true");
            string returnVal = new Xml2CodeConverter(new CSharpClassWriter(settings)).Convert(input, out errorMessage);
            string resultsCompare = File.ReadAllText(resultPath);
            Assert.AreEqual(resultsCompare.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""), returnVal.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""));
        }

        [TestMethod]
        public void CheckAttributesWithPascal()
        {
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_TYPE_CHECKING_CheckAttributes_INPUT.txt";
            string resultPath = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_TYPE_CHECKING_CheckAttributesSettings_OUTPUT.txt";
            string input = File.ReadAllText(path);
            string errorMessage = string.Empty;
            var settings = new Dictionary<string, object>();
            settings.Add("UsePascalCase", "true");
            string returnVal = new Xml2CodeConverter(new CSharpClassWriter(settings)).Convert(input, out errorMessage);
            string resultsCompare = File.ReadAllText(resultPath);
            Assert.AreEqual(resultsCompare.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""), returnVal.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""));
        }

        [TestMethod]
        public void CheckClassesWithText()
        {
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_TYPE_CHECKING_CheckClassesWithText_INPUT.txt";
            string resultPath = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_TYPE_CHECKING_CheckClassesWithText_OUTPUT.txt";
            string input = File.ReadAllText(path);
            string errorMessage = string.Empty;
            var settings = new Dictionary<string, object>();
            settings.Add("UsePascalCase", "true");
            string returnVal = new Xml2CodeConverter(new CSharpClassWriter(settings)).Convert(input, out errorMessage);
            string resultsCompare = File.ReadAllText(resultPath);
            Assert.AreEqual(resultsCompare.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""), returnVal.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""));
        }


        [TestMethod]
        public void CheckArrays()
        {
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_TYPE_CHECKING_CheckArrays_INPUT.txt";
            string resultPath = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_TYPE_CHECKING_CheckArrays_OUTPUT.txt";
            string input = File.ReadAllText(path);
            string errorMessage = string.Empty;
            var settings = new Dictionary<string, object>();
            settings.Add("UsePascalCase", "true");
            string returnVal = new Xml2CodeConverter(new CSharpClassWriter(settings)).Convert(input, out errorMessage);
            string resultsCompare = File.ReadAllText(resultPath);
            Assert.AreEqual(resultsCompare.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""), returnVal.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""));
        }


        [TestMethod]
        public void CheckArrays2()
        {
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_TYPE_CHECKING_CheckArrays2_INPUT.txt";
            string resultPath = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_TYPE_CHECKING_CheckArrays2_OUTPUT.txt";
            string input = File.ReadAllText(path);
            string errorMessage = string.Empty;
            var settings = new Dictionary<string, object>();
            settings.Add("UsePascalCase", "true");
            string returnVal = new Xml2CodeConverter(new CSharpClassWriter(settings)).Convert(input, out errorMessage);
            string resultsCompare = File.ReadAllText(resultPath);
            Assert.AreEqual(resultsCompare.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""), returnVal.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""));
        }

        [TestMethod]
        public void Run_Nullable()
        {
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_TYPE_CHECKING_INPUT1.txt";
            string resultPath = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_TYPE_CHECKING_OUTPUT1.txt";
            string input = File.ReadAllText(path);
            string errorMessage = string.Empty;
            string returnVal = new Xml2CodeConverter(new CSharpClassWriter(null)).Convert(input, out errorMessage);
            string resultsCompare = File.ReadAllText(resultPath);
            Assert.AreEqual(resultsCompare.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""), returnVal.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""));
        }
    }
}
