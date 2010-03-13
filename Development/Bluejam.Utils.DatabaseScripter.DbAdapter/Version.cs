using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Bluejam.Utils.DatabaseScripter.DbAdapter
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Version : IComparable<Version>
    {

        /// <summary>
        /// Gets or sets the major.
        /// </summary>
        /// <value>The major.</value>
        public int Major { get; set; }

        /// <summary>
        /// Gets or sets the minor.
        /// </summary>
        /// <value>The minor.</value>
        public int Minor { get; set; }

        /// <summary>
        /// Gets or sets the revision.
        /// </summary>
        /// <value>The revision.</value>
        public int Revision { get; set; }

        public Version()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Version"/> class.
        /// </summary>
        /// <param name="major">The major.</param>
        /// <param name="minor">The minor.</param>
        /// <param name="revision">The revision.</param>
        public Version(int major, int minor, int revision)
        {
            Major = major;
            Minor = minor;
            Revision = revision;
        }

        public Version(string version)
        {
            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentException("Null or empty version parameter");
            }

            var versionArray = version.Split('.');
            if (versionArray.Length != 3)
            {
                throw new ArgumentException();
            }

            int major;
            if (!int.TryParse(versionArray[0], out major))
            {
                throw new ArgumentException("Major is not an integer");
            }
            Major = major;

            int minor;
            if (!int.TryParse(versionArray[1], out minor))
            {
                throw new ArgumentException("Minor is not an integer");
            }
            Minor = minor;

            int revision;
            if (!int.TryParse(versionArray[2], out revision))
            {
                throw new ArgumentException("Revision is not an integer");
            }
            Revision = revision;
        }

        #region IComparable<Version> Members

        public int CompareTo(Version other)
        {
            if (this.Major != other.Major)
            {
                return this.Major.CompareTo(other.Major);
            }

            if (this.Minor != other.Minor)
            {
                return this.Minor.CompareTo(other.Minor);
            }

            return this.Revision.CompareTo(other.Revision);
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return this.Major + "." + this.Minor + "." + this.Revision;
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(Version v1, Version v2)
        {
            return (v1.CompareTo(v2) > 0);
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(Version v1, Version v2)
        {
            return (v1.CompareTo(v2) >= 0);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(Version v1, Version v2)
        {
            return (v1.CompareTo(v2) < 0);
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(Version v1, Version v2)
        {
            return (v1.CompareTo(v2) <= 0);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Version v1, Version v2)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(v1, v2))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)v1 == null) || ((object)v2 == null))
            {
                return false;
            }

            return (v1.CompareTo(v2) == 0);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Version v1, Version v2)
        {
            return !(v1 == v2);
        }

    }
}
