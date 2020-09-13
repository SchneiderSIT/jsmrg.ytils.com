using System;
using System.Drawing;
using System.IO;
using application.jsmrg.ytils.com.Lib.Common;
using application.jsmrg.ytils.com.Lib.Terminal;

namespace application.jsmrg.ytils.com.lib.IO
{
    public class IoCheck : ICheck
    {
        /// <summary>
        /// If no special functionality is being addressed all parameters
        /// of JsMrg will be interpreted as file paths, either absolute or
        /// relative.
        /// </summary>
        public Check Run(string[] args)
        {
            return Check.Combine(CheckFilesExist(args), CheckFilesAccessible(args));
        }

        /// <summary>
        /// Check all given files if they are existing. Files can be
        /// addressed by absolute and relative paths.
        /// </summary>
        private Check CheckFilesExist(string[] files)
        {
            var result = Check.Create();
            
            foreach (var file in files)
            {
                if (false == File.Exists(file))
                {
                    result.CheckResult = CheckResult.Error;
                    result.Messages.Add(new TerminalMessage() { Color = Color.Red, Message = $"{file} is not a file." });
                }
            }

            return result;
        }

        /// <summary>
        /// Check all given files if they are readable and writable.
        /// </summary>
        private Check CheckFilesAccessible(string[] files)
        {
            var result = Check.Create();
            
            foreach (var file in files)
            {
                try
                {
                    using (var fileStream = new FileStream(file, FileMode.Open))
                    {
                        if (false == fileStream.CanRead && fileStream.CanWrite)
                        {
                            SetResultNotAccessible(result, file);
                        }
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    SetResultNotAccessible(result, file);
                }
                catch (Exception e)
                {
                    SetResultNotAccessible(result, file);
                }
            }

            return result;
        }

        private Check SetResultNotAccessible(Check result, string file)
        {
            result.CheckResult = CheckResult.Error;
            result.Messages.Add(new TerminalMessage()
                {Color = Color.Red, Message = $"{file} is not accessible."});

            return result;
        }
    }
}