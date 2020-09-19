namespace application.jsmrg.ytils.com.lib.Engine
{
    public class MatchInspection
    {
        public static MatchInspection CreateEmpty(MatchInspectionType initialType)
        {
            return new MatchInspection()
            {
                Type = initialType,
                CommandText = string.Empty,
                Injection = string.Empty
            };
        }
        
        public MatchInspectionType Type { get; set; }
        public string Injection { get; set; }
        public string CommandText { get; set; }
    }
}