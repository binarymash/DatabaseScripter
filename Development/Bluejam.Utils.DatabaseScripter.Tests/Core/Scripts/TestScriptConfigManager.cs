using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Tests;
using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests
{
    [TestFixture]
    public class TestScriptConfigManager
    {

        Core.Config.ScriptConfig _config;
        Core.Config.DatabaseScripterConfig _globalConfig;

        #region Setup/teardown

        [SetUp]
        public void Setup()
        {
            _config = new Core.Config.ScriptConfig();
            _config.Name = "myScript";
            _config.Properties = new Dictionary<string, string>();
            _config.Properties.Add("scriptkey1", "scriptvalue1");
            _config.Properties.Add("foo", "scriptbar");

            _globalConfig = Core.Config.DatabaseScripterConfig.Instance;
            _globalConfig.GlobalScriptProperties = new Dictionary<string, string>();
            _globalConfig.GlobalScriptProperties.Add("foo", "globalbar");
            _globalConfig.GlobalScriptProperties.Add("globalkey1", "globalvalue1");

        }

        #endregion

        #region Tests

        [Test]
        public void Test_GetConfig_WhenInScriptConfig()
        {            
            Assert.AreEqual("scriptvalue1", Core.Scripts.ScriptConfigManager.GetConfig(_config, "scriptkey1"));
        }

        [Test]
        public void Test_GetConfig_WhenInGlobalConfig()
        {
            Assert.AreEqual("globalvalue1", Core.Scripts.ScriptConfigManager.GetConfig(_config, "globalkey1"));
        }

        [Test]
        public void Test_GetConfig_WhenBothInScriptConfigAndGlobalConfig()
        {
            Assert.AreEqual("scriptbar", Core.Scripts.ScriptConfigManager.GetConfig(_config, "foo"));
        }

        [Test]
        public void Test_GetConfig_WhenInNeitherScriptConfigNorGlobalConfig()
        {
            try
            {
                Core.Scripts.ScriptConfigManager.GetConfig(_config, "other");
                Assert.Fail();
            }
            catch (Core.DatabaseScripterException ex)
            {
                Assert.AreEqual(Core.ErrorCode.CouldNotFindPropertyForScript, ex.ErrorCode);
            }
            catch
            {
                Assert.Fail();
            }
        }

        #endregion

    }
}
