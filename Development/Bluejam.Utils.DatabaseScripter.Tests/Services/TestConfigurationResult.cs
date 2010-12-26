using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Services
{
    [TestFixture]
    public class TestConfigurationResult
    {
        [Test]
        public void TestConstructor()
        {
            var errorCode = Bluejam.Utils.DatabaseScripter.Domain.ErrorCode.Ok;
            var configuration = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Values.ConfigurationFactory.Development;
            //var executionPlan = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Values.ExecutionPlanFactory.Development;
            //var configurationResult = new Bluejam.Utils.DatabaseScripter.Services.ConfigurationResult(errorCode, configuration, executionPlan);
            var configurationResult = new Bluejam.Utils.DatabaseScripter.Services.ConfigurationResult(errorCode, configuration);

            Assert.That(configurationResult.ErrorCode, Is.EqualTo(errorCode));
            Assert.That(configurationResult.Configuration, Is.EqualTo(configuration));
            //Assert.That(configurationResult.ExecutionPlan, Is.EqualTo(executionPlan));

        }

    }
}
