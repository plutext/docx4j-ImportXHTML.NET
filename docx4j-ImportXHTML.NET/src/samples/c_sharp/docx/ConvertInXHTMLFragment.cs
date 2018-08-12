using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using org.docx4j;
using org.docx4j.convert.@in.xhtml;
using org.docx4j.openpackaging.packages;

using System.Collections.Specialized;
using Common.Logging;

namespace docx4jImportXHTML.NET.samples.docx
{

    /// <summary>
    /// Demonstrates using docx4j-ImportXHTML to import a string of XHTML as Word docx content.
    /// 
    /// See https://github.com/plutext/docx4j-ImportXHTML/tree/master/src/samples/java/org/docx4j/samples
    /// for converting from file, URL etc.  These are written in Java, but conversion to C# is straightforward.
    /// 
    /// If you are trying this in Visual Studio, it'll be faster if you "start without debugging" (Ctrl+F5)
    /// And first, remember to set this as the "startup object" in project properties.
    /// 
    /// </summary>
    class ConvertInXHTMLFragment
    {
        static void Main(string[] args)
        {
            ILog log = configureLogging();
            log.Info("Hello from Common Logging");

            string projectDir = System.IO.Directory.GetParent(
                System.IO.Directory.GetParent(
                Environment.CurrentDirectory.ToString()).ToString()).ToString() + "\\";

            // resulting docx
            String OUTPUT_DOCX = projectDir + @"OUT_XHTMLFragment.docx";


            // Programmatically configure Common Logging
            // (alternatively, you could do it declaratively in app.config)
            NameValueCollection commonLoggingproperties = new NameValueCollection();
            commonLoggingproperties["showDateTime"] = "false";
            commonLoggingproperties["level"] = "INFO";
            LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter(commonLoggingproperties);

            ikvm.runtime.Startup.addBootClassPathAssembly(
                System.Reflection.Assembly.GetAssembly(
                    typeof(org.docx4j.convert.@in.xhtml.XHTMLImporterImpl)));

            // Configure to find docx4j.properties
            // .. add as URL the dir containing docx4j.properties (not the file itself!)
            // and docx4j-ImportXHTML.properties (assumed to be in the same dir)
            Plutext.PropertiesConfigurator.setDocx4jPropertiesDir(projectDir + @"src\samples\resources\");
            // Workaround  to prevent ClassNotFoundException, 
            // at IKVM.NativeCode.java.lang.Class.forName0 
            // caused by Class.forName("org.docx4j.convert.in.xhtml.FSColorToHexString")
            // in docx4j code.
            ikvm.runtime.Startup.addBootClassPathAssembly(
                System.Reflection.Assembly.GetAssembly(
                    typeof(org.docx4j.convert.@in.xhtml.FSColorToHexString)));

            String xhtml = "<ul>" +
                "<li>Outer 1 </li>" +
                 "<li>Outer 2 </li>" +
                  "<ul>" +
                   "<li>Inner 1 </li>" +
                    "<li>Inner 2 </li>" +
                    "</ul>" +
                 "<li>Outer 3 </li>" +
                "</ul>";

            WordprocessingMLPackage wordMLPackage = WordprocessingMLPackage.createPackage();

            XHTMLImporterImpl XHTMLImporter = new XHTMLImporterImpl(wordMLPackage);

            wordMLPackage.getMainDocumentPart().getContent().addAll(
                    XHTMLImporter.convert(xhtml, null));

            Console.WriteLine(
                    org.docx4j.XmlUtils.marshaltoString(wordMLPackage.getMainDocumentPart().getJaxbElement(), true, true));

            //Save the document 
            Docx4J.save(wordMLPackage, new java.io.File(OUTPUT_DOCX), Docx4J.FLAG_NONE);
            log.Info("Saved: " + OUTPUT_DOCX);
      
	
        }

        public static ILog configureLogging()
        {
            //ikvm.runtime.Startup.addBootClassPathAssembly(
            //    System.Reflection.Assembly.GetAssembly(
            //        typeof(com.plutext.slf4jNetCommonsLogging.NetCommonsLoggerFactory)));

            //ikvm.runtime.Startup.addBootClassPathAssembly(
            //    System.Reflection.Assembly.GetAssembly(
            //        typeof(org.slf4j.impl.StaticLoggerBinder)));

            ikvm.runtime.Startup.addBootClassPathAssembly(
                System.Reflection.Assembly.GetAssembly(
                    typeof(org.slf4j.LoggerFactory)));

            

            NameValueCollection commonLoggingproperties = new NameValueCollection();
            commonLoggingproperties["showDateTime"] = "false";
            commonLoggingproperties["level"] = "INFO";

            Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter(commonLoggingproperties);
            // In VS 2010 for output type console application, that shows up in a new console window,
            // whether you start with debugging, or without.

            //Common.Logging.LogManager.Adapter = new Common.Logging.Simple.TraceLoggerFactoryAdapter(commonLoggingproperties);
            // In VS 2010 for output type console application, that shows up in a "show output from debugging",
            // provided you are debugging!

            // In a real application, you might route Common.Logging to NLog
            // Common.Logging.LogManager.Adapter = new Common.Logging.NLog.NLogLoggerFactoryAdapter(commonLoggingproperties);

            return Common.Logging.LogManager.GetCurrentClassLogger();

        }
    }
}
