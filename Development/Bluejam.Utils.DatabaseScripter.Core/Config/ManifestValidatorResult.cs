using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{
    public class ManifestValidatorResult : Result
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestValidatorResult"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="isValid">if set to <c>true</c> [is valid].</param>
        public ManifestValidatorResult(ErrorCode errorCode, bool isValid) : base(errorCode)
        {
            IsValid = isValid;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the manifest is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid { get; protected set; }
    }
}
