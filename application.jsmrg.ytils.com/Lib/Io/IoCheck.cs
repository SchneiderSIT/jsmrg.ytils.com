using System;
using System.Drawing;
using System.IO;
using application.jsmrg.ytils.com.Lib.Common;
using application.jsmrg.ytils.com.Lib.Terminal;

namespace application.jsmrg.ytils.com.lib.IO
{
    public class IoCheck
    {
        public Check CheckReadableAndAccessible(string[] files)
        {
            return Check.Combine(CheckFilesExist(files), CheckFilesAccessible(files));
        }

        public Check CheckReadableAndAccessible(string file)
        {
            var files = new[] { file };

            return CheckReadableAndAccessible(files);
        }

        public Check CheckWritable(string file)
        {
            var result = Check.Create();
            
            if (false == IsWritable(file))
            {
                result.CheckResult = CheckResult.Error;
                result.Messages.Add(new TerminalMessage() { Color = Color.Red, Message = $"{file} is not writable." });
            }

            return result;
        }
        
        private bool IsWritable(string file)
        {
            try
            {
                FileStream fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                fs.Close();
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
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
                    result = SetResultNotAccessible(result, file);
                }
                catch (Exception)
                {
                    result = SetResultNotAccessible(result, file);
                }
            }

            return result;
        }

        private Check SetResultNotAccessible(Check result, string file)
        {
            result.CheckResult = CheckResult.Error;
            result.Messages.Add(new TerminalMessage() { Color = Color.Red, Message = $"{file} is not accessible." } );

            return result;
        }
    }
}