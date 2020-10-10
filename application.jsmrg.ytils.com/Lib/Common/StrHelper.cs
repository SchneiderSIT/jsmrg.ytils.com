using System;
using System.Linq;
using application.jsmrg.ytils.com.lib.Engine;

namespace application.jsmrg.ytils.com.Lib.Common
{
    public static class StrHelper
    {
        private const string SingleWhiteSpace = " ";

        public static string CutFirstChar(string val)
        {
            return val.Substring(1);
        }
        
        public static string GetTrimmedWhiteSpaceSplitIndex(string val, int index)
        {
            if (null != val)
            {
                var splits = val.Split(SingleWhiteSpace);

                if (splits.Length >= index)
                {
                    return splits[index].Trim();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// This method removes the jsmrg command alongside with htmlvar command infix. 
        /// </summary>
        public static string RemoveHtmlVarCommand(string val)
        {
            var originalVal = val;
            val = val.Replace(JsMrgRunner.CommandPrefix, String.Empty);
            val = val.Replace(JsMrgRunner.CommandSuffix, String.Empty);
            val = val.Trim();
            
            if (val.StartsWith(JsMrgCommand.HtmlVar))
            {
                val = RemovePrefix(val, JsMrgCommand.HtmlVar);
                val = val.Trim();

                return val;
            }
            
            return originalVal;
        }
        
        public static string PopCommand(string val)
        {
            var splits = val.Split(SingleWhiteSpace);
            
            return String.Join(string.Empty, splits.Skip(1).ToArray());
        }
        
        public static bool IsEncapsulatedBy(string val, string prefix, string suffix, out string extractedVal)
        {
            extractedVal = val;
            
            if (val.StartsWith(prefix) && val.EndsWith(suffix))
            {
                extractedVal = RemovePrefix(RemoveSuffix(val, suffix), prefix);

                return true;
            }

            return false;
        }

        private static string RemovePrefix(string val, string prefix)
        {
            return val.Remove(0, prefix.Length);
        }

        private static string RemoveSuffix(string val, string suffix)
        {
            return val.Remove(val.Length - suffix.Length, suffix.Length);
        }
    }
}