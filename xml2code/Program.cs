using System;
using System.IO;
using System.Linq;
using Xml2CSharp;
using Xml2CSharp.ClassWriters;

//Console.WriteLine(args.First());

var codeWriter = new CSharpClassWriter(new System.Collections.Generic.Dictionary<string, object>
{
    ["UsePascalCase"] = true
});
var result = new Xml2CodeConverter(codeWriter).Convert(File.ReadAllText(args.First()), out var error);
if (error is not null)
{
    Console.Error.WriteLine(error);
    Environment.Exit(1);
}
Console.WriteLine(result);