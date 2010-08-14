//DatabaseScripter  Copyright (C) 2010  Philip Wood
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
using System.Text;
using log4net;

namespace Bluejam.Utils.DatabaseScripter.Services
{
    public class ConfigService
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(ConfigService));

        public string ManifestSchema
        {
            get
            {
                var manifestValidator = new Core.ManifestValidator();
                return manifestValidator.SchemaString;
            }
        }

        public string ConfigSchema
        {
            get
            {
                var configValidator = new Core.ConfigurationValidator();
                return configValidator.SchemaString;
            }
        }

        public Domain.ErrorCode Create(string[] args)
        {
            var errorCode = Domain.ErrorCode.Ok;

            try
            {
                Core.ConfigurationFactory.Create(args);
            }
            catch (Core.DatabaseScripterException ex)
            {
                log.Error("An error occurred. Check the debug information that follows.", ex);
                errorCode = ex.ErrorCode;
            }

            return errorCode;
        }
    }
}
