using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Microsoft.SqlServer.Management.Smo;

namespace Bluejam.Utils.DatabaseScripter.SystemTests
{
    public class DatabaseAsserter
    {
        private Server server;
        private string databaseName;

        public DatabaseAsserter(Server server, string databaseName)
        {
            this.server = server;
            this.databaseName = databaseName;
        }

        public void AssertNominal()
        {
            AssertThatDatabaseExists();
            AssertThatIncrement0_0_0_1HasBeenApplied();
            AssertThatSampleDataHasBeenInserted();
        }

        public void AssertThatDatabaseExists()
        {
            Assert.That(server.Databases.Contains(databaseName), Is.True);
        }

        public void AssertThatDatabaseDoesNotExist()
        {
            Assert.That(server.Databases.Contains(databaseName), Is.False);
        }

        public void AssertThatSchemaVersionIs0_0_0_0()
        {
            var database = server.Databases[databaseName];
            Assert.That(database.ExtendedProperties["SCHEMA_VERSION"].Value, Is.EqualTo("0.0.0.0"));
        }

        public void AssertThatSchemaVersionIs0_0_0_1()
        {
            var database = server.Databases[databaseName];
            Assert.That(database.ExtendedProperties["SCHEMA_VERSION"].Value, Is.EqualTo("0.0.0.1"));
        }

        public void AssertThatIncrement0_0_0_1HasBeenApplied()
        {
            var database = server.Databases[databaseName];
            Assert.That(database.Tables.Contains("MediumType"), Is.True);
            Assert.That(database.Tables.Contains("SeriesItem"), Is.True);
        }

        public void AssertThatIncrement0_0_0_1HasNotBeenApplied()
        {
            var database = server.Databases[databaseName];
            Assert.That(database.Tables.Contains("MediumType"), Is.False);
            Assert.That(database.Tables.Contains("SeriesItem"), Is.False);
        }

        public void AssertThatIncrement0_0_0_2HasNotBeenApplied()
        {
            var database = server.Databases[databaseName];
            Assert.IsFalse(database.Tables.Contains("AnotherTable"));
        }

        public void AssertThatSampleDataHasBeenInserted()
        {
            var database = server.Databases[databaseName];
            Assert.That(database.Tables["MediumType"].RowCount, Is.EqualTo(2));
            Assert.That(database.Tables["SeriesItem"].RowCount, Is.EqualTo(1));
        }

        public void AssertThatSampleDataHasNotBeenInserted()
        {
            var database = server.Databases[databaseName];
            Assert.That(database.Tables["MediumType"].RowCount, Is.EqualTo(0));
            Assert.That(database.Tables["SeriesItem"].RowCount, Is.EqualTo(0));
        }

    }
}
