using System;
using System.Text.RegularExpressions;

namespace Custom_Log_Parser.core
{
    public class LogItem
    {
        private static readonly string PATTERN = @"(.+)\s\[(.+)\]\s+(\w+).+LogManager\s-\s([\w]+)[^\w]*([\w]+)[^{]+";
        private static readonly string PATTERN_ERROR = @"Exception thrown in ([^:]+):([^:]+)";
        private static readonly string PATTERN_ERROR_SIGN = @"Creating\sa\ssignature\sfor\s([^:]+).+ErrorMessage"":""(.+)""";
        private static readonly string PATTERN_SIGN_REMOVE = @",?""DigitalSignature"":""([^""]+)""";
        private static readonly Regex regex = new Regex(PATTERN, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex regexError = new Regex(PATTERN_ERROR, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex regexErrorSign = new Regex(PATTERN_ERROR_SIGN, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex regexSignRemove = new Regex(PATTERN_SIGN_REMOVE, RegexOptions.Compiled);
        public string Content { get; set; }
        public string Date { get; private set; }
        public string Type { get; private set; }
        public string Group { get; private set; }
        public string Command { get; private set; }
        public string CommandType { get; private set; }

        public LogItem(string content)
        {
            Content = content;
        }
        public void Parse()
        {
            Content = regexSignRemove.Replace(Content, String.Empty);

            var matches = regex.Match(Content);

            Date = GetValueIfExists(matches, 1);
            Group = GetValueIfExists(matches, 2);
            Type = GetValueIfExists(matches, 3);

            if (Type.ToUpper().Equals("ERROR"))
            {
                matches = regexError.Match(Content);
                Command = GetValueIfExists(matches, 1);
                CommandType = GetValueIfExists(matches, 2);
            }
            else if(Content.Contains("Creating a signature for"))
            {
                Type += "_C";
                matches = regexErrorSign.Match(Content);
                Command = GetValueIfExists(matches, 1);
                CommandType = GetValueIfExists(matches, 2);
            }
            else
            {
                Command = GetValueIfExists(matches, 4);
                CommandType = GetValueIfExists(matches, 5).ToUpper();
            }
        }
        private string GetValueIfExists(Match matches, int index)
        {
            return matches.Groups.TryGetValue(index.ToString(), out var group) ? group.Value.Trim() : String.Empty;
        }
    }
}
