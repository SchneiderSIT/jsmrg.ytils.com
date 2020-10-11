using System.Collections.Generic;
using application.jsmrg.ytils.com.Lib.Terminal;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public class OperateMatchesResult
    {
        public List<TerminalMessage> Messages { get; set; }
        
        public bool IsOk { get; set; }
    }
}