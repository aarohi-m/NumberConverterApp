using System;
using System.ComponentModel.DataAnnotations;

namespace NumberConverterApp.Models
{
    public class ConversionHistory(string originalValue, string convertedValue, string fromFormat, string toFormat)
    {
        [Key] // Auto-generated primary key
        public int Id { get; set; }
        public string OriginalValue { get; set; } = originalValue;
        public string ConvertedValue { get; set; } = convertedValue;
        public string FromFormat { get; set; } = fromFormat;
        public string ToFormat { get; set; } = toFormat;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // Standardized global time format
    }
}