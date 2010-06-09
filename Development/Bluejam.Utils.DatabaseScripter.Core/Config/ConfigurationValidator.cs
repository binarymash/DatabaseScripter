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
using log4net;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{
    public class ConfigurationValidator : SchemaValidatorBase
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(ConfigurationValidator));
        private static object syncLock = new Object();
        private static bool isValid;

        public ConfigurationValidator() : base("Bluejam.Utils.DatabaseScripter.Core.ConfigSchema.xsd")
        {
        }

        public Result Validate(IXPathNavigable configSectionNode)
        {
            log.Debug("Validating the configuration");

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

                    if (isValid)
                    {
                        log.Debug("The configuration is valid");
                    }
                    else
                    {
                        log.Error("The configuration is invalid");
                    }
                }
                catch (XmlException ex)
                {
                    log.Error("An error occurred when validating the configuration", ex);
                    isValid = false;
                }

                return new Result(isValid ? ErrorCode.Ok : ErrorCode.InvalidConfig);
            }
        }

        private static void ValidationCallback(object sender, ValidationEventArgs e)
        {
            //TODO: store errors in collection on this.
            isValid = false;
            log.ErrorFormat(CultureInfo.InvariantCulture, "The configuration contains an error: {0}", e.Message);
        }

    }
}
