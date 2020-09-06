using System.Collections.Generic;
using System.Drawing;
using application.jsmrg.ytils.com.lib;
using application.jsmrg.ytils.com.Lib.Terminal;

namespace application.jsmrg.ytils.com.Lib.Common
{
    public class Check
    {
        public CheckResult CheckResult { get; set; }
        public List<TerminalMessage> Messages { get; set; }
        
        /// <summary>
        /// Static instance creator for this.
        /// </summary>
        public static Check Create()
        {
            return new Check()
            {
                CheckResult = CheckResult.Ok,
                Messages = new List<TerminalMessage>()
            };
        }

        /// <summary>
        /// Combines two IoCheckResults to a single one. 
        /// </summary>
        public static Check Combine(Check result0, Check result1)
        {
            var result = Check.Create();
            
            ExpectOkOrError(result0);
            ExpectOkOrError(result1);
            
            // Cannot reach this if one of both Expect*() methods
            // does not pass. 
            result.CheckResult = CombineOkAndError(result0, result1);
            result.Messages.AddRange(result0.Messages);
            result.Messages.AddRange(result1.Messages);
            
            return result;
        }

        private static CheckResult CombineOkAndError(Check result0, Check result1)
        {
            if (result0.CheckResult == CheckResult.Ok && result1.CheckResult == CheckResult.Ok)
            {
                return CheckResult.Ok;
            }

            return CheckResult.Error;
        }

        private static void ExpectOkOrError(Check result)
        {
            if (!(result.CheckResult == CheckResult.Ok || result.CheckResult == CheckResult.Error))
            {
                var message = "Unexpected Runtime error at adf09599-a0ea-4929-878a-f3e9a26f335a. Please report to developer on ytils.com.";
                var terminalMessage = new TerminalMessage();
                terminalMessage.Color = Color.Red;
                terminalMessage.Message = message;
                
                throw new JsmrgException(terminalMessage, message);
            }
        }
    }
}