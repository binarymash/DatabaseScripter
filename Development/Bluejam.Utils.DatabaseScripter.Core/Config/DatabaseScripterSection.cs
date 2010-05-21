using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using log4net;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{
    public class DatabaseScripterSection : IConfigurationSectionHandler
    {


        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, XmlNode section)
        {
            var configurationValidator = new ConfigurationValidator();
            if (!configurationValidator.IsValid(section))
            {
                log.Error("The configuration contains errors.");
                throw new DatabaseScripterException(ErrorCode.InvalidConfig, "The configuration contains errors.");
            }

            // Gets the child element names and attributes.
            foreach (XmlNode child in section.ChildNodes)
            {
                switch (child.Name)
                {
                    case "preview":
                        DatabaseScripterConfig.Instance.Preview = Boolean.Parse(child.InnerText);
                        break;
                    case "globalScriptProperties":
                        DatabaseScripterConfig.Instance.GlobalScriptProperties = GetScriptProperties(child);
                        break;
                    case "scripts":
                        DatabaseScripterConfig.Instance.Scripts = GetScripts(child);
                        break;
                    case "manifestPath":
                        DatabaseScripterConfig.Instance.Manifest = ManifestFactory.Create(child.InnerText);
                        break;
                }
            }

            return DatabaseScripterConfig.Instance;
        }

        #endregion

        #region Non-public 

        private static readonly ILog log = LogManager.GetLogger(typeof(DatabaseScripterSection));

        private static List<ScriptConfig> GetScripts(XmlNode node)
        {
            var scripts = new List<ScriptConfig>();

            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {                    
                    case "script":
                        scripts.Add(GetScript(child));
                        break;
                }
            }

            return scripts;
        }

        private static Dictionary<string, string> GetScriptProperties(XmlNode node)
        {
            var properties = new Dictionary<string, string>();

            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "property":
                        var property = GetProperty(child);
                        properties.Add(property.Key, property.Value);
                        break;
                }
            }

            return properties;
        }

        private static ScriptConfig GetScript(XmlNode node)
        {
            var script = new ScriptConfig();
            foreach (XmlAttribute attribute in node.Attributes)
            {
                switch (attribute.Name)
                {
                    case "name":
                        script.Name = attribute.Value;
                        break;
                }
            }
            foreach(XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {
                    case "properties":
                        script.Properties = GetScriptProperties(child);
                        break;
                }
            }

            return script;
        }

        private static KeyValuePair<string, string> GetProperty(XmlNode node)
        {
            string name = null;
            string value = null;

            foreach (XmlAttribute attribute in node.Attributes)
            {                
                switch (attribute.Name)
                {
                    case "name":
                        name = attribute.Value;
                        break;
                }
            }

            value = node.InnerText;

            return new KeyValuePair<string, string>(name, value);
        }

        #endregion

    }
}
