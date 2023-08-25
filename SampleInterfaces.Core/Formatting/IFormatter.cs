namespace SampleInterfaces.Core.Formatting;

public interface IFormatter<in T>
{
    public string Format(T input);
}