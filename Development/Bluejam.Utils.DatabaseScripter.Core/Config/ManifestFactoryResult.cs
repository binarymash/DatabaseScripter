using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{
    public class ManifestFactoryResult : Result
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestFactoryResult"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="manifest">The manifest.</param>
        public ManifestFactoryResult(ErrorCode errorCode, Manifest manifest) : base(errorCode)
        {
            Manifest = manifest;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the manifest.
        /// </summary>
        /// <value>The manifest.</value>
        public Manifest Manifest { get; protected set; }

        #endregion


    }
}
