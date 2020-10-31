using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using application.jsmrg.ytils.com.lib.IO;
using application.jsmrg.ytils.com.Lib.Terminal;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public class JsMrgRunner
    {
        public const string CommandPrefix = "/**jsmrg ";
        public const string CommandSuffix = "*/";
        
        public string ResultingFileContent { get; private set; }
        public string OperatedFile { get; set; }
        public string EnvironmentPath { get; set; }

        public bool Run(string inputFile, string outputFile, out List<TerminalMessage> messages)
        {
            messages = new List<TerminalMessage>();

            Console.WriteLine("inputFile: " + inputFile);
            EnvironmentPath = IoHelper.GetEnvironmentPath();
            ResultingFileContent = File.ReadAllText(inputFile);
            OperatedFile = inputFile;
            
            var regex = new Regex(@"/\*\*(jsmrg)(?:(?!\*/).)*\*/", RegexOptions.Singleline);
            var matches = regex.Matches(ResultingFileContent);

            var operationResult = OperateMatches(messages, matches);
            messages = operationResult.Messages;
            
            if (operationResult.IsOk)
            {
                if (IoHelper.WriteOutputFile(outputFile, ResultingFileContent))
                {
                    messages.Add(TerminalMessage.Create($"JsMrg successful.", Color.DarkGreen));
                    messages.Add(TerminalMessage.LineBreak());
                }
                else
                {
                    messages.Add(TerminalMessage.Create($"JsMrg complete, but failed to write output file {outputFile}.", Color.Red));
                    
                    // Last operation failed, so overwrite IsOk:
                    operationResult.IsOk = false;
                }
            }

            return operationResult.IsOk;
        }

        private OperateMatchesResult OperateMatches(List<TerminalMessage> messages, MatchCollection matches)
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
                            messages.Add(TerminalMessage.Create(string.Format(TerminalMessages.StoppingJsMrgRunner, match.Value), Color.Red));
                            error = true;
                            break;
                    }
                }
            }
            catch (JsMrgRunnerException jsMrgRunnerException)
            {
                messages.Add(TerminalMessage.Create(string.Format(jsMrgRunnerException.Message, jsMrgRunnerException.Message), Color.Red));
                error = true;
            }
            catch (Exception)
            {
                messages.Add(TerminalMessage.Create(string.Format(TerminalMessages.UnexpectedExceptionWhileJsMrgRunner, latestMatchValue), Color.Red));
                error = true;
            }

            if (false == error)
            {
                ResultingFileContent = resultingFileContent;
            }

            return new OperateMatchesResult()
            {
                Messages = messages,
                IsOk = false == error
            };
        }

        private string Include(MatchInspection matchInspection, string fileContent)
        {
            var runner = new JsMrgIncludeRunner(matchInspection, EnvironmentPath, fileContent);
            fileContent = runner.Run();

            return fileContent;
        }

        private string HtmlVar(MatchInspection matchInspection, string fileContent)
        {
            var runner = new JsMrgHtmlVarRunner(matchInspection, EnvironmentPath, fileContent);
            fileContent = runner.Run();
            
            return fileContent;
        }
    }
}