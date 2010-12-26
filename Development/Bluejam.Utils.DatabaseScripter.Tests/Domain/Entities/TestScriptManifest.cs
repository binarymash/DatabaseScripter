using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Factories = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories;
using Moq;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Domain.Entities
{

    [TestFixture]
    public class TestScriptManifest : DomainTestBase
    {

        #region Validation

        #region Nominal

        [Test]
        public void Nominal_IsValid()
        {
            var scriptManifest = Factories.Domain.Entities.ScriptManifestFactory.Create;
            AssertIsValid(scriptManifest);
        }

        #endregion

        #region Name

        [Test]
        public void WhenNameIsNull_IsInvalid()
        {
            var scriptManifest = Factories.Domain.Entities.ScriptManifestFactory.Create;
            AssertIsValid(scriptManifest);

            scriptManifest.Name = null;
            AssertIsInvalid(scriptManifest, 1);
            AssertValidationResult(scriptManifest, "Name", "A script manifest must have a name");
        }

        [Test]
        public void WhenNameIsEmpty_IsInvalid()
        {
            var scriptManifest = Factories.Domain.Entities.ScriptManifestFactory.Create;
            AssertIsValid(scriptManifest);

            scriptManifest.Name = string.Empty;
            AssertIsInvalid(scriptManifest, 1);
            AssertValidationResult(scriptManifest, "Name", "A script manifest must have a name");
        }

        [Test]
        public void WhenNameIsWhiteSpace_IsInvalid()
        {
            var scriptManifest = Factories.Domain.Entities.ScriptManifestFactory.Create;
            AssertIsValid(scriptManifest);

            scriptManifest.Name = "   ";
            AssertIsInvalid(scriptManifest, 1);
            AssertValidationResult(scriptManifest, "Name", "A script manifest must have a name");
        }

        #endregion

        #region Description

        [Test]
        public void WhenDescriptionIsNull_IsValid()
        {
            var scriptManifest = Factories.Domain.Entities.ScriptManifestFactory.Create;
            AssertIsValid(scriptManifest);

            scriptManifest.Description = null;
            AssertIsValid(scriptManifest);
        }

        [Test]
        public void WhenDescriptionIsEmpty_IsValid()
        {
            var scriptManifest = Factories.Domain.Entities.ScriptManifestFactory.Create;
            AssertIsValid(scriptManifest);

            scriptManifest.Description = string.Empty;
            AssertIsValid(scriptManifest);
        }

        [Test]
        public void WhenDescriptionIsWhiteSpace_IsValid()
        {
            var scriptManifest = Factories.Domain.Entities.ScriptManifestFactory.Create;
            AssertIsValid(scriptManifest);

            scriptManifest.Description = "   ";
            AssertIsValid(scriptManifest);
        }

        #endregion

        #region Path

        [Test]
        public void WhenPathIsNull_IsInvalid()
        {
            var scriptManifest = Factories.Domain.Entities.ScriptManifestFactory.Create;
            AssertIsValid(scriptManifest);

            scriptManifest.Path = null;
            AssertIsInvalid(scriptManifest, 1);
            AssertValidationResult(scriptManifest, "Path", "A script manifest must have a path");
        }

        [Test]
        public void WhenPathIsEmpty_IsInvalid()
        {
            var scriptManifest = Factories.Domain.Entities.ScriptManifestFactory.Create;
            AssertIsValid(scriptManifest);

            scriptManifest.Path = string.Empty;
            AssertIsInvalid(scriptManifest, 1);
            AssertValidationResult(scriptManifest, "Path", "A script manifest must have a path");
        }

        [Test]
        public void WhenPathIsWhiteSpace_IsInvalid()
        {
            var scriptManifest = Factories.Domain.Entities.ScriptManifestFactory.Create;
            AssertIsValid(scriptManifest);

            scriptManifest.Path = "   ";
            AssertIsInvalid(scriptManifest, 1);
            AssertValidationResult(scriptManifest, "Path", "A script manifest must have a path");
        }

        #endregion

        #region CurrentVersion

        [Test]
        public void WhenCurrentVersionIsNull_IsValid()
        {
            var scriptManifest = Factories.Domain.Entities.ScriptManifestFactory.Create;
            AssertIsValid(scriptManifest);

            scriptManifest.CurrentVersion = null;
            AssertIsValid(scriptManifest);
        }

        //TODO: validation of permitted current version format

        #endregion

        #region NewVersion

        [Test]
        public void WhenNewVersionIsNull_IsValid()
        {
            var scriptManifest = Factories.Domain.Entities.ScriptManifestFactory.Create;
            AssertIsValid(scriptManifest);

            scriptManifest.NewVersion = null;
            AssertIsValid(scriptManifest);
        }

        //TODO: validation of permitted current version format

        #endregion

        #endregion

    }
}
