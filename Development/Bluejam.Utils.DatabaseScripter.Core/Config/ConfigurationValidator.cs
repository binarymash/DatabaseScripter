using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{
    public class ConfigurationValidator : SchemaValidatorBase
    {
        private static object syncLock = new Object();
        private static bool isValid;

        public ConfigurationValidator() : base("Bluejam.Utils.DatabaseScripter.Core.ConfigSchema.xsd")
        {
        }

        public bool IsValid(IXPathNavigable configSectionNode)
        {
            lock (syncLock)
            {
                try
                {
                    isValid = true;

                    //TODO: is there a better way to do this?
                    var navigator = configSectionNode.CreateNavigator();
                    var doc = new XmlDocument();
                    doc.LoadXml(navigator.OuterXml);
                    doc.Schemas.Add(Schema);
                    doc.Validate(ValidationCallback);

                    return isValid;
                }
                catch (XmlException ex)
                {
                    //TODO: log xml exception
                    throw new DatabaseScripterException(ErrorCode.InvalidManifest, "An exception occurred when validating the manifest schema.", ex);
                }
            }
        }

        private static void ValidationCallback(object sender, ValidationEventArgs e)
        {
            //TODO: log validation errors
            //TODO: store errors in collection on this.
            isValid = false;
            Console.WriteLine("Validation Error: {0}", e.Message);
        }

    }
}
