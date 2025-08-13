using System.Globalization;
namespace NumberConverterApp.Services
{
    public class NumberConverter : INumberConverter
    {
        

        private static readonly Dictionary<string, int> BaseMap =
            new(StringComparer.OrdinalIgnoreCase)
            { ["bin"]=2, ["oct"]=8, ["dec"]=10, ["hex"]=16 };

        public string Convert(string from, string to, string value)
        {
            from ??= "";
            to ??= "";
            value ??= "";

            if (from.Equals("ascii", StringComparison.OrdinalIgnoreCase))
            {
                var nums = value.Select(ch => (int)ch).ToList();
                return FormatFromInts(nums, to);
            }

            if (from.Equals("dec", StringComparison.OrdinalIgnoreCase))
            {
                var tokens = SplitTokens(value);
                var numbers = ParseDecimals(tokens);
                return FormatFromDoubles(numbers, to);
            }

            var itokens = SplitTokens(value);
            var ints = ParseNumericTokens(itokens, from);
            return FormatFromInts(ints, to);
        }

        private static IEnumerable<string> SplitTokens(string s) =>
            s.Split(new[] { ' ', ',', ';', '\n', '\r', '\t' },
                    StringSplitOptions.RemoveEmptyEntries);

        private static List<int> ParseNumericTokens(IEnumerable<string> tokens, string from)
        {
            if (!BaseMap.TryGetValue(from, out int fromBase))
                throw new ArgumentException($"Unknown 'from': {from}");

            return tokens.Select(tok =>
            {
                var norm = Normalize(tok, fromBase);
                return System.Convert.ToInt32(norm, fromBase);
            }).ToList();
        }

        private static List<double> ParseDecimals(IEnumerable<string> tokens)
        {
            var list = new List<double>();
            foreach (var t in tokens)
            {
                if (!double.TryParse(t, NumberStyles.Float, CultureInfo.InvariantCulture, out var d))
                    throw new ArgumentException($"Invalid decimal number: {t}");
                if (double.IsNaN(d) || double.IsInfinity(d))
                    throw new ArgumentException($"Unsupported decimal value: {t}");
                list.Add(d);
            }
            return list;
        }

        private static string FormatFromInts(List<int> numbers, string to)
        {
            if (BaseMap.TryGetValue(to, out int toBase))
            {
                return string.Join(" ", numbers.Select(n =>
                {
                    var s = System.Convert.ToString(n, toBase)!;
                    return toBase == 16 ? s.ToUpperInvariant() : s;
                }));
            }

            if (to.Equals("ascii", StringComparison.OrdinalIgnoreCase))
                return new string(numbers.Select(n => (char)n).ToArray());

            throw new ArgumentException($"Unknown 'to': {to}");
        }

        private static string FormatFromDoubles(List<double> numbers, string to, int fracDigits = 12)
        {
            if (to.Equals("dec", StringComparison.OrdinalIgnoreCase))
                return string.Join(" ", numbers.Select(d => d.ToString("G17", CultureInfo.InvariantCulture)));

            if (BaseMap.TryGetValue(to, out int toBase))
                return string.Join(" ", numbers.Select(d => ToBaseWithFraction(d, toBase, fracDigits)));

            if (to.Equals("ascii", StringComparison.OrdinalIgnoreCase))
            {
                return string.Join("", numbers.Select(d =>
                {
                    var rounded = Math.Round(d);
                    if (Math.Abs(d - rounded) > 1e-9)
                        throw new ArgumentException($"ASCII requires integers. Got: {d}");
                    return ((char)System.Convert.ToInt32(rounded)).ToString();
                }));
            }

            throw new ArgumentException($"Unknown 'to': {to}");
        }

        private static string ToBaseWithFraction(double value, int b, int fracDigits)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException($"Unsupported number: {value}");

            var neg = value < 0;
            value = Math.Abs(value);

            var ip = (long)Math.Floor(value);
            var fp = value - ip;

            var intPart = IntToBase(ip, b);
            if (fracDigits <= 0 || fp == 0.0)
                return neg ? "-" + intPart : intPart;

            var sb = new System.Text.StringBuilder(intPart);
            sb.Append('.');
            for (int i = 0; i < fracDigits && fp > 0.0; i++)
            {
                fp *= b;
                var digit = (int)Math.Floor(fp + 1e-15);
                sb.Append(Digit(digit));
                fp -= digit;
            }

            var s = sb.ToString();
            int dot = s.IndexOf('.');
            if (dot >= 0)
            {
                int end = s.Length - 1;
                while (end > dot && s[end] == '0') end--;
                if (end == dot) end--;
                s = s.Substring(0, end + 1);
            }

            return neg ? "-" + s : s;
        }

        private static string IntToBase(long n, int b)
        {
            if (n == 0) return "0";
            var sb = new System.Text.StringBuilder();
            while (n > 0)
            {
                int d = (int)(n % b);
                sb.Insert(0, Digit(d));
                n /= b;
            }
            return sb.ToString();
        }

        private static char Digit(int d) => (char)(d < 10 ? '0' + d : 'A' + (d - 10));

        private static string Normalize(string input, int fromBase)
        {
            input = input.Trim();
            if (fromBase == 16 && input.StartsWith("0x", StringComparison.OrdinalIgnoreCase)) return input[2..];
            if (fromBase == 2  && input.StartsWith("0b", StringComparison.OrdinalIgnoreCase)) return input[2..];
            if (fromBase == 8  && input.StartsWith("0o", StringComparison.OrdinalIgnoreCase)) return input[2..];
            return input;
        }
    }
}