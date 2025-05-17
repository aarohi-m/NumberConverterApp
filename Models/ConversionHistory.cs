using System;

namespace NumberConverterApp.Models  
{
    public class ConversionHistory
    {
        public int Id { get; set; }  // Unique identifier
        public string OriginalValue { get; set; }  // Input number
        public string ConvertedValue { get; set; }  // Output result
        public string FromFormat { get; set; }  // Source format
        public string ToFormat { get; set; }  // Target format
        public DateTime Timestamp { get; set; }  // When the conversion happened

        public ConversionHistory(string originalValue, string convertedValue, string fromFormat, string toFormat)
        {
            OriginalValue = originalValue;
            ConvertedValue = convertedValue;
            FromFormat = fromFormat;
            ToFormat = toFormat;
            Timestamp = DateTime.Now;
        }
    }
}