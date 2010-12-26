using System;
using System.IO;
using System.Linq;
using log4net;
using Palmmedia.ReportGenerator.Parser;

namespace Palmmedia.ReportGenerator
{
    /// <summary>
    /// Command line access to the ReportBuilder.
    /// </summary>
    internal class Program
    {
		/// <summary>
		/// The logger.
		/// </summary>
		private static readonly ILog logger = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// The main method.
        /// </summary>
        /// <param name="args">The arguments.</param>
        internal static void Main(string[] args)
        {
			log4net.Config.XmlConfigurator.Configure();

            if (args.Length != 2)
            {
                ShowHelp();
                return;
            }

            var reportFiles = args[0].Split(';');
            var targetDirectory = args[1];

            // Check whether report exists
            if (!reportFiles.Any())
            {
				logger.Error("No report files specified.");
                return;
            }

            foreach (var file in reportFiles)
            {
                if (!File.Exists(file))
                {
					logger.Error(string.Format("The report file '{0}' does not exist.", file));
                    return;
                }
            }

            // Create target directory
            if (!Directory.Exists(targetDirectory))
            {
                try
                {
                    Directory.CreateDirectory(targetDirectory);
                }
                catch (Exception ex)
                {
					logger.Error(string.Format("The target directory '{0}' could not be created: {1}", targetDirectory, ex.Message));
                    return;
                }                
            }

            var stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();

            // Initiate parser
            var parser = ParserFactory.CreateParser(reportFiles);

            if (parser == null)
            {
				logger.Error("No matching parser found.");
                stopWatch.Stop();
                return;
            }

            new ReportBuilder(parser, targetDirectory).CreateReport();

            stopWatch.Stop();
            logger.Info(string.Format("Report generation took {0} seconds", stopWatch.ElapsedMilliseconds / 1000));
        }

        /// <summary>
        /// Shows the help of the programm.
        /// </summary>
        private static void ShowHelp()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine("Parameters:");
            Console.WriteLine("    ReportFile(s) TargetDirectory");

            Console.WriteLine(string.Empty);
            Console.WriteLine("Explanations:");
            Console.WriteLine("   ReportFile(s): The reports that should be parsed (separated by semicolon)");
            Console.WriteLine("   TargetDirectory: The directory where the HTMl report should be saved");

            Console.WriteLine(string.Empty);
            Console.WriteLine("Examples:");
            Console.WriteLine("   \"Partcover.xml\" \"C:\\report\"");
            Console.WriteLine("   \"Partcover1.xml;PartCover2.xml\" \"report\"");
        }
    }
}
