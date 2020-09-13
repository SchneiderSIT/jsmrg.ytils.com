using application.jsmrg.ytils.com.lib;

namespace application.jsmrg.ytils.com.Lib.Terminal
{
    public static class TerminalMessages
    {
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