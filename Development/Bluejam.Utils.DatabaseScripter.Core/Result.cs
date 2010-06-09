using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Core
{

    /// <summary>
    /// The result of a method call
    /// </summary>
    public class Result
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public Result(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public ErrorCode ErrorCode { get; protected set; }

        #endregion

    }
}
