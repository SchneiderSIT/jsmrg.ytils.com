using application.jsmrg.ytils.com.lib;
using NUnit.Framework;

namespace test.jsmrg.ytils.com.IntegrationTest
{
    public class BasicHtmlVar2Test
    {
        [Test]
        public void TestBasicHtmlVar2()
        {
            var programRunner = new ProgramRunner(new [] { "ResTest/BasicHtmlVar2Test/MainFile2.js", "ResTest/BasicHtmlVar2Test/MainFile2.out.js" });
            var result = programRunner.Run();
            var output = programRunner.JsMrgOutput;
            
            Assert.AreEqual(ProgramRunnerExit.Done, result);
            Assert.True(output.Contains("<div class=\\\"someContainer\\\">"));
            Assert.True(output.Contains("<div class=\\\"\" + foo + \"\\\">\" + bar + \"</div>"));
            Assert.IsTrue(output.Contains("'<div class=\\'someContainer\\'><div class=\\'' + foo + '\\'>' + bar + '</div></div>';"));
        }
    }
}