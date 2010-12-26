using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Services
{
    [TestFixture]
    public class TestConfigService
    {

        Bluejam.Utils.DatabaseScripter.Services.ConfigService configService;

        [SetUp]
        public void SetUp()
        {
            configService = new Bluejam.Utils.DatabaseScripter.Services.ConfigService();
        }

        [Test]
        public void WhenArgsParameterIsNull_GetConfigurationThrowsAnArgumentNullException()
        {
            try
            {
                var configurationResult = configService.GetConfiguration(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("args"));
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
                var configurationResult = configService.GetExecutionPlan(null);
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
        public void ManifestSchema_ReturnsTheManifestSchema()
        {
            Assert.That(configService.ManifestSchema, Is.EqualTo(Bluejam.Utils.DatabaseScripter.Test.Resources.EmbeddedResourceReader.ManifestSchema));
        }

        [Test]
        public void ConfigSchema_ReturnsTheConfigSchema()
        {
            Assert.That(configService.ConfigSchema, Is.EqualTo(Bluejam.Utils.DatabaseScripter.Test.Resources.EmbeddedResourceReader.ConfigSchema));
        }

        [Test]
        public void EnvironmentConfigSchema_ReturnsTheEnvironmentConfigSchema()
        {
            Assert.That(configService.EnvironmentConfigSchema, Is.EqualTo(Bluejam.Utils.DatabaseScripter.Test.Resources.EmbeddedResourceReader.EnvironmentConfigSchema));
        }


    
    }
}
