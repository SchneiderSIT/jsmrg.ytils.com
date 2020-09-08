using application.jsmrg.ytils.com.Lib.Common;

namespace application.jsmrg.ytils.com.Lib.Terminal.CommandParam
{
    public class HelpCheck : ICheck
    {
        public Check Run(string[] args)
        {
            var result = Check.Create();
            
            result.CheckResult = CheckResult.Ignore;
            
            foreach (var arg in args)
            {
                if (MatchesHelpRequest(arg))
                {
                    result.CheckResult = CheckResult.Apply;
                }
            }

            return result;
        }

        private bool MatchesHelpRequest(string arg)
        {
            arg = arg.ToLower();

            return arg == "--help" ||
                   arg == "--?";
        }
    }
}