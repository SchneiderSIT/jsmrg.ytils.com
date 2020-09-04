using System.Collections.Generic;
using System.Text;
using Console = Colorful.Console;

namespace application.jsmrg.ytils.com.Lib.Terminal
{
    public static class TerminalWriter
    {
        /// <summary>
        /// This method outputs a List of TerminalMessages to the console with the given
        /// colors using Colorful.Console NuGet package. The appended string lines will
        /// be returned as well but with no color information. 
        /// </summary>
        public static string WriteTerminalMessages(List<TerminalMessage> messages)
        {
            var output = new StringBuilder();
            
            foreach (var message in messages)
            {
                Console.WriteLine(message.Message, message.Color);
                output.AppendLine(message.Message);
            }

            return output.ToString();
        }
    }
}