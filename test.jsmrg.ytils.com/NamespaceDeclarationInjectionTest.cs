using application.jsmrg.ytils.com.lib;
using NUnit.Framework;

namespace test.jsmrg.ytils.com
{
    public class NamespaceDeclarationInjectionTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestNamespaceDeclarationInjection()
        {
            var programRunner = new ProgramRunner(new [] { "_ResTest/NamespaceDeclarationInjectionTest/MainFile.js" });
            var result = programRunner.Run();
            
            Assert.AreEqual(ProgramRunnerExit.Done, result);
        }
    }
}