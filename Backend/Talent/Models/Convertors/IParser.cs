namespace Talent.Models.Convertors
{
    public interface IParser
    {
        bool TryParse(string data, out object? value);
    }
}