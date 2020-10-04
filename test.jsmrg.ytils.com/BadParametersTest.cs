using application.jsmrg.ytils.com.lib;
using NUnit.Framework;

namespace test.jsmrg.ytils.com
{
    public class BadParametersTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestExpectErrorBecauseOnlyOneFileParameter()
        {
            var programRunner = new ProgramRunner(new [] { "_ResTest/NamespaceDeclarationInjectionTest/MainFile.js" });
            var result = programRunner.Run();
            
            Assert.AreEqual(ProgramRunnerExit.Error, result);
        }
    }
}