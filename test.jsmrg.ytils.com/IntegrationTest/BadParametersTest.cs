using application.jsmrg.ytils.com.lib;
using NUnit.Framework;

namespace test.jsmrg.ytils.com.IntegrationTest
{
    public class BadParametersTest
    {
        [Test]
        public void TestExpectErrorBecauseOnlyOneFileParameter()
        {
            var programRunner = new ProgramRunner(new [] { "ResTest/NamespaceDeclarationInjectionTest/MainFile.js" });
            var result = programRunner.Run();
            
            Assert.AreEqual(ProgramRunnerExit.Error, result);
        }
    }
}