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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace Bluejam.Utils.DatabaseScripter.Config
{
    public abstract class SchemaValidatorBase
    {
        private string resourceName;

        protected SchemaValidatorBase(string resourceName)
        {
            this.resourceName = resourceName;
        }

        public string SchemaString
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (var schemaResourceStream = assembly.GetManifestResourceStream(resourceName))
                {
                    var reader = new StreamReader(schemaResourceStream);
                    return reader.ReadToEnd();
                }
            }
        }

        public XmlSchema Schema
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                XmlSchema xmlSchema = null;
                using (var schemaResourceStream = assembly.GetManifestResourceStream(resourceName))
                {
                    var schemaStream = new StreamReader(schemaResourceStream);
                    var schemaReader = XmlReader.Create(schemaStream);
                    xmlSchema = XmlSchema.Read(schemaReader, null);
                }
                return xmlSchema;
            }
        }

    }
}
