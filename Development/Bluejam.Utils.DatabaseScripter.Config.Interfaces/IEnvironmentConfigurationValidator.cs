using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Config.Interfaces
{
    public interface IEnvironmentConfigurationValidator
    {
        Result Validate(string environmentConfigurationFilePath);
        string SchemaString { get; }
        System.Xml.Schema.XmlSchema Schema { get; }
    }
}
