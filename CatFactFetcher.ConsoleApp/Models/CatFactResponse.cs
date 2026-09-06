using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CatFactFetcher.ConsoleApp.Models
{
    public class CatFactResponse
    {
        [JsonPropertyName("fact")]
        public string Fact { get; set; } = string.Empty;
        [JsonPropertyName("length")]
        public int Length { get; set; }
    }
}
