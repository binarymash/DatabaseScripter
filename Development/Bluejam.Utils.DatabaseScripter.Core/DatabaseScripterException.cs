﻿//DatabaseScripter  Copyright (C) 2010  Philip Wood
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
using System.Linq;
using System.Security.Permissions;
using System.Text;

using Domain = Bluejam.Utils.DatabaseScripter.Domain;

namespace Bluejam.Utils.DatabaseScripter.Core
{
    [Serializable]
    public class DatabaseScripterException : Exception
    {

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public Domain.ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseScripterException"/> class.
        /// </summary>
        public DatabaseScripterException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseScripterException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public DatabaseScripterException(Domain.ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseScripterException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        public DatabaseScripterException(Domain.ErrorCode errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseScripterException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DatabaseScripterException(Domain.ErrorCode errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseScripterException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DatabaseScripterException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseScripterException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public DatabaseScripterException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseScripterException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        /// </exception>
        protected DatabaseScripterException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            ErrorCode = (Domain.ErrorCode)info.GetValue("ErrorCode", typeof(Domain.ErrorCode));
        }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic).
        /// </exception>
        /// <PermissionSet>
        /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/>
        /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter"/>
        /// </PermissionSet>
        [SecurityPermission(SecurityAction.LinkDemand,
        Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ErrorCode", ErrorCode); 
        }
    }
}
