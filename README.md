docx4j-ImportXHTML.NET
======================

Converts XHTML to OpenXML WordML (docx) using docx4j.NET (licensed under ASLv2) and Flying Saucer. 

This project is basically https://github.com/plutext/docx4j-ImportXHTML IKVM'd, plus samples written in C#.

This project is licensed under LGPL v2.1 (or later), since that's the license used by Flying Saucer.

Users
You can install the NuGet package; see http://www.nuget.org/packages/docx4j.NET/

Samples

Installing the NuGet package will add a dir src to your project; in src/samples you will see sample code for:

docx to PDF
docx to HTML
interop with Open XML SDK
mail merge (MERGEFIELD processing)
content control data binding All of those should run out of the box (provided you have set: Project Properties > Startup object)
For examples of how to do other stuff with docx4j, please see https://github.com/plutext/docx4j/tree/master/src/samples Translating any of that code from Java to C# ought to be straightforward.

Developers
You can clone this project.

The easiest way to add the needed references is still via NuGet.