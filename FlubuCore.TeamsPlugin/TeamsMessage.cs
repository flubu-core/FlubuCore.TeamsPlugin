using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FlubuCore.TeamsPlugin
{
    public class TeamsMessage
    {
        [JsonPropertyName("@type")]
        internal string Type => "MessageCard";
        [JsonPropertyName("@context")]
        internal string Context => "http://schema.org/extensions";
        [JsonPropertyName("title")]
        public virtual string Title { get; set; }
        [JsonPropertyName("text")]
        public virtual string Text { get; set; }
        [JsonPropertyName("themeColor")]
        public virtual string ThemeColor { get;  set; }
    }
}
