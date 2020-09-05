using application.jsmrg.ytils.com.lib;
using application.jsmrg.ytils.com.Lib.Terminal;

namespace application.jsmrg.ytils.com
{
    class Program
    {
        static void Main(string[] args)
        {
            var programRunner = new ProgramRunner(args);
            

            if (!programRunner.IoCheck(out var terminalMessages))
            {
                TerminalWriter.WriteTerminalMessages(terminalMessages);
                
                // Bail out, we are not ready to run. 
                return;
            }
        }
    }
}