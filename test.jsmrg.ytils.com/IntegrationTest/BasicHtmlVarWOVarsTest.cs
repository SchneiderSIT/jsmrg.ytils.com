using application.jsmrg.ytils.com.lib;
using NUnit.Framework;

namespace test.jsmrg.ytils.com.IntegrationTest
{
    // ReSharper disable once InconsistentNaming
    public class BasicHtmlVarWOVarsTest
    {
        [Test]
        // ReSharper disable once InconsistentNaming
        public void TestBasicHtmlVarWOVars()
        {
            var programRunner = new ProgramRunner(new [] { "ResTest/BasicHtmlVarWOVarsTest/MainFile.js", "ResTest/BasicHtmlVarWOVarsTest/MainFile.out.js" });
            var result = programRunner.Run();
            // var output = programRunner.JsMrgOutput;
            
            Assert.AreEqual(ProgramRunnerExit.Done, result);
        }
    }
}