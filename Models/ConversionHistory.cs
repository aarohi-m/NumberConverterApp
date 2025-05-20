using System;
using System.ComponentModel.DataAnnotations;

namespace NumberConverterApp.Models
{
    public class ConversionHistory
    {
        [Key] // Auto-generated primary key
        public int Id { get; set; }
        public string OriginalValue { get; set; }
        public string ConvertedValue { get; set; }
        public string FromFormat { get; set; }
        public string ToFormat { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // Standardized global time format

        public ConversionHistory(string originalValue, string convertedValue, string fromFormat, string toFormat)
        {
            OriginalValue = originalValue;
            ConvertedValue = convertedValue;
            FromFormat = fromFormat;
            ToFormat = toFormat;
        }
    }
}