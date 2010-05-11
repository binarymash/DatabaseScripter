using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace Bluejam.Utils.DatabaseScripter.Core
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
