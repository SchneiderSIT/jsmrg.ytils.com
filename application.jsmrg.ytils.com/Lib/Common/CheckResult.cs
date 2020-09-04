using System.Collections.Generic;
using application.jsmrg.ytils.com.Lib.Terminal;

namespace application.jsmrg.ytils.com.Lib.Common
{
    public class CheckResult
    {
        public bool IsOk { get; set; }
        public List<TerminalMessage> Messages { get; set; }
        
        /// <summary>
        /// Static instance creator for this.
        /// </summary>
        public static CheckResult Create()
        {
            return new CheckResult()
            {
                IsOk = true,
                Messages = new List<TerminalMessage>()
            };
        }

        /// <summary>
        /// Combines two IoCheckResults to a single one. 
        /// </summary>
        public static CheckResult Combine(CheckResult result0, CheckResult result1)
        {
            var result = CheckResult.Create();
            
            result.Messages.AddRange(result0.Messages);
            result.Messages.AddRange(result1.Messages);

            result.IsOk = result0.IsOk && result1.IsOk;

            return result;
        }
    }
}