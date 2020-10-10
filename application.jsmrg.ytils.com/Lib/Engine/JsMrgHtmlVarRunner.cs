using System.Text.RegularExpressions;
using System.Threading;
using application.jsmrg.ytils.com.Lib.Common;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public class JsMrgHtmlVarRunner : AbstractJsMrgRunner
    {
        public JsMrgHtmlVarRunner(MatchInspection matchInspection, string operationPath, string fileContent) : base(matchInspection, operationPath, fileContent) { }
        public override string Run()
        {
            

            // TODO: fileContent vs. >>HtmlVar.html''foo''bar<<
            
            /*
            var combinedPath = IoHelper.CombineOperationPathWithCommandPath(OperationPath, MatchInspection.CommandParams);

            try
            {
                var includeFileContent = File.ReadAllText(combinedPath);
                FileContent = FileContent.Replace(MatchInspection.Match.Value, includeFileContent);
            }
            catch (Exception)
            {
                throw new JsMrgRunnerException($"Failed to extract contents of {combinedPath} to JsMrg command {MatchInspection.Match.Value}.");
            }
            */

            return FileContent;
        }
    }
}