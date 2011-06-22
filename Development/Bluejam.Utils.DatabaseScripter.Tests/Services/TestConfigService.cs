using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Services;
using Config = Bluejam.Utils.DatabaseScripter.Config;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Services
{
    [TestFixture]
    public class TestConfigService
    {

        ConfigService configService;
        Moq.Mock<Config.Interfaces.IConfigurationFactory> mockConfigurationFactory;
        Moq.Mock<Config.Interfaces.IManifestValidator> mockManifestValidator;
        Moq.Mock<Config.Interfaces.IConfigurationValidator> mockConfigValidator;
        Moq.Mock<Config.Interfaces.IEnvironmentConfigurationValidator> mockEnvironmentConfigValidator;

        [SetUp]
        public void SetUp()
        {
            mockConfigurationFactory = new Moq.Mock<Config.Interfaces.IConfigurationFactory>();
            mockManifestValidator = new Moq.Mock<Config.Interfaces.IManifestValidator>();
            mockConfigValidator = new Moq.Mock<Config.Interfaces.IConfigurationValidator>();
            mockEnvironmentConfigValidator = new Moq.Mock<Config.Interfaces.IEnvironmentConfigurationValidator>();

            configService = new ConfigService(mockConfigurationFactory.Object, 
                mockManifestValidator.Object, mockConfigValidator.Object, mockEnvironmentConfigValidator.Object);
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
        public void ManifestSchema_ReturnsTheManifestSchema()
        {
            var schema = Bluejam.Utils.DatabaseScripter.Test.Resources.EmbeddedResourceReader.ManifestSchema;
            mockManifestValidator.Setup(mock => mock.SchemaString).Returns(schema);
            Assert.That(configService.ManifestSchema, Is.EqualTo(schema));
        }

        [Test]
        public void ConfigSchema_ReturnsTheConfigSchema()
        {
            var schema = Bluejam.Utils.DatabaseScripter.Test.Resources.EmbeddedResourceReader.ConfigSchema;
            mockConfigValidator.Setup(mock => mock.SchemaString).Returns(schema);
            Assert.That(configService.ConfigSchema, Is.EqualTo(schema));
        }

        [Test]
        public void EnvironmentConfigSchema_ReturnsTheEnvironmentConfigSchema()
        {
            var schema = Bluejam.Utils.DatabaseScripter.Test.Resources.EmbeddedResourceReader.EnvironmentConfigSchema;
            mockEnvironmentConfigValidator.Setup(mock => mock.SchemaString).Returns(schema); 
            Assert.That(configService.EnvironmentConfigSchema, Is.EqualTo(Bluejam.Utils.DatabaseScripter.Test.Resources.EmbeddedResourceReader.EnvironmentConfigSchema));
        }
    
    }
}
