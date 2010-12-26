using System.Collections.Generic;
using System.Xml.Linq;

namespace Palmmedia.ReportGenerator.Parser
{
    /// <summary>
    /// Base class for some <see cref="IParser"/> implementations.
    /// </summary>
    public abstract class ParserBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParserBase"/> class.
        /// </summary>
        /// <param name="report">The report file as XContainer.</param>
        protected ParserBase(XContainer report)
        {
            this.Report = report;
            this.LineCoverageByFileDictionary = new Dictionary<string, int[]>();
        }

        /// <summary>
        /// Gets the report file as XContainer.
        /// </summary>
        protected XContainer Report { get; private set; }

        /// <summary>
        /// Gets the dictionary containing the line coverage information by file.
        /// </summary>
        protected Dictionary<string, int[]> LineCoverageByFileDictionary { get; private set; }
    }
}
