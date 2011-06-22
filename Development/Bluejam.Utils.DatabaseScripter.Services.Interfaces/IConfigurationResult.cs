using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Services.Interfaces
{
    public interface IConfigurationResult
    {
        Domain.Interfaces.ErrorCode ErrorCode { get; }
        Domain.Values.Configuration Configuration { get; }
    }
}
