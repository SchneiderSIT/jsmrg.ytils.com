using System;
using System.Collections.Generic;
using application.jsmrg.ytils.com.Lib.Common;
using application.jsmrg.ytils.com.lib.IO;
using application.jsmrg.ytils.com.Lib.Terminal;
using application.jsmrg.ytils.com.Lib.Terminal.Help;

namespace application.jsmrg.ytils.com.lib
{
    public class ProgramRunner
    {
        private string[] Args;
    
        public ProgramRunner(string[] args)
        {
            Args = args;
        }
        
        /// <summary>
        /// Runs JsMrg. 
        /// </summary>
        public ProgramRunnerExit Run()
        {
            // if (CheckResult.Apply == )
            
            if (CheckResult.Ok != IoCheck(out var terminalMessages))
            {
                TerminalWriter.WriteTerminalMessages(terminalMessages);
                
                // Bail out, we are not ready to run. 
                return ProgramRunnerExit.IoCheckOut;
            }

            return ProgramRunnerExit.Done;
        }

        private CheckResult HelpCheck(out List<TerminalMessage> messages)
        {
            var helpCheck = new HelpCheck();
            var helpCheckResult = helpCheck.Run(Args);

            messages = new List<TerminalMessage>();
            
            // TODO
            return CheckResult.Ignore;
        }

        /// <summary>
        /// Runs the IoCheck.
        /// </summary>
        private CheckResult IoCheck(out List<TerminalMessage> messages)
        {
            var ioCheck = new IoCheck();
            var ioCheckResult = ioCheck.Run(Args);
            
            messages = new List<TerminalMessage>();
            if (ioCheckResult.CheckResult == CheckResult.Error)
            {
                messages = ioCheckResult.Messages;
                
                return CheckResult.Error;
            }

            return CheckResult.Ok;
        }
    }
}