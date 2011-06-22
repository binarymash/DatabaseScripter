using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Services.Interfaces
{
    public interface IScriptingService
    {

        IExecutionPlanResult GetExecutionPlan(Domain.Values.Configuration configuration);
        Domain.Interfaces.ErrorCode Execute(Domain.Values.Configuration configuration, Domain.Values.ExecutionPlan executionPlan);

    }
}
