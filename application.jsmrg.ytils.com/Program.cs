using application.jsmrg.ytils.com.lib;

namespace application.jsmrg.ytils.com
{
    static class Program
    {
        static void Main(string[] args)
        {
            var programRunner = new ProgramRunner(args);
            
            var exit = programRunner.Run();
        }
    }
}