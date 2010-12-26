using Microsoft.VisualStudio.TestTools.UnitTesting;
using Palmmedia.ReportGenerator;

namespace Palmmedia.ReportGeneratorTest
{
    /// <summary>
    /// This is a test class for ClassTest and is intended
    /// to contain all ClassTest Unit Tests
    /// </summary>
	[TestClass()]
	public class ClassTest
	{
		private TestContext testContextInstance;

		/// <summary>
		/// Gets or sets the test context which provides
		/// information about and functionality for the current test run.
		/// </summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}

			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
 
		// You can use the following additional attributes as you write your tests:

		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext)
		// {
		// }

		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup()
		// {
		// }

		// Use TestInitialize to run code before running each test
		// [TestInitialize()]
		// public void MyTestInitialize()
		// {
		// }

		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup()
		// {
		// }
		#endregion

		/// <summary>
		/// A test for Class Constructor
		/// </summary>
		[TestMethod()]
		public void ClassConstructorTest()
		{
			var assemblyname = "TestAssembly";
			var classname = "TestClass";
			var target = new Class(assemblyname, classname);
			Assert.AreEqual(assemblyname, target.Assemblyname, "Not equal");
			Assert.AreEqual(classname, target.Name, "Not equal");
		}

        /// <summary>
        /// A test for Equals
        /// </summary>
        [TestMethod()]
        public void EqualsTest()
        {
            var assemblyname = "TestAssembly";
            var classname = "TestClass";

            var target1 = new Class(assemblyname, classname);
            var target2 = new Class(assemblyname, classname);

            Assert.IsTrue(target1.Equals(target2), "Objects are not equal");

            Assert.IsFalse(target1.Equals(null), "Objects are equal");

            Assert.IsFalse(target1.Equals(new object()), "Objects are equal");
        }
	}
}
