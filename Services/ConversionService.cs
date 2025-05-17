public class ConversionService
{
    public string ConvertNumber(string value, string fromBase, string toBase)
    {
        try
        {
            Dictionary<string, Func<string, string>> conversionMethods = new()
            {
                { "binary-to-decimal", v => Convert.ToInt32(v, 2).ToString() },
                { "octal-to-decimal", v => Convert.ToInt32(v, 8).ToString() },
                { "hexadecimal-to-decimal", v => Convert.ToInt32(v.ToUpper(), 16).ToString() },
                { "decimal-to-binary", v => int.TryParse(v, out var n) ? Convert.ToString(n, 2) : "Invalid input" },
                { "decimal-to-octal", v => int.TryParse(v, out var n) ? Convert.ToString(n, 8) : "Invalid input" },
                { "decimal-to-hexadecimal", v => int.TryParse(v, out var n) ? Convert.ToString(n, 16) : "Invalid input" }
            };

            string key = $"{fromBase}-to-{toBase}";
            return conversionMethods.ContainsKey(key) ? conversionMethods[key].Invoke(value) : "Invalid conversion";
        }
        catch (Exception)
        {
            return "Error in conversion";
        }
    }
}