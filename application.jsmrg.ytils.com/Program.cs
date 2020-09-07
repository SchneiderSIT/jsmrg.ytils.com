using application.jsmrg.ytils.com.lib;
using application.jsmrg.ytils.com.Lib.Terminal;

namespace application.jsmrg.ytils.com
{
    class Program
    {
        static void Main(string[] args)
        {
            var programRunner = new ProgramRunner(args);
            programRunner.Run();
        }
    }
}