using System.Text.RegularExpressions;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public class MatchInspection
    {
        public static MatchInspection CreateEmpty(Match match, MatchInspectionType initialType)
        {
            return new MatchInspection()
            {
                Match = match,
                Command = initialType,
                CommandParams = string.Empty,
                Injection = string.Empty
            };
        }
        
        public Match Match { get; set; }
        public MatchInspectionType Command { get; set; }
        public string Injection { get; set; }
        public string CommandParams { get; set; }
        
    }
}