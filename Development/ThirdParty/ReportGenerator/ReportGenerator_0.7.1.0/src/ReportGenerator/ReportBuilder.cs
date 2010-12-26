﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using log4net;
using Palmmedia.ReportGenerator.Parser;

namespace Palmmedia.ReportGenerator
{
    /// <summary>
    /// Converts a report generated by PartCover or NCover into a readable HTML report.
    /// In contrast to the XSLT-Transformation included in PartCover, the report is more detailed.
    /// It does not only show the coverage quota, but also includes the source code and visualizes which line has been covered.
    /// </summary>
    public class ReportBuilder
    {
        /// <summary>
        /// The head of each generated HTML file.
        /// </summary>
        private const string HtmlStart = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">" +
            "<html><head><title>Coverage Report</title>" +
            "<meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\" />" + 
            "<style type=\"text/css\">" + 
            "html {font-family: sans-serif; margin: 20px; font-size: 0.9em; background-color: #f5f5f5;} " +
            "h1 {font-size: 1.2em; font-weight: bold; margin: 20px 0px 15px 0px; padding: 0px;} " +
            "h2 {font-size: 1.0em; font-weight: bold; margin: 10px 0px 15px 0px;padding: 0px;} " +
            "th {text-align: left;} " +
			"a {color: #cc0000; text-decoration: none;} " +
			"a:hover {color: #000000; text-decoration: none;} " +
            ".container {margin: auto; width: 960px; border: solid 1px #a7bac5; padding: 0px 20px 20px 20px; background-color: #ffffff;} " +
            ".overview { border: solid 1px #a7bac5; border-collapse: collapse;} " +
            ".overview th { border: solid 1px #a7bac5; border-collapse: collapse; padding: 2px 5px 2px 5px; background-color: #d2dbe1;} " +
            ".overview td { border: solid 1px #a7bac5; border-collapse: collapse; padding: 2px 5px 2px 5px;} " +
			".coverage { border: solid 1px #a7bac5; border-collapse: collapse; font-size: 5px;} " +
            ".coverage td { padding: 0px; } " +
            ".right {text-align: right; padding-right: 8px;} " +
			".light {color: #888888;} " +
			".green {background-color: #00ff21;} " +
			".red {background-color: #ff0000;} " +
			".gray {background-color: #dcdcdc;} " +
			".footer {font-size: 0.7em; text-align: center; margin-top: 35px;} " +
            "</style>" +
            "</head><body><div class=\"container\">";

