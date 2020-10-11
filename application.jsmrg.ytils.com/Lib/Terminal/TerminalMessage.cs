using System.Drawing;
using System.Dynamic;

namespace application.jsmrg.ytils.com.Lib.Terminal
{
    public class TerminalMessage
    {
        public static TerminalMessage LineBreak()
        {
            return Create(string.Empty);
        }
        
        public static TerminalMessage Create(string message)
        {
            return new TerminalMessage() { Message = message };
        }

        public static TerminalMessage Create(string message, Color color)
        {
            return new TerminalMessage() { Message = message, Color = color };
        }
        
        public string Message { get; set; }

        public Color Color { get; set; }
    }
}