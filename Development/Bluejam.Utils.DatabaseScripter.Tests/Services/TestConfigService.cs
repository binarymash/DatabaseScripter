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
        public void WhenArgsParameterIsNull_CreateThrowsAnArgumentNullException()
        {
            try
            {
                var configurationResult = configService.Create(null);
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
        [Ignore("need to use IoC and mocks for this to work")]
        public void WhenNominal_CreateReturnsAConfigurationResult()
        {
            var args = new string[] { };
            var configurationResult = configService.Create(args);
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
