using System.Text.RegularExpressions;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public class JsMrgIncludeRunner : AbstractJsMrgRunner
    {
        public JsMrgIncludeRunner(MatchInspection match, string fileContent) : base(match, fileContent) { }
        public override string Run()
        {
            return FileContent;
        }
    }
}