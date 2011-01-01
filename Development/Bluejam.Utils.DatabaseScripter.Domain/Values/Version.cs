//DatabaseScripter  Copyright (C) 2011  Philip Wood
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Bluejam.Utils.DatabaseScripter.Domain.Values
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Version : Core.DomainModel.ValidatableValueObject
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
        /// Gets or sets the build.
        /// </summary>
        /// <value>The build.</value>
        public int Build { get; set; }

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
        /// <param name="build">The build.</param>
        /// <param name="revision">The revision.</param>
        public Version(int major, int minor, int build, int revision)
        {
            Major = major;
            Minor = minor;
            Build = build;
            Revision = revision;
        }

        public Version(string version)
        {
            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentException("Null or empty version parameter", "version");
            }

            var versionArray = version.Split('.');
            if (versionArray.Length != 4)
            {
                throw new ArgumentException("Format is not n.n.n.n", "version");
            }

            int major;
            if (!int.TryParse(versionArray[0], out major))
            {
                throw new ArgumentException("Major is not an integer", "version");
            }
            Major = major;

            int minor;
            if (!int.TryParse(versionArray[1], out minor))
            {
                throw new ArgumentException("Minor is not an integer", "version");
            }
            Minor = minor;

            int build;
            if (!int.TryParse(versionArray[2], out build))
            {
                throw new ArgumentException("Build is not an integer", "version");
            }
            Build = build;

            int revision;
            if (!int.TryParse(versionArray[3], out revision))
            {
                throw new ArgumentException("Revision is not an integer", "version");
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

            if (this.Build != other.Build)
            {
                return this.Build.CompareTo(other.Build);
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
            return this.Major + "." + this.Minor + "." + this.Build + "." + this.Revision;
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="version1">The version1.</param>
        /// <param name="version2">The version2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(Version version1, Version version2)
        {
            return (version1.CompareTo(version2) > 0);
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="version1">The version1.</param>
        /// <param name="version2">The version2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(Version version1, Version version2)
        {
            return (version1.CompareTo(version2) >= 0);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="version1">The version1.</param>
        /// <param name="version2">The version2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(Version version1, Version version2)
        {
            return (version1.CompareTo(version2) < 0);
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="version1">The version1.</param>
        /// <param name="version2">The version2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(Version version1, Version version2)
        {
            return (version1.CompareTo(version2) <= 0);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="version1">The version1.</param>
        /// <param name="version2">The version2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Version version1, Version version2)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(version1, version2))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)version1 == null) || ((object)version2 == null))
            {
                return false;
            }

            return (version1.CompareTo(version2) == 0);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="version1">The version1.</param>
        /// <param name="version2">The version2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Version version1, Version version2)
        {
            return !(version1 == version2);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            return (this == (Version)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {

            return base.GetHashCode();
        }
    }
}
