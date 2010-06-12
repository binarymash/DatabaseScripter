//DatabaseScripter  Copyright (C) 2010  Philip Wood
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
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
    public class TestScriptConfigManager
    {

        ScriptConfig _config;
        DatabaseScripterConfig _globalConfig;

        #region Setup/teardown

        [SetUp]
        public void Setup()
        {
            _config = new ScriptConfig();
            _config.Name = "myScript";
            _config.Properties = new Dictionary<string, string>();
            _config.Properties.Add("scriptkey1", "scriptvalue1");
            _config.Properties.Add("foo", "scriptbar");

            _globalConfig = DatabaseScripterConfig.Instance;
            _globalConfig.GlobalScriptProperties = new Dictionary<string, string>();
            _globalConfig.GlobalScriptProperties.Add("foo", "globalbar");
            _globalConfig.GlobalScriptProperties.Add("globalkey1", "globalvalue1");

        }

        #endregion

        #region Tests

        [Test]
        public void Test_GetConfig_WhenInScriptConfig()
        {            
            Assert.AreEqual("scriptvalue1", ScriptConfigManager.GetConfig(_config, "scriptkey1"));
        }

        [Test]
        public void Test_GetConfig_WhenInGlobalConfig()
        {
            Assert.AreEqual("globalvalue1", ScriptConfigManager.GetConfig(_config, "globalkey1"));
        }

        [Test]
        public void Test_GetConfig_WhenBothInScriptConfigAndGlobalConfig()
        {
            Assert.AreEqual("scriptbar", ScriptConfigManager.GetConfig(_config, "foo"));
        }

        [Test]
        public void Test_GetConfig_WhenInNeitherScriptConfigNorGlobalConfig()
        {
            try
            {
                ScriptConfigManager.GetConfig(_config, "other");
                Assert.Fail();
            }
            catch (DatabaseScripterException ex)
            {
                Assert.AreEqual(ErrorCode.CouldNotFindPropertyForScript, ex.ErrorCode);
            }
            catch
            {
                Assert.Fail();
            }
        }

        #endregion

    }
}
