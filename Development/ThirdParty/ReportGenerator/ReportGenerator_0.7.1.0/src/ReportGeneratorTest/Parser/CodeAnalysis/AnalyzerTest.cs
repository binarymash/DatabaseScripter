using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Palmmedia.ReportGenerator.Parser.CodeAnalysis;

namespace Palmmedia.ReportGeneratorTest.Parser.CodeAnalysis
{
    /// <summary>
    /// This is a test class for Analyzer and is intended
    /// to contain all Analyzer Unit Tests
    /// </summary>
    [TestClass()]
    public class AnalyzerTest
    {
        private static string classFile = Path.Combine(FileManager.GetCodeAnalysisDirectory(), "AnalyzerTestClass.cs");

        /// <summary>
        /// A test for FindMethod
        /// </summary>
        [TestMethod()]
        public void FindMethod_SearchExistingMethod_MethodResultMustNotBeNullAndSupplyCorrectLinenumber()
        {
            MethodInfo methodInfo = new MethodInfo(
                "AnalyzerTestClass",
                "DoSomething",
                "string  (string, string[], System.Guid, string, string, System.Decimal, int, stringint, ref int, float, double, bool, unsigned byte, char, object, byte, short, unsigned int, unsigned long, unsigned short, ICSharpCode.NRefactory.Ast.INode)");

            MethodResult methodResult = Analyzer.FindMethod(classFile, methodInfo);

            Assert.IsNotNull(methodResult, "MethodResult must not be null.");

            Assert.AreEqual(35, methodResult.Start, "Start line number does not match.");
            Assert.AreEqual(38, methodResult.End, "End line number does not match.");
        }

        /// <summary>
        /// A test for FindMethod
        /// </summary>
        [TestMethod()]
        public void FindMethod_SearchExistingConstructor_MethodResultMustNotBeNullAndSupplyCorrectLinenumber()
        {
            MethodInfo methodInfo = new MethodInfo(
                "AnalyzerTestClass",
                ".ctor",
                "void  ()");

            MethodResult methodResult = Analyzer.FindMethod(classFile, methodInfo);

            Assert.IsNotNull(methodResult, "MethodResult must not be null.");

            Assert.AreEqual(10, methodResult.Start, "Start line number does not match.");
            Assert.AreEqual(12, methodResult.End, "End line number does not match.");
        } 

        /// <summary>
        /// A test for FindMethod
        /// </summary>
        [TestMethod()]
        public void FindMethod_SearchExistingGenericMethod_MethodResultIsNull()
        {
            MethodInfo methodInfo = new MethodInfo(
                "AnalyzerTestClass",
                "GenericMethod", 
                "void  (int)");

            MethodResult methodResult = Analyzer.FindMethod(classFile, methodInfo);

            Assert.IsNull(methodResult, "MethodResult is not null.");
        }        
    }
}
