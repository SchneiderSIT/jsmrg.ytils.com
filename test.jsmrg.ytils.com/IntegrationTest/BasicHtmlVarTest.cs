using application.jsmrg.ytils.com.lib;
using NUnit.Framework;

namespace test.jsmrg.ytils.com.IntegrationTest
{
    public class BasicHtmlVarTest
    {
        [Test]
        public void TestBasicHtmlVar()
        {
            var programRunner = new ProgramRunner(new [] { "_ResTest/BasicHtmlVarTest/MainFile.js", "_ResTest/BasicHtmlVarTest/MainFile.out.js" });
            var result = programRunner.Run();
            var output = programRunner.JsMrgOutput;
            
            Assert.AreEqual(ProgramRunnerExit.Done, result);
            Assert.True(output.Contains("<div class=\\\"someContainer\\\">"));
            Assert.True(output.Contains("<div class=\\\"\" + foo + \"\\\">\" + bar + \"</div>"));
        }
    }
}