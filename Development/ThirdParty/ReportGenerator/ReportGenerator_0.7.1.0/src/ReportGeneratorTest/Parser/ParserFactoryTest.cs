using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Palmmedia.ReportGenerator.Parser;

namespace Palmmedia.ReportGeneratorTest.Parser
{
    /// <summary>
    /// This is a test class for ParserFactoryTest and is intended
    /// to contain all ParserFactoryTest Unit Tests
    /// </summary>
	[TestClass()]
	public class ParserFactoryTest
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            FileManager.CopyTestClasses();
        }

        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            FileManager.DeleteTestClasses();
        }

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
		/// A test for CreateParser
		/// </summary>
		[TestMethod()]
		public void CreateParserTest()
		{
            // Single reports
			var filePath = Path.Combine(FileManager.GetReportDirectory(), "Partcover2.3.xml");
			Assert.IsInstanceOfType(ParserFactory.CreateParser(new string[] { filePath }), typeof(PartCover23Parser), "Wrong type");

			filePath = Path.Combine(FileManager.GetReportDirectory(), "Partcover2.2.xml");
            Assert.IsInstanceOfType(ParserFactory.CreateParser(new string[] { filePath }), typeof(PartCover22Parser), "Wrong type");

			filePath = Path.Combine(FileManager.GetReportDirectory(), "NCover1.5.8.xml");
            Assert.IsInstanceOfType(ParserFactory.CreateParser(new string[] { filePath }), typeof(NCoverParser), "Wrong type");

            // Multi reports - Single file
			filePath = Path.Combine(FileManager.GetReportDirectory(), "MultiPartcover2.3.xml");
			Assert.IsInstanceOfType(ParserFactory.CreateParser(new string[] { filePath }), typeof(MultiReportParser), "Wrong type");

			filePath = Path.Combine(FileManager.GetReportDirectory(), "MultiPartcover2.2.xml");
			Assert.IsInstanceOfType(ParserFactory.CreateParser(new string[] { filePath }), typeof(MultiReportParser), "Wrong type");

			filePath = Path.Combine(FileManager.GetReportDirectory(), "MultiNCover1.5.8.xml");
			Assert.IsInstanceOfType(ParserFactory.CreateParser(new string[] { filePath }), typeof(MultiReportParser), "Wrong type");

            // Multi reports - Several files
            filePath = Path.Combine(FileManager.GetReportDirectory(), "Partcover2.2.xml");
            var filePath2 = Path.Combine(FileManager.GetReportDirectory(), "Partcover2.3.xml");
            Assert.IsInstanceOfType(ParserFactory.CreateParser(new string[] { filePath, filePath2 }), typeof(MultiReportParser), "Wrong type");

            filePath2 = Path.Combine(FileManager.GetReportDirectory(), "MultiPartcover2.3.xml");
            Assert.IsInstanceOfType(ParserFactory.CreateParser(new string[] { filePath, filePath2 }), typeof(MultiReportParser), "Wrong type");

            // No report
            Assert.IsNull(ParserFactory.CreateParser(new string[] { string.Empty }), "Excepted null");
		}
	}
}
