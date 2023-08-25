namespace SampleInterfaces.Core.Parsing;

public interface IParser<out T>
{
    public T Parse(string input);
}