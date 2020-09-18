using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
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
            ResultingFileContent = string.Empty;

            ResultingFileContent = File.ReadAllText(file);
            var regex = new Regex(@"/\*\*(jsmrg)(?:(?!\*/).)*\*/", RegexOptions.Singleline);
            var matches = regex.Matches(ResultingFileContent);

            var matchOperator = new MatchOperator();
            foreach (Match match in matches)
            {
                var inspection = matchOperator.Operate(match);
                switch (inspection.Result)
                {
                    case MatchInspectionResult.Include:
                        file = Include(file, match.Value);
                        break;
                    case MatchInspectionResult.HtmlVar:
                        file = HtmlVar(file, match.Value);
                        break;
                    case MatchInspectionResult.Error:
                        messages.Add(TerminalMessage.Create(string.Format(TerminalMessages.StoppingJsMrgRunner, match.Value), Color.Red));
                        break;
                }
            }

            return true;
        }

        private string Include(string file, string match)
        {
            return file;
        }

        private string HtmlVar(string file, string match)
        {
            return file;
        }
    }
}