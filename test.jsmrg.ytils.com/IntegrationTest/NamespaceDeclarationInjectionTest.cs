using application.jsmrg.ytils.com.lib;
using NUnit.Framework;

namespace test.jsmrg.ytils.com.IntegrationTest
{
    public class NamespaceDeclarationInjectionTest
    {
        [Test]
        public void TestNamespaceDeclarationInjection()
        {
            var programRunner = new ProgramRunner(new [] { "_ResTest/NamespaceDeclarationInjectionTest/MainFile.js", "_ResTest/NamespaceDeclarationInjectionTest/MainFile.out.js" });
            var result = programRunner.Run();
            var output = programRunner.JsMrgOutput;
            
            Assert.AreEqual(ProgramRunnerExit.Done, result);
            Assert.True(output.Contains("YTILS.create = { };"));
            Assert.True(output.Contains("YTILS.defaults = { };"));
            Assert.True(output.Contains("YTILS.dependency = { };"));
        }
    }
}