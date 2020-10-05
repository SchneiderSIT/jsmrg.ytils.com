using System;
using System.IO;
using application.jsmrg.ytils.com.lib.IO;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public class JsMrgIncludeRunner : AbstractJsMrgRunner
    {
        public JsMrgIncludeRunner(MatchInspection match, string operationPath, string fileContent) : base(match, operationPath, fileContent) { }

        public override string Run()
        {
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

            return FileContent;
        }
    }
}