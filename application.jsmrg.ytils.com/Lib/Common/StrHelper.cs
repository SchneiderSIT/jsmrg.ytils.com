using System;
using System.Linq;

namespace application.jsmrg.ytils.com.Lib.Common
{
    public static class StrHelper
    {
        private const string SingleWhiteSpace = " ";
        
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

        public static string PopCommand(string val)
        {
            var splits = val.Split(SingleWhiteSpace);
            
            return String.Join(string.Empty, splits.Skip(1).ToArray());
        }
        
        public static bool IsEncapsulatedBy(string val, string prefix, string suffix, out string extractedVal)
        {
            extractedVal = val;
            val = val.ToLower();
            
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