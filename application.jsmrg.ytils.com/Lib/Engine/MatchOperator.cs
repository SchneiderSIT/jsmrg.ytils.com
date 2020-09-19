using System.Text.RegularExpressions;
using application.jsmrg.ytils.com.Lib.Common;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public class MatchOperator
    {
        public MatchInspection Operate(Match match)
        {
            var matchingStr = match.Value.ToLower();
            var matchInspection = MatchInspection.CreateEmpty(MatchInspectionType.Error);

            if (StrHelper.IsEncapsulatedBy(matchingStr, JsMrgRunner.CommandPrefix, JsMrgRunner.CommandSuffix,
                out var nonEncapsulatedStr))
            {
                matchInspection.Type = AnalyseMatch(nonEncapsulatedStr);
                matchInspection.CommandText = nonEncapsulatedStr;

                return matchInspection;
            }
            
            return matchInspection;
        }

        private MatchInspectionType AnalyseMatch(string nonEncapsulatedStr)
        {
            var possibleOperation = StrHelper.GetTrimmedWhiteSpaceSplitIndex(nonEncapsulatedStr, 0).ToLower();

            if (possibleOperation == JsMrgCommand.Include)
            {
                return MatchInspectionType.Include;
            }
            
            if (possibleOperation == JsMrgCommand.HtmlVar)
            {
                return MatchInspectionType.HtmlVar;
            }

            return MatchInspectionType.Error;
        }
    }
}