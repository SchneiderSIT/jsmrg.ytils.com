using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using application.jsmrg.ytils.com.Lib.Common;
using application.jsmrg.ytils.com.lib.Engine;
using application.jsmrg.ytils.com.lib.IO;
using application.jsmrg.ytils.com.Lib.Terminal;
using application.jsmrg.ytils.com.Lib.Terminal.CommandParam;

namespace application.jsmrg.ytils.com.lib
{
    public class ProgramRunner
    {
        public string JsMrgOutput { get; internal set; }
        
        private readonly string[] Args;
        private string InputFile { get; set; }
        private string OutputFile  { get; set; }
    
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
                
                return ProgramRunnerExit.Help;
            }

            OutputStartupMessages();

            // If help is not requested, we are expecting exactly two parameters. 
            if (2 != Args.Length)
            {
                TerminalWriter.WriteTerminalMessage(TerminalMessage.Create(TerminalMessages.UnexpectedNumberOfParams,
                    Color.Red));
                    
                return ProgramRunnerExit.Error;
            }

            InputFile = Args[0];
            OutputFile = Args[1];
            
            if (CheckResult.Ok != IoCheck(out var terminalMessages))
            {
                TerminalWriter.WriteTerminalMessages(terminalMessages);
                
                // Bail out, we are not ready to run. 
                return ProgramRunnerExit.IoCheckOut;
            }
            
            var jsMrgRunner = new JsMrgRunner();
            var runResult = jsMrgRunner.Run(InputFile, OutputFile, out var combinedRunMessages);
            var runExit = ProgramRunnerExit.Done;
            
            if (false == runResult)
            {
                TerminalWriter.WriteTerminalMessage(TerminalMessage.Create("JsMrg run ended with error(s).",
                    Color.Red));
                runExit = ProgramRunnerExit.Error;
            }

            TerminalWriter.WriteTerminalMessages(combinedRunMessages);
            if (runExit == ProgramRunnerExit.Done)
            {
                JsMrgOutput = jsMrgRunner.ResultingFileContent;
            }

            return runExit;
        }

        private void OutputStartupMessages()
        {
            TerminalWriter.WriteTerminalMessages(TerminalMessages.InitialMessagesWOLicense);
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
            var initialMessagesWLicense = TerminalMessages.InitialMessagesWLicense1
                .Concat(TerminalMessages.InitialMessagesWLicense2).ToArray();
            
            foreach (var message in initialMessagesWLicense)
            {
                messages.Add(TerminalMessage.Create(message));    
            }

            if (force || helpCheckResult.CheckResult == CheckResult.Apply)
            {
                foreach (var message in TerminalMessages.Help)
                {
                    messages.Add(TerminalMessage.Create(message));
                }

                return CheckResult.Apply;
            }

            return CheckResult.Ignore;
        }

        /// <summary>
        /// Runs the IoCheck.
        /// </summary>
        private CheckResult IoCheck(out List<TerminalMessage> messages)
        {
            var ioCheck = new IoCheck();
            var ioCheckInputFile = ioCheck.CheckReadableAndAccessible(InputFile);
            // TODO
            // - if file exists, try to write-open 
            // - if file not exists, try to create and write-open
            var ioCheckOutputFile = ioCheck.CheckWritable(OutputFile);
            var combinedIoCheck = Check.Combine(ioCheckInputFile, ioCheckOutputFile);
            
            messages = new List<TerminalMessage>();
            if (combinedIoCheck.CheckResult == CheckResult.Error)
            {
                messages = combinedIoCheck.Messages;
                
                return CheckResult.Error;
            }

            return CheckResult.Ok;
        }
    }
}