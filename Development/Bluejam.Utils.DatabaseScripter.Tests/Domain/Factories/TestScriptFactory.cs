using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Domain.Strategies
{
    [TestFixture]
    public class TestScriptFactory
    {
        [Test]
        public void WhenConfigurationParameterIsNull_CreateThrowsANullArgumentException()
        {
            var executionPlan = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Values.ExecutionPlanFactory.Development;
            var configInjector = new Moq.Mock<Bluejam.Utils.DatabaseScripter.Domain.Strategies.ConfigInjector>();
            try
            {
                var script = Bluejam.Utils.DatabaseScripter.Domain.Factories.ScriptFactory.Create(null, executionPlan, configInjector.Object);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("configuration"));
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void WhenExecutionPlanParameterIsNull_CreateThrowsANullArgumentException()
        {
            var configuration = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Values.ConfigurationFactory.Development;
            var configInjector = new Moq.Mock<Bluejam.Utils.DatabaseScripter.Domain.Strategies.ConfigInjector>();
            try
            {
                var script = Bluejam.Utils.DatabaseScripter.Domain.Factories.ScriptFactory.Create(configuration, null, configInjector.Object);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("executionPlan"));
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void WhenConfigInjectorParameterIsNull_CreateThrowsANullArgumentException()
        {
            var executionPlan = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Values.ExecutionPlanFactory.Development;
            var configuration = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Values.ConfigurationFactory.Development;
            try
            {
                var script = Bluejam.Utils.DatabaseScripter.Domain.Factories.ScriptFactory.Create(configuration, executionPlan, null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("configInjector"));
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}
