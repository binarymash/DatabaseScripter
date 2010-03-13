using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.IO;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{
    public class DatabaseScripterSection : IConfigurationSectionHandler
    {

        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, XmlNode section)
        {            
            // Gets the child element names and attributes.
            foreach (XmlNode child in section.ChildNodes)
            {
                switch (child.Name)
                {
                    case "globalScriptProperties":
                        Configuration.Instance.GlobalScriptProperties = GetScriptProperties(child);
                        break;
                    case "scripts":
                        Configuration.Instance.Scripts = GetScripts(child);
                        break;
                    case "manifestPath":
                        Configuration.Instance.Manifest = GetManifest(child.InnerText);
                        break;
                }
            }

            return Configuration.Instance;
        }

        private string GetConnection(XmlNode node)
        {
            return ConfigurationManager.ConnectionStrings[node.InnerText].ConnectionString;
        }

        private List<ScriptConfig> GetScripts(XmlNode node)
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

        private Dictionary<string, string> GetScriptProperties(XmlNode node)
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

        private ScriptConfig GetScript(XmlNode node)
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

        private KeyValuePair<string, string> GetProperty(XmlNode node)
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

        private Manifest.Manifest GetManifest(string path)
        {
            if (!File.Exists(path))
            {
                //TODO: log missing manifest
                return null;
            }

            var xmlReader = XmlReader.Create(path);
            var xmlSerializer = new XmlSerializer(typeof(Manifest.Manifest));
            var manifest = (Manifest.Manifest)xmlSerializer.Deserialize(xmlReader);
            manifest.FilePath = path;
            //TODO: log bad xml

            return manifest;
        }

        private Version GetVersion(XmlNode node)
        {
            return new Version(node.InnerText);
        }

        #endregion

    }
}
