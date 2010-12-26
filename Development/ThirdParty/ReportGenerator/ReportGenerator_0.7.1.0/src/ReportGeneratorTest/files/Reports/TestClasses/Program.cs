
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            new TestClass().SampleFunction();
            new TestClass2("Test").ExecutedMethod();
            new TestClass2("Test").SampleFunction("Munich");
            new PartialClass().ExecutedMethod_1();
            new PartialClass().ExecutedMethod_2();
        }
    }
}
