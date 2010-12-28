using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Domain.Values;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Values
{
    public abstract class VersionFactory
    {
        public static Version v0_0_0_0
        {
            get
            {
                return new Version(0, 0, 0, 0);
            }
        }

        public static Version v0_0_0_1
        {
            get
            {
                return new Version(0, 0, 0, 1);
            }
        }

        public static Version v0_0_0_2
        {
            get
            {
                return new Version(0, 0, 0, 2);
            }
        }

        public static Version v1_0_0_0
        {
            get
            {
                return new Version(1,0,0,0);
            }
        }

        public static Version v1_0_0_1
        {
            get
            {
                return new Version(1, 0, 0, 1);
            }
        }

        public static Version v1_0_0_2
        {
            get
            {
                return new Version(1, 0, 0, 2);
            }
        }

    }
}
