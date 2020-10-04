using System.Text.RegularExpressions;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public class JsMrgHtmlVarRunner : AbstractJsMrgRunner
    {
        public JsMrgHtmlVarRunner(MatchInspection matchInspection, string operationPath, string fileContent) : base(matchInspection, operationPath, fileContent) { }
        public override string Run()
        {
            // var combinedPath 
            
            return FileContent;
        }
    }
}