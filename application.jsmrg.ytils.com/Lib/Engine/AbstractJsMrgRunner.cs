using System.Text.RegularExpressions;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public abstract class AbstractJsMrgRunner
    {
        protected MatchInspection MatchInspection;
        protected string FileContent;
        protected string OperationPath;
        
        public AbstractJsMrgRunner(MatchInspection matchInspection, string operationPath, string fileContent)
        {
            MatchInspection = matchInspection;
            FileContent = fileContent;
            OperationPath = operationPath;
        }

        public abstract string Run();
    }
}