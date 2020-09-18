namespace application.jsmrg.ytils.com.Lib.Common
{
    public static class StrHelper
    {
        public static bool IsEncapsulatedBy(string val, string prefix, string suffix, out string extractedVal)
        {
            extractedVal = val;
            val = val.ToLower();
            
            if (val.StartsWith(prefix) && val.EndsWith(suffix))
            {
                extractedVal = RemovePrefix(RemoveSuffix(val, suffix), prefix);
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