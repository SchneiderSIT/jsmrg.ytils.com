using System;
using System.IO;
using application.jsmrg.ytils.com.Lib.Common;

namespace application.jsmrg.ytils.com.lib.IO
{
    public static class IoHelper
    {
        /// <summary>
        /// This method does a basic File.Exists(). If this fails,
        /// a path combination of the execution path and a relative
        /// interpretation of the filePath parameter. If that File.Exists() then
        /// the parameter will be adjusted and returned. 
        /// </summary>
        public static string OptionalAdjustmentToAbsolutPath(string filePath)
        {
            if (false == File.Exists(filePath))
            {
                var tFilePath = Path.GetFullPath(GetExecutionPath() + filePath);
                if (File.Exists(tFilePath))
                {
                    filePath = tFilePath;
                }
            }

            return filePath;
        }
        
        public static string GetExecutionPath()
        {
            return System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName;
        }

        public static string GetEnvironmentPath()
        {
            return Environment.CurrentDirectory;
        }
    
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