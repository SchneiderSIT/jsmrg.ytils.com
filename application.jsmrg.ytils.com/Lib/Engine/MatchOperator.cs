using System.Text.RegularExpressions;
using application.jsmrg.ytils.com.Lib.Common;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public class MatchOperator
    {
        public MatchInspection Operate(Match match)
        {
            var matchingStr = match.Value; // match.Value.ToLower();
            var matchInspection = MatchInspection.CreateEmpty(match, MatchInspectionType.Error);

            // TODO: Distinguish between htmlvar and include, IsEncapsulatedBy() removes whitespaces for nonEncapsulatedStr
            // TODO: This trims whitespace from htmlvar >>HtmlVar.html ''foo ''bar<< to >>HtmlVar.html''foo''bar<<
            if (StrHelper.IsEncapsulatedBy(matchingStr, JsMrgRunner.CommandPrefix, JsMrgRunner.CommandSuffix,
                out var nonEncapsulatedStr))
            {
                matchInspection.Command = AnalyseMatch(nonEncapsulatedStr, out var possibleCommandParam);
                matchInspection.CommandParams = possibleCommandParam;

                return matchInspection;
            }
            
            return matchInspection;
        }

        private MatchInspectionType AnalyseMatch(string nonEncapsulatedStr, out string possibleCommandParam)
        {
            var possibleOperation = StrHelper.GetTrimmedWhiteSpaceSplitIndex(nonEncapsulatedStr, 0).ToLower();
            possibleCommandParam = StrHelper.PopCommand(nonEncapsulatedStr);

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