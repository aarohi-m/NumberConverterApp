public class Conversion
{
    public int Id { get; set; }
    public string InputNumber { get; set; } = string.Empty;
    public string ConvertedNumber { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}