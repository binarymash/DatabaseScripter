using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.DbAdapter;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests
{
    [TestFixture]
    public class TestVersion
    {

        #region Tests

        [Test]
        public void Test_Constructor_WhenInts()
        {
            var version = new DbAdapter.Version(5, 4, 3, 2);
            Assert.AreEqual(5, version.Major);
            Assert.AreEqual(4, version.Minor);
            Assert.AreEqual(3, version.Build);
            Assert.AreEqual(2, version.Revision);
        }

        [Test]
        public void Test_Constructor_WhenGoodString()
        {
            var version = new DbAdapter.Version("10.2.1.5");
            Assert.AreEqual(10, version.Major);
            Assert.AreEqual(2, version.Minor);
            Assert.AreEqual(1, version.Build);
            Assert.AreEqual(5, version.Revision);
        }

        [Test]
        [Ignore("fix")]
        public void Test_Constructor_WhenStringContainsBadData()
        {
            try
            {
                var version = new DbAdapter.Version("10.v.1.5");
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        [Ignore("fix")]
        public void Test_Constructor_WhenStringIsTooShort()
        {
            try
            {
                var version = new DbAdapter.Version("1.5.4");
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        [Ignore("fix")]
        public void Test_Constructor_WhenNullString()
        {
            try
            {
                var version = new DbAdapter.Version(null);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        [Ignore("fix")]
        public void Test_Constructor_WhenEmptyString()
        {
            try
            {
                var version = new DbAdapter.Version(string.Empty);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Test_GreaterThanOperator()
        {
            var version1 = new DbAdapter.Version(2, 3, 4, 1);

            //revision is greater
            var version2 = new DbAdapter.Version(2, 3, 4, 2);
            Assert.IsTrue(version2 > version1);

            //build is greater
            version2 = new DbAdapter.Version(2, 3, 5, 1);
            Assert.IsTrue(version2 > version1);

            //minor is greater
            version2 = new DbAdapter.Version(2, 4, 1, 0);
            Assert.IsTrue(version2 > version1);

            //major is greater
            version2 = new DbAdapter.Version(3, 1, 1, 0);
            Assert.IsTrue(version2 > version1);

            //equal
            version2 = new DbAdapter.Version(2, 3, 4, 1);
            Assert.IsFalse(version2 > version1);

            //revision is less
            version2 = new DbAdapter.Version(2, 3, 4, 0);
            Assert.IsFalse(version2 > version1);

            //build is less
            version2 = new DbAdapter.Version(2, 3, 2, 4);
            Assert.IsFalse(version2 > version1);

            //minor is less
            version2 = new DbAdapter.Version(2, 2, 5, 5);
            Assert.IsFalse(version2 > version1);

            //major is less
            version2 = new DbAdapter.Version(1, 4, 5, 5);
            Assert.IsFalse(version2 > version1);
        }

        [Test]
        public void Test_GreaterThanOrEqualToOperator()
        {
            var version1 = new DbAdapter.Version(2, 3, 4, 5);

            //revision is greater
            var version2 = new DbAdapter.Version(2, 3, 4, 6);
            Assert.IsTrue(version2 >= version1);

            //build is greater
            version2 = new DbAdapter.Version(2, 3, 5, 4);
            Assert.IsTrue(version2 >= version1);

            //minor is greater
            version2 = new DbAdapter.Version(2, 4, 1, 1);
            Assert.IsTrue(version2 >= version1);

            //major is greater
            version2 = new DbAdapter.Version(3, 1, 1, 1);
            Assert.IsTrue(version2 >= version1);

            //equal
            version2 = new DbAdapter.Version(2, 3, 4, 5);
            Assert.IsTrue(version2 >= version1);

            //revision is less
            version2 = new DbAdapter.Version(2, 3, 4, 3);
            Assert.IsFalse(version2 >= version1);

            //build is less
            version2 = new DbAdapter.Version(2, 3, 3, 6);
            Assert.IsFalse(version2 >= version1);

            //minor is less
            version2 = new DbAdapter.Version(2, 2, 5, 6);
            Assert.IsFalse(version2 >= version1);

            //major is less
            version2 = new DbAdapter.Version(1, 4, 5, 6);
            Assert.IsFalse(version2 >= version1);
        }

        [Test]
        public void Test_EqualityOperator()
        {
            var version1 = new DbAdapter.Version(2, 3, 4, 5);

            //revision is greater
            var version2 = new DbAdapter.Version(2, 3, 4, 6);
            Assert.IsFalse(version2 == version1);

            //minor is greater
            version2 = new DbAdapter.Version(2, 4, 5, 1);
            Assert.IsFalse(version2 == version1);

            //major is greater
            version2 = new DbAdapter.Version(3, 1, 1, 1);
            Assert.IsFalse(version2 == version1);

            //equal
            version2 = new DbAdapter.Version(2, 3, 4, 5);
            Assert.IsTrue(version2 == version1);

            //revision is less
            version2 = new DbAdapter.Version(2, 3, 4, 4);
            Assert.IsFalse(version2 == version1);

            //build is less
            version2 = new DbAdapter.Version(2, 3, 3, 5);
            Assert.IsFalse(version2 == version1);

            //minor is less
            version2 = new DbAdapter.Version(2, 2, 5, 5);
            Assert.IsFalse(version2 == version1);

            //major is less
            version2 = new DbAdapter.Version(1, 4, 5, 5);
            Assert.IsFalse(version2 == version1);

            //both null
            Assert.IsTrue((DbAdapter.Version)null == (DbAdapter.Version)null);

            //lhs null
            Assert.IsFalse((DbAdapter.Version)null == version1);

            //rhs null
            Assert.IsFalse(version2 == (DbAdapter.Version)null);
        
        }

        [Test]
        public void Test_LessThanOrEqualToOperator()
        {
            var version1 = new DbAdapter.Version(2, 3, 4, 5);

            //revision is greater
            var version2 = new DbAdapter.Version(2, 3, 4, 6);
            Assert.IsFalse(version2 <= version1);

            //build is greater
            version2 = new DbAdapter.Version(2, 3, 5, 4);
            Assert.IsFalse(version2 <= version1);

            //minor is greater
            version2 = new DbAdapter.Version(2, 4, 1, 1);
            Assert.IsFalse(version2 <= version1);

            //major is greater
            version2 = new DbAdapter.Version(3, 1, 1, 1);
            Assert.IsFalse(version2 <= version1);

            //equal
            version2 = new DbAdapter.Version(2, 3, 4, 5);
            Assert.IsTrue(version2 <= version1);

            //revision is less
            version2 = new DbAdapter.Version(2, 3, 4, 3);
            Assert.IsTrue(version2 <= version1);

            //build is less
            version2 = new DbAdapter.Version(2, 3, 3, 5);
            Assert.IsTrue(version2 <= version1);

            //minor is less
            version2 = new DbAdapter.Version(2, 2, 5, 5);
            Assert.IsTrue(version2 <= version1);

            //major is less
            version2 = new DbAdapter.Version(1, 4, 5, 5);
            Assert.IsTrue(version2 <= version1);
        }

        [Test]
        public void Test_LessThanOperator()
        {
            var version1 = new DbAdapter.Version(2, 3, 4, 5);

            //revision is greater
            var version2 = new DbAdapter.Version(2, 3, 4, 6);
            Assert.IsFalse(version2 < version1);

            //build is greater
            version2 = new DbAdapter.Version(2, 3, 5, 1);
            Assert.IsFalse(version2 < version1);

            //minor is greater
            version2 = new DbAdapter.Version(2, 4, 1, 1);
            Assert.IsFalse(version2 < version1);

            //major is greater
            version2 = new DbAdapter.Version(3, 1, 1, 1);
            Assert.IsFalse(version2 < version1);

            //equal
            version2 = new DbAdapter.Version(2, 3, 4, 5);
            Assert.IsFalse(version2 < version1);

            //revision is less
            version2 = new DbAdapter.Version(2, 3, 4, 3);
            Assert.IsTrue(version2 < version1);

            //build is less
            version2 = new DbAdapter.Version(2, 3, 3, 5);
            Assert.IsTrue(version2 < version1);

            //minor is less
            version2 = new DbAdapter.Version(2, 2, 5, 5);
            Assert.IsTrue(version2 < version1);

            //major is less
            version2 = new DbAdapter.Version(1, 4, 5, 5);
            Assert.IsTrue(version2 < version1);
        }

        [Test]
        public void Test_InequalityOperator()
        {
            var version1 = new DbAdapter.Version(2, 3, 4, 5);

            //revision is greater
            var version2 = new DbAdapter.Version(2, 3, 4, 6);
            Assert.IsTrue(version2 != version1);

            //build is greater
            version2 = new DbAdapter.Version(2, 3, 5, 5);
            Assert.IsTrue(version2 != version1);

            //minor is greater
            version2 = new DbAdapter.Version(2, 4, 1, 1);
            Assert.IsTrue(version2 != version1);

            //major is greater
            version2 = new DbAdapter.Version(3, 1, 1, 1);
            Assert.IsTrue(version2 != version1);

            //equal
            version2 = new DbAdapter.Version(2, 3, 4, 5);
            Assert.IsFalse(version2 != version1);

            //revision is less
            version2 = new DbAdapter.Version(2, 3, 4, 3);
            Assert.IsTrue(version2 != version1);

            //build is less
            version2 = new DbAdapter.Version(2, 3, 3, 5);
            Assert.IsTrue(version2 != version1);

            //minor is less
            version2 = new DbAdapter.Version(2, 2, 5, 5);
            Assert.IsTrue(version2 != version1);

            //major is less
            version2 = new DbAdapter.Version(1, 4, 5, 5);
            Assert.IsTrue(version2 != version1);

            //both null
            Assert.IsFalse((DbAdapter.Version)null != (DbAdapter.Version)null);

            //lhs null
            Assert.IsTrue((DbAdapter.Version)null != version1);

            //rhs null
            Assert.IsTrue(version2 != (DbAdapter.Version)null);

        }

        #endregion

    }
}
