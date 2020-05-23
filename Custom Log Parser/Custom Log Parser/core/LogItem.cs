using System;

namespace Custom_Log_Parser.core
{
    public class LogItem
    {
        public string Content { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public LogItem(string content)
        {
            Content = content;
            Parse();
        }
        public void Parse()
        {
            Date = Content.Substring(0, 23);
            var start = Content.IndexOf("]");
            var end = Content.IndexOf("ApiPZU", start);
            Type = Content.Substring(start + 1, end - start - 1).Trim();
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
