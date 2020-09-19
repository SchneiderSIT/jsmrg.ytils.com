using System.Text.RegularExpressions;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public class JsMrgHtmlVarRunner : AbstractJsMrgRunner
    {
        public JsMrgHtmlVarRunner(MatchInspection matchInspection, string fileContent) : base(matchInspection, fileContent) { }
        public override string Run()
        {
            return FileContent;
        }
    }
}