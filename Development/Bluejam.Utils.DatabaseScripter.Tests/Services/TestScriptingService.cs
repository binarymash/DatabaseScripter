using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Services
{
    [TestFixture]
    public class TestScriptingService
    {
        Bluejam.Utils.DatabaseScripter.Services.ScriptingService service;
        Moq.Mock<Bluejam.Utils.DatabaseScripter.Domain.Strategies.ConfigInjector> mockConfigInjector = new Moq.Mock<Bluejam.Utils.DatabaseScripter.Domain.Strategies.ConfigInjector>();

        [SetUp]
        public void SetUp()
        {
            service = new Bluejam.Utils.DatabaseScripter.Services.ScriptingService(mockConfigInjector.Object);
        }

        [Test]
        public void WhenConfigurationIsNull_ExecuteThrowsArgumentNullException()
        {
            try
            {
                service.Execute(null, Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Values.ExecutionPlanFactory.Development);
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
        public void WhenExecutionPlanIsNull_ExecuteThrowsArgumentNullException()
        {
            try
            {
                service.Execute(Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Values.ConfigurationFactory.Development, null);
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
        public void WhenConfigurationParameterIsNull_GetExecutionPlanThrowsAnArgumentNullException()
        {
            try
            {
                var configurationResult = service.GetExecutionPlan(null);
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

    
    }
}
