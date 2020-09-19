using System.IO;
using application.jsmrg.ytils.com.Lib.Common;

namespace application.jsmrg.ytils.com.lib.IO
{
    public static class IoHelper
    {
        public static string CreateParamFilePath(string basePath, string paramFilePath)
        {
            if (paramFilePath.StartsWith(Path.DirectorySeparatorChar))
            {
                paramFilePath = StrHelper.CutFirstChar(paramFilePath);
            }

            return basePath + paramFilePath;
        }
    }
}