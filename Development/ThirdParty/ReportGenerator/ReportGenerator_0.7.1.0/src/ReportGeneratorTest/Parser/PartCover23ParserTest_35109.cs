﻿using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Palmmedia.ReportGenerator.Parser;

namespace Palmmedia.ReportGeneratorTest.Parser
{
    /// <summary>
    /// This is a test class for PartCover23ParserTest and is intended
    /// to contain all PartCover23ParserTest Unit Tests
    /// </summary>
	[TestClass()]
    public class PartCover23ParserTest_35109
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

        static readonly string filePath = Path.Combine(FileManager.GetReportDirectory(), "Partcover2.3.0.35109.xml");

		static IParser parser;

		#region Additional test attributes
		// You can use the following additional attributes as you write your tests:

		// Use ClassInitialize to run code before running the first test in the class
		[ClassInitialize()]
		public static void MyClassInitialize(TestContext testContext)
		{
            FileManager.CopyTestClasses();

			var report = XDocument.Load(filePath);
			parser = new PartCover23Parser(report);
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
		/// A test for NumberOfLineVisits
		/// </summary>
		[TestMethod()]
		public void NumberOfLineVisitsTest()
		{
            Assert.AreEqual(1, parser.NumberOfLineVisits("Test", "Test.TestClass", "C:\\temp\\TestClass.cs", 14), "Wrong number of line visits");
           
			Assert.AreEqual(0, parser.NumberOfLineVisits("Test", "Test.TestClass", "C:\\temp\\TestClass.cs", 18), "Wrong number of line visits");

			Assert.AreEqual(0, parser.NumberOfLineVisits("Test", "Test.TestClass2", "C:\\temp\\TestClass2.cs", 19), "Wrong number of line visits");

            Assert.AreEqual(2, parser.NumberOfLineVisits("Test", "Test.TestClass2", "C:\\temp\\TestClass2.cs", 25), "Wrong number of line visits");

            Assert.AreEqual(1, parser.NumberOfLineVisits("Test", "Test.TestClass2", "C:\\temp\\TestClass2.cs", 31), "Wrong number of line visits");

            Assert.AreEqual(0, parser.NumberOfLineVisits("Test", "Test.TestClass2", "C:\\temp\\TestClass2.cs", 37), "Wrong number of line visits");

            Assert.AreEqual(4, parser.NumberOfLineVisits("Test", "Test.TestClass2", "C:\\temp\\TestClass2.cs", 54), "Wrong number of line visits");

            Assert.AreEqual(0, parser.NumberOfLineVisits("Test", "Test.TestClass2", "C:\\temp\\TestClass2.cs", 81), "Wrong number of line visits");

            Assert.AreEqual(1, parser.NumberOfLineVisits("Test", "Test.PartialClass", "C:\\temp\\PartialClass.cs", 9), "Wrong number of line visits");

            Assert.AreEqual(0, parser.NumberOfLineVisits("Test", "Test.PartialClass", "C:\\temp\\PartialClass.cs", 14), "Wrong number of line visits");

            Assert.AreEqual(1, parser.NumberOfLineVisits("Test", "Test.PartialClass", "C:\\temp\\PartialClass2.cs", 9), "Wrong number of line visits");

            Assert.AreEqual(0, parser.NumberOfLineVisits("Test", "Test.PartialClass", "C:\\temp\\PartialClass2.cs", 14), "Wrong number of line visits");
		}

		/// <summary>
		/// A test for NumberOfFiles
		/// </summary>
		[TestMethod()]
		public void NumberOfFilesTest()
		{
			Assert.AreEqual(4, parser.Files().Count(), "Wrong number of files");
		}

		/// <summary>
		/// A test for FilesOfClass
		/// </summary>
		[TestMethod()]
		public void FilesOfClassTest()
		{
			Assert.AreEqual(1, parser.FilesOfClass("Test", "Test.TestClass").Count(), "Wrong number of files");
            Assert.AreEqual(2, parser.FilesOfClass("Test", "Test.PartialClass").Count(), "Wrong number of files");
		}

		/// <summary>
		/// A test for ClassesInAssembly
		/// </summary>
		[TestMethod()]
		public void ClassesInAssemblyTest()
		{
			Assert.AreEqual(4, parser.ClassesInAssembly("Test").Count(), "Wrong number of classes");
		}

		/// <summary>
		/// A test for Assemblies
		/// </summary>
		[TestMethod()]
		public void AssembliesTest()
		{
			Assert.AreEqual(1, parser.Assemblies().Count(), "Wrong number of assemblies");
		}
	}
}
