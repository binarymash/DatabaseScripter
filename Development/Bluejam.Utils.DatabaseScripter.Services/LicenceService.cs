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
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Services
{
    public class LicenceService
    {
        public string LicenceSplash
        {
            get
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "{1}DatabaseScripter version {0}  Copyright (C) 2011  Philip Wood {1}http://code.google.com/p/databasescripter/{1}{1}This program comes with ABSOLUTELY NO WARRANTY. This is free software, {1}and you are welcome to redistribute it under certain conditions. {1}{1}For more information, see License.txt.{1}{1}{1}",
                     Assembly.GetExecutingAssembly().GetName().Version.ToString(), 
                     System.Environment.NewLine);
            }
        }

    }
}
