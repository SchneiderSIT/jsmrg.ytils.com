using System.Text.RegularExpressions;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public class MatchOperator
    {
        public MatchInspection Operate(Match match)
        {
            return new MatchInspection();
        }

        private MatchInspectionResult IdentifyMatch(Match match)
        {
            var matchingStr = match.Value;

            return MatchInspectionResult.Include;
        }
    }
}