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
    public class TestScript
    {

        #region Validation

        [Test]
        public void WhenNameIsNullIsInvalid()
        {
            var script = Factories.Domain.Entities.ScriptFactory.InvalidBecauseNameIsEmpty;
            Assert.That(script.IsValid(), Is.False);
        }


        #endregion

        #region ToString

        [Test]
        public void WhenNominalToStringReturnsTheScriptNameAndVersions()
        {
            var script = Factories.Domain.Entities.ScriptFactory.Nominal;
            Assert.That(script.ToString(), Is.EqualTo("Script \"Nominal script 1\": Increment database from 1.0.0.0 to 1.0.0.1"));
        }

        [Test]
        public void WhenNoCurrentVersionToStringReturnsTheScriptNameAndVersions()
        {
            var script = Factories.Domain.Entities.ScriptFactory.NoCurrentVersion;
            Assert.That(script.ToString(), Is.EqualTo("Script \"No current version script\": Increment database from current to 1.0.0.1"));
        }

        [Test]
        public void WhenNewCurrentVersionToStringReturnsTheScriptNameAndVersions()
        {
            //TODO: change
            var script = Factories.Domain.Entities.ScriptFactory.NoNewVersion;
            Assert.That(script.ToString(), Is.EqualTo("Script \"No new version script\": Increment database from 1.0.0.0 to same"));
        }

        [Test]
        public void WhenNoVersioningToStringReturnsTheScriptName()
        {
            var script = Factories.Domain.Entities.ScriptFactory.NoVersion;
            Assert.That(script.ToString(), Is.EqualTo("No version script"));
        }

        #endregion

        #region Run

        [Test]
        public void WhenRunFailsDuringConnect()
        {
            var script = Factories.Domain.Entities.ScriptFactory.Nominal;
            var databaseAdapter = new Mock<Bluejam.Utils.DatabaseScripter.Domain.Strategies.DatabaseAdapter>();
            databaseAdapter.Setup(call => call.Connect(It.Is<string>(str => str == script.ConnectionString))).Returns(false);
            
            var errorCode = script.Run(databaseAdapter.Object);
            Assert.That(errorCode, Is.EqualTo(Bluejam.Utils.DatabaseScripter.Domain.ErrorCode.DatabaseAdapterFailureAtConnect));

            databaseAdapter.VerifyAll();
        }

        [Test]
        public void WhenRunFailsDuringConfirmVersion()
        {
            var script = Factories.Domain.Entities.ScriptFactory.Nominal;

            bool confirmedVersion = false;
            var databaseAdapter = new Mock<Bluejam.Utils.DatabaseScripter.Domain.Strategies.DatabaseAdapter>();
            databaseAdapter.Setup(call => call.Connect(script.ConnectionString)).Returns(true);
            databaseAdapter.Setup(call => call.ConfirmVersion(script.DatabaseName, script.CurrentVersion, out confirmedVersion)).Returns(false);

            var errorCode = script.Run(databaseAdapter.Object);
            Assert.That(errorCode, Is.EqualTo(Bluejam.Utils.DatabaseScripter.Domain.ErrorCode.DatabaseAdapterFailureAtConfirmVersion));

            databaseAdapter.VerifyAll();
        }

        [Test]
        public void WhenRunFailsBecauseVersionIsIncorrect()
        {
            var script = Factories.Domain.Entities.ScriptFactory.Nominal;

            bool confirmedVersion = false;
            var databaseAdapter = new Mock<Bluejam.Utils.DatabaseScripter.Domain.Strategies.DatabaseAdapter>();
            databaseAdapter.Setup(call => call.Connect(script.ConnectionString)).Returns(true);
            databaseAdapter.Setup(call => call.ConfirmVersion(script.DatabaseName, script.CurrentVersion, out confirmedVersion)).Returns(true);

            var errorCode = script.Run(databaseAdapter.Object);
            Assert.That(errorCode, Is.EqualTo(Bluejam.Utils.DatabaseScripter.Domain.ErrorCode.IncorrectCurrentVersion));

            databaseAdapter.VerifyAll();
        }

        [Test]
        public void WhenRunFailsDuringRunCommand()
        {
            var script = Factories.Domain.Entities.ScriptFactory.Nominal;

            bool confirmedVersion = true;
            var databaseAdapter = new Mock<Bluejam.Utils.DatabaseScripter.Domain.Strategies.DatabaseAdapter>();
            databaseAdapter.Setup(call => call.Connect(script.ConnectionString)).Returns(true);
            databaseAdapter.Setup(call => call.ConfirmVersion(script.DatabaseName, script.CurrentVersion, out confirmedVersion)).Returns(true);
            databaseAdapter.Setup(call => call.RunCommand(script.DatabaseName, script.Command)).Returns(false);

            var errorCode = script.Run(databaseAdapter.Object);
            Assert.That(errorCode, Is.EqualTo(Bluejam.Utils.DatabaseScripter.Domain.ErrorCode.DatabaseAdapterFailureAtRunCommand));

            databaseAdapter.VerifyAll();
        }

        [Test]
        public void WhenRunFailsDuringSetVersion()
        {
            var script = Factories.Domain.Entities.ScriptFactory.Nominal;

            bool confirmedVersion = true;
            var databaseAdapter = new Mock<Bluejam.Utils.DatabaseScripter.Domain.Strategies.DatabaseAdapter>();
            databaseAdapter.Setup(call => call.Connect(script.ConnectionString)).Returns(true);
            databaseAdapter.Setup(call => call.ConfirmVersion(script.DatabaseName, script.CurrentVersion, out confirmedVersion)).Returns(true);
            databaseAdapter.Setup(call => call.RunCommand(script.DatabaseName, script.Command)).Returns(true);
            databaseAdapter.Setup(call => call.SetVersion(script.DatabaseName, script.NewVersion)).Returns(false);

            var errorCode = script.Run(databaseAdapter.Object);
            Assert.That(errorCode, Is.EqualTo(Bluejam.Utils.DatabaseScripter.Domain.ErrorCode.DatabaseAdapterFailureAtSetVersion));

            databaseAdapter.VerifyAll();
        }

        [Test]
        public void WhenRunNominalWithoutTransaction()
        {
            var script = Factories.Domain.Entities.ScriptFactory.Nominal;

            bool confirmedVersion = true;
            var databaseAdapter = new Mock<Bluejam.Utils.DatabaseScripter.Domain.Strategies.DatabaseAdapter>();
            databaseAdapter.Setup(call => call.Connect(script.ConnectionString)).Returns(true);
            databaseAdapter.Setup(call => call.ConfirmVersion(script.DatabaseName, script.CurrentVersion, out confirmedVersion)).Returns(true);
            databaseAdapter.Setup(call => call.RunCommand(script.DatabaseName, script.Command)).Returns(true);
            databaseAdapter.Setup(call => call.SetVersion(script.DatabaseName, script.NewVersion)).Returns(true);

            var errorCode = script.Run(databaseAdapter.Object);
            Assert.That(errorCode, Is.EqualTo(Bluejam.Utils.DatabaseScripter.Domain.ErrorCode.Ok));

            databaseAdapter.VerifyAll();
        }

        [Test]
        public void WhenRunNominalWithoutVersioning()
        {
            var script = Factories.Domain.Entities.ScriptFactory.NoVersion;

            var databaseAdapter = new Mock<Bluejam.Utils.DatabaseScripter.Domain.Strategies.DatabaseAdapter>();
            databaseAdapter.Setup(call => call.Connect(script.ConnectionString)).Returns(true);
            databaseAdapter.Setup(call => call.RunCommand(script.DatabaseName, script.Command)).Returns(true);

            var errorCode = script.Run(databaseAdapter.Object);
            Assert.That(errorCode, Is.EqualTo(Bluejam.Utils.DatabaseScripter.Domain.ErrorCode.Ok));

            databaseAdapter.VerifyAll();
        }

        [Test]
        public void WhenRunWithTransactionAndBeginTransactionFails()
        {
            var script = Factories.Domain.Entities.ScriptFactory.NominalWithTransaction;

            var databaseAdapter = new Mock<Bluejam.Utils.DatabaseScripter.Domain.Strategies.DatabaseAdapter>();
            databaseAdapter.Setup(call => call.Connect(script.ConnectionString)).Returns(true);
            databaseAdapter.Setup(call => call.BeginTransaction()).Returns(false);

            var errorCode = script.Run(databaseAdapter.Object);
            Assert.That(errorCode, Is.EqualTo(Bluejam.Utils.DatabaseScripter.Domain.ErrorCode.DatabaseAdapterFailureAtBeginTransaction));

            databaseAdapter.VerifyAll();
        }

        [Test]
        public void WhenRunWithTransactionAndCommitTransactionFailsTheTransactionIsRolledBack()
        {
            var script = Factories.Domain.Entities.ScriptFactory.NominalWithTransaction;

            var confirmedVersion = true;
            var databaseAdapter = new Mock<Bluejam.Utils.DatabaseScripter.Domain.Strategies.DatabaseAdapter>();
            databaseAdapter.Setup(call => call.Connect(script.ConnectionString)).Returns(true);
            databaseAdapter.Setup(call => call.BeginTransaction()).Returns(true);
            databaseAdapter.Setup(call => call.ConfirmVersion(script.DatabaseName, script.CurrentVersion, out confirmedVersion)).Returns(true);
            databaseAdapter.Setup(call => call.RunCommand(script.DatabaseName, script.Command)).Returns(true);
            databaseAdapter.Setup(call => call.SetVersion(script.DatabaseName, script.NewVersion)).Returns(true);
            databaseAdapter.Setup(call => call.CommitTransaction()).Returns(false);
            databaseAdapter.Setup(call => call.RollBackTransaction()).Returns(true);

            var errorCode = script.Run(databaseAdapter.Object);
            Assert.That(errorCode, Is.EqualTo(Bluejam.Utils.DatabaseScripter.Domain.ErrorCode.DatabaseAdapterFailureAtCommitTransaction));

            databaseAdapter.VerifyAll();
        }

        [Test]
        public void WhenRunWithTransactionAndRunCommandFailsTheTransactionIsRolledBack()
        {
            var script = Factories.Domain.Entities.ScriptFactory.NominalWithTransaction;

            var confirmedVersion = true;
            var databaseAdapter = new Mock<Bluejam.Utils.DatabaseScripter.Domain.Strategies.DatabaseAdapter>();
            databaseAdapter.Setup(call => call.Connect(script.ConnectionString)).Returns(true);
            databaseAdapter.Setup(call => call.BeginTransaction()).Returns(true);
            databaseAdapter.Setup(call => call.ConfirmVersion(script.DatabaseName, script.CurrentVersion, out confirmedVersion)).Returns(true);
            databaseAdapter.Setup(call => call.RunCommand(script.DatabaseName, script.Command)).Returns(false);
            databaseAdapter.Setup(call => call.RollBackTransaction()).Returns(true);

            var errorCode = script.Run(databaseAdapter.Object);
            Assert.That(errorCode, Is.EqualTo(Bluejam.Utils.DatabaseScripter.Domain.ErrorCode.DatabaseAdapterFailureAtRunCommand));

            databaseAdapter.VerifyAll();
        }

        [Test]
        public void WhenRunWithTransactionAndRolledFails()
        {
            var script = Factories.Domain.Entities.ScriptFactory.NominalWithTransaction;

            var confirmedVersion = true;
            var databaseAdapter = new Mock<Bluejam.Utils.DatabaseScripter.Domain.Strategies.DatabaseAdapter>();
            databaseAdapter.Setup(call => call.Connect(script.ConnectionString)).Returns(true);
            databaseAdapter.Setup(call => call.BeginTransaction()).Returns(true);
            databaseAdapter.Setup(call => call.ConfirmVersion(script.DatabaseName, script.CurrentVersion, out confirmedVersion)).Returns(true);
            databaseAdapter.Setup(call => call.RunCommand(script.DatabaseName, script.Command)).Returns(false);
            databaseAdapter.Setup(call => call.RollBackTransaction()).Returns(false);

            var errorCode = script.Run(databaseAdapter.Object);
            Assert.That(errorCode, Is.EqualTo(Bluejam.Utils.DatabaseScripter.Domain.ErrorCode.DatabaseAdapterFailureAtRollbackTransaction));

            databaseAdapter.VerifyAll();
        }


        #endregion

    }
}
