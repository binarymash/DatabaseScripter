using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Core.Scripts;
using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests
{
    [TestFixture]
    public class TestScriptFactory
    {

        [Test]
        public void Test_Create_WhenNoScripts()
        {
            var scripts = ScriptFactory.Create();
            Assert.AreEqual(0, scripts.Count);
        }
    }
}
