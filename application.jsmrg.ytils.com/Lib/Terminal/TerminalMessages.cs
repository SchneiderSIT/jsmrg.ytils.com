using application.jsmrg.ytils.com.lib;

namespace application.jsmrg.ytils.com.Lib.Terminal
{
    public static class TerminalMessages
    {
        public const string StoppingJsMrgRunner = @"Failed to apply {0}, file not existing or invalid command.";

        public const string UnexpectedExceptionWhileJsMrgRunner =
            @"Unexpected exception while running JsMrg on file {0}.";
        
        public static readonly string[] InitialMessages = new string[]
        {
            "",
            "Ytils JsMrg",
            $"(c) {App.Date}, v{App.Version} by Kim Schneider",
            "Licensed under MIT, see: https://jsmrg.ytils.com/license",
        };
        
        public static readonly string[] Help = new string[]
        {
            "",
            "Usage: jsmrg file1 [, file2, [file 3] ...]",
            "",
            "Enter jsmrg --help to open this dialogue.",
            "",
            "Visit https://jsmrg.ytils.com/documentation for a full documentation.",
            "",
        };
    }
}