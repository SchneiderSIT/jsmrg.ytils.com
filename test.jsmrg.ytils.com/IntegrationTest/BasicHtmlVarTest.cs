using application.jsmrg.ytils.com.lib;
using NUnit.Framework;

namespace test.jsmrg.ytils.com.IntegrationTest
{
    public class BasicHtmlVarTest
    {
        [Test]
        public void TestBasicHtmlVar()
        {
            var programRunner = new ProgramRunner(new[]
                {"ResTest/BasicHtmlVarTest/MainFile.js", "ResTest/BasicHtmlVarTest/MainFile.out.js"});
            var result = programRunner.Run();
            var output = programRunner.JsMrgOutput;

            Assert.AreEqual(ProgramRunnerExit.Done, result);
            
            var containsSomeContainer = output.Contains("<div class=\\\"someContainer\\\">");
            Assert.True(containsSomeContainer);
            
            var expectedFooBarString = "<div class=\\\"\" + foo + \"\\\">\" + bar + \"</div>";
            var containsFooBar = output.Contains(expectedFooBarString);
            Assert.True(containsFooBar);
        }
    }
}