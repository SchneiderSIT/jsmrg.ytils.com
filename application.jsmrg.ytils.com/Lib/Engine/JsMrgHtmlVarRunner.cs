using System.Collections.Generic;
using System.IO;
using application.jsmrg.ytils.com.Lib.Common;
using application.jsmrg.ytils.com.lib.IO;

namespace application.jsmrg.ytils.com.lib.Engine
{
    public class JsMrgHtmlVarRunner : AbstractJsMrgRunner
    {
        public static readonly string[] HtmlVarPrefixes = { HtmlVarPrefixSingleQuote, HtmlVarPrefixDoubleQuote };
        
        private const string HtmlVarPrefixSingleQuote = "%s%";
        private const string HtmlVarPrefixDoubleQuote = "%d%";
        private const string VarParamEncapsulationPrefix = "{{";
        private const string VarParamEncapsulationSuffix = "}}";

        private bool LinebreakToSingleWhiteSpace;
        private bool EscapeDoubleQuotes;
        private bool EscapeSingleQuotes;

        public JsMrgHtmlVarRunner(MatchInspection matchInspection, string operationPath, string fileContent) : base(matchInspection, operationPath, fileContent) { }
        public override string Run()
        {
            var matchedStr = MatchInspection.Match.Value;
            var extractedCommandParamAndVars = StrHelper.RemoveHtmlVarCommand(matchedStr);
            LinebreakToSingleWhiteSpace = IsLineBreakToSingleWhitespaceCommanded(extractedCommandParamAndVars, out extractedCommandParamAndVars);
            EscapeDoubleQuotes = IsEscapeDoubleQuotesCommanded(extractedCommandParamAndVars, out extractedCommandParamAndVars);
            EscapeSingleQuotes = IsEscapeSingleQuotesCommanded(extractedCommandParamAndVars, out extractedCommandParamAndVars);
            
            var fileToInclude = GetFilePathToInclude(extractedCommandParamAndVars);
            
            var iOCheck = new IoCheck();
            var checkResult = iOCheck.CheckReadableAndAccessible(fileToInclude);

            if (CheckResult.Error == checkResult.CheckResult)
            {
                throw new JsMrgRunnerException($"{fileToInclude} commanded by {matchedStr} is not readable.");
            }

            List<string> varParams;
            VerifyVarCommands(extractedCommandParamAndVars, out varParams);
            
            var fileToIncludeContent = File.ReadAllText(fileToInclude);
            fileToIncludeContent = ApplyEscapings(fileToIncludeContent);
            fileToIncludeContent = OperateIncludeContentWithVarParams(fileToIncludeContent, varParams);
            fileToIncludeContent = ReduceToOneLine(fileToIncludeContent);

            FileContent = FileContent.Replace(matchedStr, fileToIncludeContent);

            return FileContent;
        }

        private string ApplyEscapings(string fileToIncludeContent)
        {
            if (EscapeDoubleQuotes)
            {
                fileToIncludeContent = fileToIncludeContent.Replace("\"", "\\\"");
            }
            if (EscapeSingleQuotes)
            {
                fileToIncludeContent = fileToIncludeContent.Replace("'", "\\'");
            }

            return fileToIncludeContent;
        }

        private string ReduceToOneLine(string fileToIncludeContent)
        {
            var replacement = LinebreakToSingleWhiteSpace ? StrHelper.SingleWhiteSpace : string.Empty;

            fileToIncludeContent = StrHelper.RemoveLineBreaks(fileToIncludeContent, replacement);

            return fileToIncludeContent;
        }

        private bool IsEscapeSingleQuotesCommanded(string commandLine, out string cutCommandLine)
        {
            return OperateOptionalHtmlVarCommand(JsMrgHtmLVarAdditionalCommand.EscSingleQuotes, commandLine, out cutCommandLine);
        }
        
        private bool IsEscapeDoubleQuotesCommanded(string commandLine, out string cutCommandLine)
        {
            return OperateOptionalHtmlVarCommand(JsMrgHtmLVarAdditionalCommand.EscDoubleQuotes, commandLine, out cutCommandLine);
        }

        private bool IsLineBreakToSingleWhitespaceCommanded(string commandLine, out string cutCommandLine)
        {
            return OperateOptionalHtmlVarCommand(JsMrgHtmLVarAdditionalCommand.Lb2Space, commandLine, out cutCommandLine);
        }
        
        private bool OperateOptionalHtmlVarCommand(string commandToCheck, string commandLine, out string cutCommandLine)
        {
            cutCommandLine = commandLine;
            
            if (commandLine.Contains(commandToCheck))
            {
                cutCommandLine = cutCommandLine.Replace(commandToCheck, string.Empty);
                cutCommandLine = cutCommandLine.Trim();

                return true;
            }

            return false;
        }

        private string OperateIncludeContentWithVarParams(string content, List<string> varParams)
        {
            foreach (var varParam in varParams)
            {
                if (varParam.StartsWith(HtmlVarPrefixSingleQuote))
                {
                    var varName = StrHelper.RemovePrefix(varParam, HtmlVarPrefixSingleQuote);

                    content = content.Replace(CreateVarPlaceholder(varName), $"' + {varName} + '");
                }
                else // HtmlVarPrefixDoubleQuote
                {
                    var varName = StrHelper.RemovePrefix(varParam, HtmlVarPrefixDoubleQuote);

                    content = content.Replace(CreateVarPlaceholder(varName), $"\" + {varName} + \"");                    
                }
            }
            
            return content;
        }

        private string CreateVarPlaceholder(string varName)
        {
            return VarParamEncapsulationPrefix + varName + VarParamEncapsulationSuffix;
        }

        /// <summary>
        /// This method verifies if all html vars that should be handled in given file have the right prefix.
        /// </summary>
        private void VerifyVarCommands(string commandParamAndVars, out List<string> varParams) 
        {
            varParams = StrHelper.GetWhiteSpaceSplittedStrArr(commandParamAndVars);
            if (varParams.Count > 1)
            {
                varParams.RemoveAt(0);
                foreach (var varParam in varParams)
                {
                    if (false == StrHelper.IsPrefixedByOneOfPrefixes(varParam, HtmlVarPrefixes))
                    {
                        throw new JsMrgRunnerException($"htmlvar var replacements have to start with either <''> or <\"\">, found: {varParam} in command {MatchInspection.Match.Value}");
                    }
                }
            }
            
            // Else: Nothing to do. 
        }

        private string GetFilePathToInclude(string commandParamAndVars)
        {
            return Path.Combine(OperationPath, StrHelper.GetWhiteSpaceSplittedStrArr(commandParamAndVars)[0]);
        }
    }
}