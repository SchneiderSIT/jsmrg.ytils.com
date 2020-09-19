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

        public bool Run(string file, out List<TerminalMessage> messages)
        {
            messages = new List<TerminalMessage>();

            ResultingFileContent = File.ReadAllText(file);
            var regex = new Regex(@"/\*\*(jsmrg)(?:(?!\*/).)*\*/", RegexOptions.Singleline);
            var matches = regex.Matches(ResultingFileContent);
            var error = false;

            error = OperateMatches(messages, matches, file);

            return false == error;
        }

        private bool OperateMatches(List<TerminalMessage> messages, MatchCollection matches, string file)
        {
            var error = false;
            var matchOperator = new MatchOperator();
            
            foreach (Match match in matches)
            {
                var inspection = matchOperator.Operate(match);
                switch (inspection.Command)
                {
                    case MatchInspectionType.Include:
                        file = Include(inspection, ResultingFileContent);
                        break;
                    case MatchInspectionType.HtmlVar:
                        file = HtmlVar(inspection, ResultingFileContent);
                        break;
                    case MatchInspectionType.Error:
                        messages.Add(TerminalMessage.Create(string.Format(TerminalMessages.StoppingJsMrgRunner, match.Value), Color.Red));
                        error = true;
                        break;
                }
            }

            return error;
        }

        private string Include(MatchInspection matchInspection, string fileContent)
        {
            var runner = new JsMrgIncludeRunner(matchInspection, fileContent);
            

            return fileContent;
        }

        private string HtmlVar(MatchInspection matchInspection, string fileContent)
        {
            return fileContent;
        }
    }
}