//DatabaseScripter  Copyright (C) 2011  Philip Wood
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
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Bluejam.Utils.DatabaseScripter.Test.Resources;
using Bluejam.Utils.DatabaseScripter.Domain.Entities;
using Bluejam.Utils.DatabaseScripter.Domain.Values;

using Factories = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Domain.Values
{

    [TestFixture]
    public class TestManifest
    {

        [Test]
        public void Test_ManifestSerialization()
        {
            //setup
            var manifest = Factories.Domain.Values.ManifestFactory.Basic;
            var xmlSerializer = new XmlSerializer(typeof(Manifest));
            var stringWriter = new StringWriter();

            //test
            xmlSerializer.Serialize(stringWriter, manifest);

            //assert
            Assert.AreEqual(DefaultXml, stringWriter.ToString());
        }

        [Test]
        public void Test_ManifestDeserializtion()
        {
            //setup
            var expectedManifest = Factories.Domain.Values.ManifestFactory.Basic;
            var xmlSerializer = new XmlSerializer(typeof(Manifest));
            var reader = new StringReader(DefaultXml);

            //test
            var manifest = (Manifest)xmlSerializer.Deserialize(reader);

            //assert
            Assert.AreEqual(null, manifest.FilePath);
            Assert.AreEqual(expectedManifest.ScriptManifests.Count, manifest.ScriptManifests.Count);
            for (int i = 0; i < manifest.ScriptManifests.Count; i++)
            {
                Assert.AreEqual(expectedManifest.ScriptManifests[i].Name, manifest.ScriptManifests[i].Name);
                Assert.AreEqual(expectedManifest.ScriptManifests[i].Path, manifest.ScriptManifests[i].Path);
                Assert.AreEqual(expectedManifest.ScriptManifests[i].Description, manifest.ScriptManifests[i].Description);
                Assert.AreEqual(expectedManifest.ScriptManifests[i].CurrentVersion, manifest.ScriptManifests[i].CurrentVersion);
                Assert.AreEqual(expectedManifest.ScriptManifests[i].NewVersion, manifest.ScriptManifests[i].NewVersion);
                Assert.AreEqual(expectedManifest.ScriptManifests[i].WrapInTransaction, manifest.ScriptManifests[i].WrapInTransaction);
            }
        }

        [Test]
        public void TestGetManifestWhenNull()
        {
            var manifest = Factories.Domain.Values.ManifestFactory.Default;
            var scriptManifest = manifest.GetManifest(null);
            Assert.That(scriptManifest, Is.Null);
        }

        [Test]
        public void TestGetManifestWhenEmpty()
        {
            var manifest = Factories.Domain.Values.ManifestFactory.Default;
            var scriptManifest = manifest.GetManifest(string.Empty);
            Assert.That(scriptManifest, Is.Null);
        }

        [Test]
        public void TestGetManifestWhenNotInScriptManifests()
        {            
            var name = "NonExistentName";
            var manifest = Factories.Domain.Values.ManifestFactory.Default;
            Assert.That(manifest.ScriptManifests.Exists(item => item.Name == name), Is.False);

            var scriptManifest = manifest.GetManifest(name);
            Assert.That(scriptManifest, Is.Null);
        }

        [Test]
        public void TestGetManifestWhenInScriptManifests()
        {
            var name = "script1";
            var manifest = Factories.Domain.Values.ManifestFactory.Basic;
            var expectedManifest = manifest.ScriptManifests.Find(item => item.Name == name);
            Assert.That(expectedManifest, Is.Not.Null);

            var scriptManifest = manifest.GetManifest(name);
            Assert.That(scriptManifest, Is.EqualTo(expectedManifest));
        }

        [Test]
        public void TestGetManifestWhenInScriptManifestsWithDifferentCasing()
        {
            var name = "SCript1";
            var manifest = Factories.Domain.Values.ManifestFactory.Basic;
            var expectedManifest = manifest.ScriptManifests.Find(item => item.Name == name);
            Assert.That(expectedManifest, Is.Null);
            expectedManifest = manifest.ScriptManifests.Find(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));
            Assert.That(expectedManifest, Is.Not.Null);

            var scriptManifest = manifest.GetManifest(name);
            Assert.That(scriptManifest, Is.EqualTo(expectedManifest));
        }

        [Test]
        public void WhenStartVersionIsNull_GetConcurrentScriptsThrowsNullArgumentException()
        {
            var manifest = Factories.Domain.Values.ManifestFactory.Default;
            try
            {
                manifest.GetConcurrentScripts(null, Factories.Domain.Values.VersionFactory.v1_0_0_2);
                Assert.Fail("Didn't throw exception");
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("startVersion"));
            }
            catch
            {
                Assert.Fail("Unexpected exception");
            }
        }

        [Test]
        public void WhenEndVersionIsNull_GetConcurrentScriptsThrowsNullArgumentException()
        {
            var manifest = Factories.Domain.Values.ManifestFactory.Default;
            try
            {
                manifest.GetConcurrentScripts(Factories.Domain.Values.VersionFactory.v1_0_0_0, null);
                Assert.Fail("Didn't throw exception");
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("endVersion"));
            }
            catch
            {
                Assert.Fail("Unexpected exception");
            }
        }

        [Test]
        public void WhenStartVersionIsGreaterThanEndVersion_GetConcurrentScriptsThrowsArgumentException()
        {
            var manifest = Factories.Domain.Values.ManifestFactory.Default;
            try
            {
                manifest.GetConcurrentScripts(Factories.Domain.Values.VersionFactory.v1_0_0_2, Factories.Domain.Values.VersionFactory.v1_0_0_0);
                Assert.Fail("Didn't throw exception");
            }
            catch (ArgumentException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("endVersion"));
                Assert.That(ex.Message, Is.EqualTo("endVersion must be greater than startVersion\r\nParameter name: endVersion"));
            }
            catch
            {
                Assert.Fail("Unexpected exception");
            }
        }

        [Test]
        public void WhenScriptManifestIsEmpty_GetConcurrentScriptsReturnsEmptyCollection()
        {
            var manifest = Factories.Domain.Values.ManifestFactory.Empty;
            Assert.IsEmpty(manifest.GetConcurrentScripts(Factories.Domain.Values.VersionFactory.v0_0_0_0, Factories.Domain.Values.VersionFactory.v1_0_0_2));
        }

        [Test]
        public void WhenScriptManifestHasConcurrentScripts_HasConcurrentScriptsReturnsTheScriptManifests()
        {
            var manifest = Factories.Domain.Values.ManifestFactory.Default;
            var scriptManifests = manifest.GetConcurrentScripts(Factories.Domain.Values.VersionFactory.v0_0_0_0, Factories.Domain.Values.VersionFactory.v0_0_0_2);
            Assert.That(scriptManifests, Has.Count.EqualTo(2));
            Assert.That(scriptManifests[0].Name, Is.EqualTo("increment to 0.0.0.1"));
            Assert.That(scriptManifests[1].Name, Is.EqualTo("increment to 0.0.0.2"));
        }

        [Test]
        public void WhenScriptManifestDoesntHaveConcurrentScripts_GetConcurrentScriptsReturnsEmptyCollection()
        {
            var manifest = Factories.Domain.Values.ManifestFactory.Default;
            Assert.IsEmpty(manifest.GetConcurrentScripts(Factories.Domain.Values.VersionFactory.v1_0_0_0, Factories.Domain.Values.VersionFactory.v1_0_0_2));
        }

        private const string DefaultXml = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<Manifest xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"http://code.google.com/p/databasescripter/2010/04/25/ManifestSchema\">\r\n  <ScriptManifests>\r\n    <ScriptManifest name=\"script1\" path=\"c:\\some\\path\\script1.sql\" transactional=\"true\" />\r\n    <ScriptManifest name=\"script2\" path=\"c:\\some\\path\\script2.sql\" transactional=\"false\">\r\n      <Description>This is a description of script 2</Description>\r\n      <CurrentVersion>0.0.0.0</CurrentVersion>\r\n      <NewVersion>0.0.0.1</NewVersion>\r\n    </ScriptManifest>\r\n  </ScriptManifests>\r\n</Manifest>";
    }
}