        /// <summary>
        /// The donate button.
        /// </summary>
        private const string HtmlDontate = @"<br /><br /><form action=""https://www.paypal.com/cgi-bin/webscr"" method=""post"">
            <div>
            <input type=""hidden"" name=""cmd"" value=""_s-xclick"" />
            <input type=""image"" src=""https://www.paypal.com/en_US/i/btn/x-click-but04.gif"" name=""submit"" style=""border: 0px;"" alt=""Donate"" />
            <img alt="""" src=""https://www.paypal.com/de_DE/i/scr/pixel.gif"" width=""1"" height=""1"" />
            <input type=""hidden"" name=""encrypted"" value=""-----BEGIN PKCS7-----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-----END PKCS7-----"" />
            </div>
            </form>";

        /// <summary>
        /// The end of each generated HTML file.
        /// </summary>
        private const string HtmlEnd = "</div></body></html>";

		/// <summary>
		/// The logger.
		/// </summary>
		private static readonly ILog logger = LogManager.GetLogger(typeof(ReportBuilder));

        /// <summary>
        /// The parser to use.
        /// </summary>
        private IParser parser;

        /// <summary>
        /// The directory where the generated report should be saved.
        /// </summary>
        private string targetDirectory;
        
        /// <summary>
        /// List of all assemblies.
        /// </summary>
        private List<Assembly> assemblies = new List<Assembly>();

        /// <summary>
        /// Queue containing all classes that have to be processed.
        /// </summary>
        private Queue<Class> classQueue = new Queue<Class>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportBuilder"/> class.
        /// </summary>
        /// <param name="parser">The IParser to use.</param>
        /// <param name="targetDirectory">The directory where the generated report should be saved.</param>
        public ReportBuilder(IParser parser, string targetDirectory)
        {
            this.parser = parser;
            this.targetDirectory = targetDirectory;
        }

        /// <summary>
        /// Starts the generation of the report.
        /// </summary>
        public void CreateReport()
        {
            var assemblies = this.parser.Assemblies();

            foreach (var assemblyname in assemblies)
            {
                var assembly = new Assembly(assemblyname);
                var classes = this.parser.ClassesInAssembly(assemblyname);

                foreach (var classname in classes)
                {
                    var clazz = new Class(assemblyname, classname);
                    assembly.AddClass(clazz);
                    this.classQueue.Enqueue(clazz);
                }

                this.assemblies.Add(assembly);
            }

            this.ProcessClasses();

            this.CreateIndex();
        }

        /// <summary>
        /// Processes all classes in the queue.
        /// This is done in serveral threads depending on the number of CPUs.
        /// </summary>
        private void ProcessClasses()
        {
            var counter = 0;
            var total = this.classQueue.Count;

            logger.Info("Analyzing " + total + " classes");

            var workerThreads = new List<Thread>();

            // Create one thread per CPU
            for (int i = 1; i <= Environment.ProcessorCount; i++)
            {
                var thread = new Thread(delegate()
                {
                    var clazz = this.GetNextClass();
                    var workerCounter = 0;
                    while (clazz != null)
                    {
                        workerCounter++;
                        logger.Debug(" Creating report " + ++counter + "/" + total + " (Assembly: " + clazz.Assemblyname + ", Class: " + clazz.Name + ", " + Thread.CurrentThread.Name + ")");
                        this.CreateClassReport(clazz);
                        clazz = this.GetNextClass();
                    }

                    logger.Info(" " + Thread.CurrentThread.Name + " has finished (processed " + workerCounter + " classes)");
                });
                thread.Name = "Worker Thread " + i;
                workerThreads.Add(thread);
            }

            foreach (var thread in workerThreads)
            {
                thread.Start();
            }

            foreach (var thread in workerThreads)
            {
                thread.Join();
            }
        }

        /// <summary>
        /// Creates the index showing a overview of all assemblies and classes.
        /// </summary>
        private void CreateIndex()
        {
            logger.Info("Creating index");

            var coveredLines = this.assemblies.Sum(a => a.CoveredLines);
            var coverableLines = this.assemblies.Sum(a => a.CoverableLines);
            var coverage = (coverableLines == 0) ? 0 : System.Math.Round(100 * (decimal)coveredLines / (decimal)coverableLines, 1);

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(HtmlStart);

            stringBuilder.Append("<h1>Summary</h1>");

            stringBuilder.Append("<table class=\"overview\"><tr><th>Generated on:</th><td>");
            stringBuilder.Append(DateTime.Now.ToShortDateString());
            stringBuilder.Append(" - ");
            stringBuilder.Append(DateTime.Now.ToShortTimeString());
            stringBuilder.Append("</td></tr><tr><th>Parser:</th><td>");
            stringBuilder.Append(this.parser.ToString());
            stringBuilder.Append("</td></tr><tr><th>Assemblies:</th><td>");
            stringBuilder.Append(this.assemblies.Count());
            stringBuilder.Append("</td></tr><tr><th>Files:</th><td>");
            stringBuilder.Append(this.parser.Files().Count());
            stringBuilder.Append("</td></tr><tr><th>Coverage:</th><td>");
            stringBuilder.Append(coverage);
            stringBuilder.Append(" %</td></tr><tr><th>Covered lines:</th><td>");
            stringBuilder.Append(coveredLines);
            stringBuilder.Append("</td></tr><tr><th>Coverable lines:</th><td>");
            stringBuilder.Append(coverableLines);
            stringBuilder.Append("</td></tr><tr><th>Total lines:</th><td>");
            stringBuilder.Append(this.assemblies.Sum(a => a.TotalLines));
            stringBuilder.Append("</td></tr></table>");

            stringBuilder.Append("<h1>Assemblies</h1>");

            if (this.assemblies.Any())
            {
                stringBuilder.Append("<table class=\"overview\">");
                foreach (var assembly in this.assemblies)
                {
                    var assemblycoverage = assembly.CoverageQuota;

                    stringBuilder.Append("<tr><th>");
                    stringBuilder.Append(assembly.Name);
                    stringBuilder.Append("</th><th>");
                    stringBuilder.Append(assemblycoverage);
                    stringBuilder.Append(" %</th><th>");
                    stringBuilder.Append(CreateCoverageTable(assemblycoverage));
                    stringBuilder.Append("</th></tr>");

                    foreach (var clazz in assembly.Classes)
                    {
                        var classcoverage = clazz.CoverageQuota;

                        stringBuilder.Append("<tr><td><a href=\"");
                        stringBuilder.Append(ReplaceInvalidChars(clazz.Name));
                        stringBuilder.Append(".htm\">");
                        stringBuilder.Append(clazz.Name);
                        stringBuilder.Append("</a></td><td>");
                        stringBuilder.Append(classcoverage);
                        stringBuilder.Append(" %</td><td>");
                        stringBuilder.Append(CreateCoverageTable(classcoverage));
                        stringBuilder.Append("</td></tr>");
                    }
                }

                stringBuilder.Append("</table>");
            }
            else
            {
                stringBuilder.Append("<p>No assemblies have been covered.</p>");
            }

            stringBuilder.Append(CreateFooter());

            stringBuilder.Append(HtmlEnd);

            this.WriteToFile(stringBuilder.ToString(), "index.htm");
        }

        /// <summary>
        /// Creates the report for the given class.
        /// </summary>
        /// <param name="clazz">The class.</param>
        private void CreateClassReport(Class clazz)
        {
            var files = this.parser.FilesOfClass(clazz.Assemblyname, clazz.Name);

            var filesStringBuilder = new StringBuilder();

            // Files
            filesStringBuilder.Append("<h1>File(s)</h1>");
            foreach (var file in files)
            {
                filesStringBuilder.Append(this.AnalyzeFile(file, clazz));
            }

            if (!files.Any())
            {
                filesStringBuilder.Append("<p>No files found. This usually happens if a file isn't covered by a test or is not located in a separate file.</p>");
            }

            // Summary
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(HtmlStart);
            
            stringBuilder.Append("<h1>Summary</h1>");

            stringBuilder.Append("<table class=\"overview\"><tr><th>Assembly:</th><td>");
            stringBuilder.Append(clazz.Assemblyname);
            stringBuilder.Append("</td></tr><tr><th>Class:</th><td>");
            stringBuilder.Append(clazz.Name);
            stringBuilder.Append("</td></tr><tr><th valign=\"top\">File(s):</th><td>");

            foreach (var file in files)
            {
                stringBuilder.Append(file);
                stringBuilder.Append("<br />");
            }

            stringBuilder.Append("</td></tr><tr><th>Coverage:</th><td>");
            stringBuilder.Append(clazz.CoverageQuota);
            stringBuilder.Append(" %</td></tr><tr><th>Covered lines:</th><td>");
            stringBuilder.Append(clazz.CoveredLines);
            stringBuilder.Append("</td></tr><tr><th>Coverable lines:</th><td>");
            stringBuilder.Append(clazz.CoverableLines);
            stringBuilder.Append("</td></tr><tr><th>Total lines:</th><td>");
            stringBuilder.Append(clazz.TotalLines);
            stringBuilder.Append("</td></tr></table>");

            stringBuilder.Append(filesStringBuilder.ToString());            

			stringBuilder.Append(CreateFooter());

            stringBuilder.Append(HtmlEnd);

            this.WriteToFile(stringBuilder.ToString(), clazz.Name + ".htm");  
        }

        /// <summary>
        /// Analyses a file and generates a coverage report of the file.
        /// </summary>
		/// <param name="fileName">The name of the file.</param>
        /// <param name="clazz">The name of the assembly.</param>
        /// <returns>The generated coverage report of the given file.</returns>
        private string AnalyzeFile(string fileName, Class clazz)
        {
            if (!File.Exists(fileName))
            {
                logger.Error("File (" + fileName + ") does not exist (any) more: " + fileName);
                return "<p>File (" + fileName + ") does not exist (any) more: " + fileName + "</p>";
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("<h2>");
            stringBuilder.Append(fileName);
            stringBuilder.Append("</h2>");
            stringBuilder.Append("<table>");

            stringBuilder.Append("<tr><th></th><th class=\"right\">&nbsp;#</th><th class=\"right\">Line</th><th>Content</th></tr>");

            var currentLineNumber = 0;

            using (StreamReader reader = new StreamReader(fileName, System.Text.Encoding.UTF8))
            {
                string currentLine;
                try
                {                    
                    while ((currentLine = reader.ReadLine()) != null)
                    {
                        clazz.TotalLines++;
                        currentLine = currentLine.TrimEnd();

						if (currentLine.Length > 110)
						{
							currentLine = currentLine.Substring(0, 110);
						}

						currentLine = currentLine.Replace(" ", "&nbsp;") // replace ' '
							.Replace(((char)11).ToString(), "&nbsp;&nbsp;&nbsp;") // replace tab
							.Replace(((char)9).ToString(), "&nbsp;&nbsp;&nbsp;") // replace tab
							.Replace("<", "&lt;").Replace(">", "&gt;"); // replace '<' and '>'

                        var visits = this.parser.NumberOfLineVisits(clazz.Assemblyname, clazz.Name, fileName, ++currentLineNumber);

                        stringBuilder.Append("<tr><td class=\"");
                        if (visits == 0)
                        {
                            stringBuilder.Append("red");
                            clazz.CoverableLines++;
                        }
                        else if (visits > 0)
                        {
                            stringBuilder.Append("green");
                            clazz.CoveredLines++;
                            clazz.CoverableLines++;
                        }
                        else
                        {
                            stringBuilder.Append("gray");
                        }

                        stringBuilder.Append("\">&nbsp;");
                        stringBuilder.Append("</td>");
                        stringBuilder.Append("<td class=\"right\">");
                        if (visits >= 0) 
                        {
                            stringBuilder.Append(visits);
                        }

                        stringBuilder.Append("</td>");
                        stringBuilder.Append("<td class=\"right\"><code>");
                        stringBuilder.Append(currentLineNumber);
                        stringBuilder.Append("</code></td>");
                        if (visits == -1) 
                        {
                            stringBuilder.Append("<td class=\"light\">");
                        }
                        else 
                        {
                            stringBuilder.Append("<td>");
                        }

                        if (!string.IsNullOrEmpty(currentLine)) 
                        {
                            stringBuilder.Append("<code>");
                        }

						stringBuilder.Append(currentLine);
                        if (!string.IsNullOrEmpty(currentLine))
                        {
                            stringBuilder.Append("</code>");
                        }

                        stringBuilder.Append("</td></tr>");
                    }
                }
                catch (Exception ex)
                {
					logger.Error("Error during reading class (" + fileName + "): " + ex.Message);
                    return "<p>Error during reading class (" + fileName + "): " + ex.Message + "</p>";
                }
            }

            stringBuilder.Append("</table>");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns the next class in the queue or null queue is empty.
        /// </summary>
        /// <returns>The next class in the queue or null queue is empty.</returns>
        private Class GetNextClass()
        {
            lock (this.classQueue)
            {
                if (this.classQueue.Count > 0)
                {
                    return this.classQueue.Dequeue();
                }
            }

            return null;
        }

        /// <summary>
        /// Writes the given text to a file.
        /// </summary>
        /// <param name="content">The text to write.</param>
        /// <param name="filename">The name of the file.</param>
        private void WriteToFile(string content, string filename)
        {
            try
            {
                filename = Path.Combine(this.targetDirectory, ReplaceInvalidChars(filename));
                File.WriteAllText(filename, content, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                logger.Error("Report (" + filename + ") could not be saved: " + ex.Message);
            }
        }

		/// <summary>
		/// Replaces all invalid chars in a filename by '-'.
		/// </summary>
		/// <param name="fileName">The filename.</param>
		/// <returns>The valid filename.</returns>
		private static string ReplaceInvalidChars(string fileName)
		{
			foreach (var ch in Path.GetInvalidFileNameChars())
			{
				fileName = fileName.Replace(ch, '-');
			}

			return fileName;
		}

		/// <summary>
		/// Builds a table showing the coverage quota with red and green bars.
		/// </summary>
        /// <param name="coverage">The coverage quota.</param>
		/// <returns>Table showing the coverage quota with red and green bars.</returns>
		private static string CreateCoverageTable(decimal coverage)
		{
			var stringBuilder = new StringBuilder();
			int covered = (int)Math.Round(coverage, 0);
			int uncovered = 100 - covered;
            if (covered == 100)
            {
                covered = 103;
            }

            if (uncovered == 100)
            {
                uncovered = 103;
            }

			stringBuilder.Append("<table class=\"coverage\"><tr>");
            if (covered > 0)
            {
                stringBuilder.Append("<td class=\"green\" style=\"width: " + covered + "px;\">&nbsp;</td>");
            }

            if (uncovered > 0)
            {
                stringBuilder.Append("<td class=\"red\" style=\"width: " + uncovered + "px;\">&nbsp;</td>");
            }

			stringBuilder.Append("</tr></table>");
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Builds the footer.
		/// </summary>
		/// <returns>The footer.</returns>
		private static string CreateFooter()
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.Append("<div class=\"footer\">Generated by: ");
			stringBuilder.Append(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
			stringBuilder.Append(" ");
			stringBuilder.Append(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
            stringBuilder.Append("<br />");
            stringBuilder.Append(DateTime.Now.ToShortDateString());
            stringBuilder.Append(" - ");
            stringBuilder.Append(DateTime.Now.ToLongTimeString());
			stringBuilder.Append("<br /><a href=\"http://www.palmmedia.de\">www.palmmedia.de</a>");
            stringBuilder.Append(HtmlDontate);
			stringBuilder.Append("</div>");
			return stringBuilder.ToString();
		}
    }
}