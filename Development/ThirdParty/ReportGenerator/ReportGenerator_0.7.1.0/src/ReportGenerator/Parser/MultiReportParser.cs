using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Palmmedia.ReportGenerator.Parser
{
    /// <summary>
    /// Parser that aggregates serveral parsers.
    /// </summary>
    public class MultiReportParser : IParser
	{
        /// <summary>
        /// The parsers to aggregate.
        /// </summary>
        private IEnumerable<IParser> parsers;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiReportParser"/> class.
        /// </summary>
        /// <param name="parsers">The parsers.</param>
        public MultiReportParser(IEnumerable<IParser> parsers)
        {
            this.parsers = parsers;
        }

        /// <summary>
        /// Determine all covered files.
        /// </summary>
        /// <returns>All covered files.</returns>
        public IEnumerable<string> Files()
        {
            return this.parsers.SelectMany(p => p.Files()).Distinct().OrderBy(p => p);
        }

        /// <summary>
        /// Determine all covered assemblies.
        /// </summary>
        /// <returns>All covered assemblies.</returns>
        public IEnumerable<string> Assemblies()
        {
            return this.parsers.SelectMany(p => p.Assemblies()).Distinct().OrderBy(p => p);
        }

        /// <summary>
        /// Determine all covered classes within an assembly.
        /// </summary>
        /// <param name="assemblyname">The name of the assembly.</param>
        /// <returns>All covered classes within an assembly.</returns>
        public IEnumerable<string> ClassesInAssembly(string assemblyname)
        {
            return this.parsers.SelectMany(p => p.ClassesInAssembly(assemblyname)).Distinct().OrderBy(p => p);
        }       

        /// <summary>
        /// Determine all files a class is defined in.
        /// </summary>
        /// <param name="assemblyname">The name of the assembly.</param>
        /// <param name="classname">The name of the class.</param>
        /// <returns>All files a class is defined in.</returns>
        public IEnumerable<string> FilesOfClass(string assemblyname, string classname)
        {
            return this.parsers.SelectMany(p => p.FilesOfClass(assemblyname, classname)).Distinct().OrderBy(p => p);
        }

        /// <summary>
        /// Determine how often a line of code has been covered.
        /// If line could not be covered at all -1 is returned.
        /// </summary>
        /// <param name="assemblyname">The name of the assembly.</param>
        /// <param name="classname">The name of the class.</param>
		/// <param name="fileName">The name of the file.</param>
        /// <param name="lineNumber">The number of the line (starting with 1, not zero based).</param>
        /// <returns>Number of visits.</returns>
		public int NumberOfLineVisits(string assemblyname, string classname, string fileName, int lineNumber)
        {
            var result = -1;

            foreach (var parser in this.parsers)
            {
                var parserResult = parser.NumberOfLineVisits(assemblyname, classname, fileName, lineNumber);

                if (parserResult >= 0)
                {
                    if (result == -1)
                    {
                        result = parserResult;
                    }
                    else
                    {
                        result += parserResult;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(this.GetType().Name);
            sb.Append(" (");

            int numberOfParsers = this.parsers.Count();
            for (int i = 0; i < numberOfParsers; i++)
            {
                sb.Append(this.parsers.ElementAt(i).ToString());
                if (i < numberOfParsers - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append(")");
            return sb.ToString();
        }
	}
}
