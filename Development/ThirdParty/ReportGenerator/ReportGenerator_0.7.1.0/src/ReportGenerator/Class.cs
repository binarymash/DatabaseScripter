using System.Collections.Generic;

namespace Palmmedia.ReportGenerator
{
    /// <summary>
    /// Represents a class.
    /// </summary>
    internal class Class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Class"/> class.
        /// </summary>
        /// <param name="assemblyname">The name of the assembly.</param>
        /// <param name="name">The name of the class.</param>
        public Class(string assemblyname, string name)
        {
            this.Assemblyname = assemblyname;
            this.Name = name;
        }

        /// <summary>
        /// Gets the name of the assembly.
        /// </summary>
        public string Assemblyname { get; private set; }

        /// <summary>
        /// Gets the name of the class.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the number of covered lines.
        /// </summary>
        public int CoveredLines { get; set; }

        /// <summary>
        /// Gets or sets the number of coverable lines.
        /// </summary>
        public int CoverableLines { get; set; }

        /// <summary>
        /// Gets or sets the number of total lines.
        /// </summary>
        public int TotalLines { get; set; }

        /// <summary>
        /// Gets the coverage quota of the class.
        /// </summary>
        public decimal CoverageQuota
        {
            get
            {
                return (this.CoverableLines == 0) ? 0 : System.Math.Round(100 * (decimal)this.CoveredLines / (decimal)this.CoverableLines, 1);
            }
        }

        /// <summary>
        /// Determines whether the given object is equal.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if equal otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !obj.GetType().Equals(typeof(Class)))
            {
                return false;
            }
            else
            {
                var clazz = (Class)obj;
                return clazz.Assemblyname.Equals(this.Assemblyname) && clazz.Name.Equals(this.Name);
            }
        }

        /// <summary>
        /// Returns the HashCode
        /// </summary>
        /// <returns>The HashCode</returns>
        public override int GetHashCode()
        {
            return this.Assemblyname.GetHashCode() + this.Name.GetHashCode();
        }
    }
}
