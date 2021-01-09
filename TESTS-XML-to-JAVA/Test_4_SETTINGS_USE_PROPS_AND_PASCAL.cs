
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xml2CSharp;
using Xml2CSharp.ClassWriters;

namespace TESTS_XML_to_JAVA
{

    [TestClass] 
  public class Test_4_SETTINGS_USE_PROPS_AND_PASCAL{
   
        [TestMethod]
        public void Run()
        { 
            string path = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "")  + @"Test_4_SETTINGS_USE_PROPS_AND_PASCAL_INPUT.txt";
            string resultPath =  Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp3.1", "") + @"Test_4_SETTINGS_USE_PROPS_AND_PASCAL_OUTPUT.txt";
            string input = File.ReadAllText(path);
            string errorMessage = string.Empty;

            Dictionary<string, object> settings = new Dictionary<string, object>();
            settings.Add("UseProperties", "true");
            settings.Add("UsePascalCase", "true");


            string returnVal = new Xml2CodeConverter(new JavaClassWriter(settings)).Convert(input, out errorMessage);
            string resultsCompare = File.ReadAllText(resultPath); 
            Assert.AreEqual(resultsCompare.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\r", "").Replace("\t", ""), returnVal.Replace(Environment.NewLine, "").Replace(" ", "").Replace("\r", "").Replace("\t", ""));
        }
    }
}
