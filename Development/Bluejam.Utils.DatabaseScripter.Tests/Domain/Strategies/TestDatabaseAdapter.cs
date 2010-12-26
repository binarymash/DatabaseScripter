using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Domain.Strategies
{
    [TestFixture]
    public class TestDatabaseAdapter
    {
        [Test]
        public void Test_IsDisposable()
        {
            var databaseAdapter = new Moq.Mock<Bluejam.Utils.DatabaseScripter.Domain.Strategies.DatabaseAdapter>();
            databaseAdapter.Object.Dispose();
        }
    }
}
