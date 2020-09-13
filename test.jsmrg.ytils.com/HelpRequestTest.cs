using application.jsmrg.ytils.com.lib;
using NUnit.Framework;

namespace test.jsmrg.ytils.com
{
    public class HelpRequestTest
    {
        [Test]
        public void TestHelp0()
        {
            Assert.AreEqual(ProgramRunnerExit.Help,RunProgramForHelp(new string[] { }));
            Assert.AreEqual(ProgramRunnerExit.Help,RunProgramForHelp(new string[] { "--help" }));
            Assert.AreEqual(ProgramRunnerExit.Help,RunProgramForHelp(new string[] { "--?" }));
        }

        private ProgramRunnerExit RunProgramForHelp(string[] args)
        {
            var programRunner = new ProgramRunner(args);

            return programRunner.Run();
        }
    }
}