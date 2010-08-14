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
using Bluejam.Utils.DatabaseScripter.Domain;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Core
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
