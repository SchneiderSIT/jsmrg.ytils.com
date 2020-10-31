namespace application.jsmrg.ytils.com.lib.Engine
{
    public abstract class AbstractJsMrgRunner
    {
        protected readonly MatchInspection MatchInspection;
        protected readonly string OperationPath;
        protected string FileContent;

        protected AbstractJsMrgRunner(MatchInspection matchInspection, string operationPath, string fileContent)
        {
            MatchInspection = matchInspection;
            FileContent = fileContent;
            OperationPath = operationPath;
        }

        public abstract string Run();
    }
}