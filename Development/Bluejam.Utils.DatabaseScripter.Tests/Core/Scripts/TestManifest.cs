using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Bluejam.Utils.DatabaseScripter.Core.Config;
using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Core.Scripts
{
    public class TestManifest
    {

        [Test]
        public void Test_ManifestSerialization()
        {
            //setup
            var manifest = GetDefaultManifest();
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
            var expectedManifest = GetDefaultManifest();
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
        private Manifest GetDefaultManifest()
        {
            var manifest = new Manifest();
            manifest.FilePath = @"c:\some\path\manifest.xml";
            manifest.ScriptManifests = new List<ScriptManifest>();

            var scriptManifest = new ScriptManifest();
            scriptManifest.Name = "script1";
            scriptManifest.Path = @"c:\some\path\script1.sql";
            scriptManifest.WrapInTransaction = true;
            manifest.ScriptManifests.Add(scriptManifest);

            scriptManifest = new ScriptManifest();
            scriptManifest.Name = "script2";
            scriptManifest.Path = @"c:\some\path\script2.sql";
            scriptManifest.Description = "This is a description of script 2";
            scriptManifest.CurrentVersion = "0.0.0.0";
            scriptManifest.NewVersion = "0.0.0.1";
            manifest.ScriptManifests.Add(scriptManifest);

            return manifest;
        }

        private const string DefaultXml = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<Manifest xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"http://code.google.com/p/databasescripter/2010/04/25/ManifestSchema\">\r\n  <ScriptManifests>\r\n    <ScriptManifest name=\"script1\" path=\"c:\\some\\path\\script1.sql\" transactional=\"true\" />\r\n    <ScriptManifest name=\"script2\" path=\"c:\\some\\path\\script2.sql\" transactional=\"false\">\r\n      <Description>This is a description of script 2</Description>\r\n      <CurrentVersion>0.0.0.0</CurrentVersion>\r\n      <NewVersion>0.0.0.1</NewVersion>\r\n    </ScriptManifest>\r\n  </ScriptManifests>\r\n</Manifest>";
    }
}
