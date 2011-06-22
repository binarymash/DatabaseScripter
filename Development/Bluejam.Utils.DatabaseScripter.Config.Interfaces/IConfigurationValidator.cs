using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Config.Interfaces
{
    public interface IConfigurationValidator
    {
        Domain.Interfaces.ErrorCode Validate(System.Xml.XPath.IXPathNavigable configSectionNode);
        string SchemaString { get; }
        System.Xml.Schema.XmlSchema Schema { get; }
    }
}
