using System.Text.RegularExpressions;
using application.jsmrg.ytils.com.Lib.Common;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public class MatchOperator
    {
        public MatchInspection Operate(Match match)
        {
            var what = IdentifyMatch(match);
            
            return new MatchInspection();
        }

        private MatchInspectionResult IdentifyMatch(Match match)
        {
            var matchingStr = match.Value.ToLower();
            string nonEncapsulatedStr;
            if (StrHelper.IsEncapsulatedBy(match.Value, JsMrgRunner.CommandPrefix, JsMrgRunner.CommandSuffix,
                out nonEncapsulatedStr))
            {
                
            }
            
            return MatchInspectionResult.Include;
        }
    }
}