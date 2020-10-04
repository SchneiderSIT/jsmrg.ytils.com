using System.IO;
using System.Text.RegularExpressions;
using application.jsmrg.ytils.com.lib.IO;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public class JsMrgIncludeRunner : AbstractJsMrgRunner
    {
        public JsMrgIncludeRunner(MatchInspection match, string operationPath, string fileContent) : base(match, operationPath, fileContent) { }
        public override string Run()
        {
            var combinedPath =
                IoHelper.CombineOperationPathWithCommandPath(OperationPath, MatchInspection.CommandParams);
            
            // TODO: CHECK THIS 
            var includeFileContent = File.ReadAllText(combinedPath);
            
            return FileContent;
        }
    }
}