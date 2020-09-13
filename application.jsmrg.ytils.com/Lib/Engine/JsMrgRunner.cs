using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using application.jsmrg.ytils.com.Lib.Terminal;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public class JsMrgRunner
    {
        public string ResultingFileContent { get; private set; }

        public bool Run(string file, out List<TerminalMessage> messages)
        {
            messages = new List<TerminalMessage>();
            ResultingFileContent = string.Empty;

            ResultingFileContent = File.ReadAllText(file);
            var regex = new Regex(@"/\*\*(jsmrg)(?:(?!\*/).)*\*/", RegexOptions.Singleline);
            var matches = regex.Matches(ResultingFileContent);

            
            return true;
        }
    }
}