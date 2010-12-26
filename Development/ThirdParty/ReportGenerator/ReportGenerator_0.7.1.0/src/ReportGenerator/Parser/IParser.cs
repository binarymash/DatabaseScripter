using System.Collections.Generic;

namespace Palmmedia.ReportGenerator.Parser
{
    /// <summary>
    /// Interface for different parsers.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Determine all covered files.
        /// </summary>
        /// <returns>All covered files.</returns>
        IEnumerable<string> Files();

        /// <summary>
        /// Determine all covered assemblies.
        /// </summary>
        /// <returns>All covered assemblies.</returns>
        IEnumerable<string> Assemblies();

        /// <summary>
        /// Determine all covered classes within an assembly.
        /// </summary>
        /// <param name="assemblyname">The name of the assembly.</param>
        /// <returns>All covered classes within an assembly.</returns>
        IEnumerable<string> ClassesInAssembly(string assemblyname);

        /// <summary>
        /// Determine all files a class is defined in.
        /// </summary>
        /// <param name="assemblyname">The name of the assembly.</param>
        /// <param name="classname">The name of the class.</param>
        /// <returns>All files a class is defined in.</returns>
        IEnumerable<string> FilesOfClass(string assemblyname, string classname);

        /// <summary>
        /// Determine how often a line of code has been covered.
        /// If line could not be covered at all -1 is returned.
        /// </summary>
        /// <param name="assemblyname">The name of the assembly.</param>
        /// <param name="classname">The name of the class.</param>
		/// <param name="fileName">The name of the file.</param>
        /// <param name="lineNumber">The number of the line (starting with 1, not zero based).</param>
        /// <returns>Number of visits.</returns>
		int NumberOfLineVisits(string assemblyname, string classname, string fileName, int lineNumber);
    }
}
