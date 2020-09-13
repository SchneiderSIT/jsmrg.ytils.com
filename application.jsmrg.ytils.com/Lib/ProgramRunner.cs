using System.Collections.Generic;
using application.jsmrg.ytils.com.Lib.Common;
using application.jsmrg.ytils.com.lib.IO;
using application.jsmrg.ytils.com.Lib.Terminal;
using application.jsmrg.ytils.com.Lib.Terminal.CommandParam;

namespace application.jsmrg.ytils.com.lib
{
    public class ProgramRunner
    {
        private readonly string[] Args;
    
        public ProgramRunner(string[] args)
        {
            Args = args;
        }
        
        /// <summary>
        /// Runs JsMrg. 
        /// </summary>
        public ProgramRunnerExit Run()
        {
            var helpCheck = false;
            List<TerminalMessage> helpMessages = new List<TerminalMessage>();
            
            if (0 == Args.Length)
            {
                helpCheck = true;
                HelpCheck(out helpMessages, true);
            }

            if (helpCheck || CheckResult.Apply == HelpCheck(out helpMessages))
            {
                TerminalWriter.WriteTerminalMessages(helpMessages);
                
                return ProgramRunnerExit.HelpOut;
            }
            
            if (CheckResult.Ok != IoCheck(out var terminalMessages))
            {
                TerminalWriter.WriteTerminalMessages(terminalMessages);
                
                // Bail out, we are not ready to run. 
                return ProgramRunnerExit.IoCheckOut;
            }

            return ProgramRunnerExit.Done;
        }

        /// <summary>
        /// This method checks if help output is requested or required.
        /// Help can be called explicitly or if a user uses JsMrg without
        /// params. 
        /// </summary>
        private CheckResult HelpCheck(out List<TerminalMessage> messages, bool force = false)
        {
            var helpCheck = new HelpCheck();
            var helpCheckResult = helpCheck.Run(Args);
            
            // Will be passed through empty if help is not applied.
            messages = new List<TerminalMessage>();

            if (force || helpCheckResult.CheckResult == CheckResult.Apply)
            {
                messages = AddInitialTerminalMessages(messages);
                
                foreach (var message in TerminalMessages.Help)
                {
                    messages.Add(TerminalMessage.Create(message));
                }

                return CheckResult.Apply;
            }

            return CheckResult.Ignore;
        }

        /// <summary>
        /// This method adds initial standard messages to every run of JsMrg. 
        /// </summary>
        private List<TerminalMessage> AddInitialTerminalMessages(List<TerminalMessage> messages)
        {
            foreach (var message in TerminalMessages.InitialMessages)
            {
                messages.Add(TerminalMessage.Create(message));
            }

            return messages;
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