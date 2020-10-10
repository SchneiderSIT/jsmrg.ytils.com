using application.jsmrg.ytils.com.lib;
using NUnit.Framework;

namespace test.jsmrg.ytils.com.IntegrationTest
{
    public class HelpRequestTest
    {
        [Test]
        public void TestHelp0()
        {
            Assert.AreEqual(ProgramRunnerExit.Help,RunProgramForHelp(new string[] { }));
            Assert.AreEqual(ProgramRunnerExit.Help,RunProgramForHelp(new[] { "--help" }));
            Assert.AreEqual(ProgramRunnerExit.Help,RunProgramForHelp(new[] { "--?" }));
        }

        private ProgramRunnerExit RunProgramForHelp(string[] args)
        {
            var programRunner = new ProgramRunner(args);

            return programRunner.Run();
        }
    }
}