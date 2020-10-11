using System;
using System.IO;
using application.jsmrg.ytils.com.Lib.Common;

namespace application.jsmrg.ytils.com.lib.IO
{
    public static class IoHelper
    {
        public static string CombineOperationPathWithCommandPath(string operationPath, string commandPath)
        {
            return Path.Combine(operationPath, commandPath);
        }

        public static bool WriteOutputFile(string filePath, string content)
        {
            try
            {
                File.WriteAllText(filePath, content);

                return true;
            }
            catch (Exception)
            {
                // Fall through. 
            }

            return false;
        }
        
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