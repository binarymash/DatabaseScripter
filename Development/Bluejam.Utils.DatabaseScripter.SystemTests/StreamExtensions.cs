using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.SystemTests
{
    /// <summary>
    /// Extensions to streams
    /// </summary>
    public static class StreamExtensions
    {

        /// <summary>
        /// Writes the source stream to the destination stream
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="destination">The destination stream.</param>
        public static void WriteTo(this System.IO.Stream source, System.IO.Stream destination)
        {
            if (source == null)
            {
                throw new System.ArgumentNullException("source", "Source is null");
            }

            if (destination == null)
            {
                throw new System.ArgumentNullException("destination", "Destination is null");
            }

            int readCount;
            var buffer = new byte[8192];
            while ((readCount = source.Read(buffer, 0, buffer.Length)) != 0)
            {
                destination.Write(buffer, 0, readCount);
            }
        }
    }
}
