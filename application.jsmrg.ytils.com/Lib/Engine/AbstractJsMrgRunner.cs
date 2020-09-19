using System.Text.RegularExpressions;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public abstract class AbstractJsMrgRunner
    {
        protected MatchInspection MatchInspection;
        protected string FileContent;
        
        public AbstractJsMrgRunner(MatchInspection matchInspection, string fileContent)
        {
            MatchInspection = matchInspection;
            FileContent = fileContent;
        }

        public abstract string Run();
    }
}