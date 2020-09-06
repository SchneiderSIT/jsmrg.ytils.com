using System;
using application.jsmrg.ytils.com.Lib.Terminal;

namespace application.jsmrg.ytils.com.lib
{
    public class JsmrgException : Exception
    {
        public JsmrgException(TerminalMessage terminalMessage, string excMessage) : base(excMessage)
        {
            TerminalWriter.WriteTerminalMessage(terminalMessage);
        }
    }
}