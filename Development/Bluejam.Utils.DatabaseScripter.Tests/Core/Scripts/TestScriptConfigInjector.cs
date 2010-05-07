﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter;
using Bluejam.Utils.DatabaseScripter.Core;
using Bluejam.Utils.DatabaseScripter.Core.Config;
using Bluejam.Utils.DatabaseScripter.Core.Scripts;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests
{
    [TestFixture]
    public class TestScriptConfigInjector
    {

        ScriptConfig _config;

        #region Setup/teardown

        [SetUp]
        public void SetUp()
        {
            _config = new ScriptConfig();
            _config.Name = "myScript";
            _config.Properties = new Dictionary<string, string>();
            _config.Properties.Add("scriptkey1", "scriptvalue1");
            _config.Properties.Add("foo", "scriptbar");
            _config.Properties.Add("thing", "key1");

            var globalConfig = DatabaseScripterConfig.Instance;
            globalConfig.GlobalScriptProperties = new Dictionary<string, string>();
            globalConfig.GlobalScriptProperties.Add("foo", "globalbar");
            globalConfig.GlobalScriptProperties.Add("globalkey1", "globalvalue1");
        }

        #endregion


        [Test]
        public void Test_InjectConfig_WhenInScriptConfig()
        {
            var command = "This is a {scriptkey1} command";
            Assert.AreEqual("This is a scriptvalue1 command", ScriptConfigInjector.InjectConfig(command, _config));
        }

        [Test]
        public void Test_InjectConfig_WhenInGlobalConfig()
        {
            var command = "This is a {globalkey1} command";
            Assert.AreEqual("This is a globalvalue1 command", ScriptConfigInjector.InjectConfig(command, _config));
        }

        [Test]
        public void Test_InjectConfig_WhenInNeitherScriptConfigNorGlobalConfig()
        {
            var command = "This is a {blahblah} command";

            try
            {
                ScriptConfigInjector.InjectConfig(command, _config);
                Assert.Fail();
            }
            catch (DatabaseScripterException exception)
            {
                Assert.AreEqual(ErrorCode.CouldNotFindPropertyForScript, exception.ErrorCode);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Test_InjectConfig_WhenInBothScriptConfigAndGlobalConfig()
        {
            var command = "This is a {foo} command";
            Assert.AreEqual("This is a scriptbar command", ScriptConfigInjector.InjectConfig(command, _config));
        }

        [Test]
        public void Test_InjectConfig_WhenNested()
        {
            var command = "This is a {global{thing}} command";
            Assert.AreEqual("This is a globalvalue1 command", ScriptConfigInjector.InjectConfig(command, _config));
        }

    }
}
