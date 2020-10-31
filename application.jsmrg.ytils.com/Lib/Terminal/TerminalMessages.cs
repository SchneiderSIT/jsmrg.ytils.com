using application.jsmrg.ytils.com.lib;

namespace application.jsmrg.ytils.com.Lib.Terminal
{
    public static class TerminalMessages
    {
        public const string GeneralFileAccessErrorMessageInitial = @"One or more IO error(s) occured.";
        public const string GeneralFileAccessErrorMessageExecutionPath = "Current execution path is {0}.";
        public const string GeneralFileAccessErrorMessageEnvironmentPath = "Current environment path is {0}.";
        
        public const string StoppingJsMrgRunner = @"Failed to apply {0}, file not existing or invalid command.";

        public const string UnexpectedExceptionWhileJsMrgRunner =
            @"Unexpected exception while running JsMrg on file {0}.";

        // ReSharper disable once InconsistentNaming
        public static readonly string[] InitialMessagesWOLicense = new string[]
        {
            "",
            "Ytils JsMrg",
            $"(c) {App.Date}, v{App.Version} by Kim Schneider"
        };

        public static string[] InitialMessagesWLicense1 = InitialMessagesWOLicense;

        public static string[] InitialMessagesWLicense2 = new string[]
        {
            "Licensed under MIT, see: https://jsmrg.ytils.com/license"
        };
        
        public static readonly string[] Help = new string[]
        {
            "",
            "Usage: jsmrg <input-file> <output-file>",
            "Enter jsmrg --help to open this dialogue.",
            "Visit https://jsmrg.ytils.com/documentation for a full documentation.",
            "",
        };

        public const string UnexpectedNumberOfParams = "JsMrg expects exactly two params to be launched with.";
    }
}