namespace NumberConverterApp.Services;
public interface INumberConverter
{
  string Convert(string input, string fromBase, string toBase);
}