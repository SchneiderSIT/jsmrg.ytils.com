using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using application.jsmrg.ytils.com.Lib.Common;
using application.jsmrg.ytils.com.lib.IO;
using application.jsmrg.ytils.com.Lib.Terminal;

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
        /// Runs the IoCheck.
        /// </summary>
        public bool IoCheck(out List<TerminalMessage> messages)
        {
            var ioCheck = new IoCheck();
            var ioCheckResult = ioCheck.Run(Args);
            
            messages = new List<TerminalMessage>();
            if (ioCheckResult.CheckResult == CheckResult.Error)
            {
                messages = ioCheckResult.Messages;
                
                return false;
            }

            return true;
        }
        
        private void Run()
        {
            
        }
    }
}