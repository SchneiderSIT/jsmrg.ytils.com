using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using application.jsmrg.ytils.com.Lib.Terminal;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public class JsMrgRunner
    {
        public const string CommandPrefix = "/**jsmrg ";
        public const string CommandIncludeInfix = "include";
        public const string CommandHtmlVarInfix = "htmlVar";
        public const string CommandSuffix = "*/";
        
        public string ResultingFileContent { get; private set; }
        public string OperatedFile { get; set; }
        public string OperationPath { get; set; }

        public bool Run(string inputFile, string outputFile, out List<TerminalMessage> messages)
        {
            messages = new List<TerminalMessage>();

            OperationPath = Path.GetDirectoryName(inputFile) + Path.DirectorySeparatorChar;
            ResultingFileContent = File.ReadAllText(inputFile);
            OperatedFile = inputFile;
            var regex = new Regex(@"/\*\*(jsmrg)(?:(?!\*/).)*\*/", RegexOptions.Singleline);
            var matches = regex.Matches(ResultingFileContent);
            var error = false;

            error = OperateMatches(messages, matches);

            return false == error;
        }

        private bool OperateMatches(List<TerminalMessage> messages, MatchCollection matches)
        {
            var error = false;
            var matchOperator = new MatchOperator();
            var latestMatchValue = string.Empty;
            var resultingFileContent = ResultingFileContent;

            try
            {
                foreach (Match match in matches)
                {
                    var inspection = matchOperator.Operate(match);
                    latestMatchValue = inspection.Match.Value;
                    switch (inspection.Command)
                    {
                        case MatchInspectionType.Include:
                            resultingFileContent = Include(inspection, resultingFileContent);
                            break;
                        case MatchInspectionType.HtmlVar:
                            resultingFileContent = HtmlVar(inspection, resultingFileContent);
                            break;
                        case MatchInspectionType.Error:
                            messages.Add(TerminalMessage.Create(
                                string.Format(TerminalMessages.StoppingJsMrgRunner, match.Value), Color.Red));
                            error = true;
                            break;
                    }
                }
            }
            catch (JsMrgRunnerException jsMrgRunnerException)
            {
                messages.Add(TerminalMessage.Create(
                    string.Format(TerminalMessages.StoppingJsMrgRunner, jsMrgRunnerException.Message), Color.Red));
                error = true;
            }
            catch (Exception)
            {
                messages.Add(TerminalMessage.Create(
                    string.Format(TerminalMessages.UnexpectedExceptionWhileJsMrgRunner, latestMatchValue), Color.Red));
                error = true;
            }

            return error;
        }

        private string Include(MatchInspection matchInspection, string fileContent)
        {
            // OperationPath + Param'd file from matchInspection.
            // TODO
            if (matchInspection.Command == MatchInspectionType.Error)
            {
                
            }
            
            var runner = new JsMrgIncludeRunner(matchInspection, OperationPath, fileContent);
            fileContent = runner.Run();
            

            return fileContent;
        }

        private string HtmlVar(MatchInspection matchInspection, string fileContent)
        {
            return fileContent;
        }
    }
}