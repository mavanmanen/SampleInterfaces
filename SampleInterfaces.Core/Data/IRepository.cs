namespace SampleInterfaces.Core.Data;

public interface IRepository<T, in TId>
{
    public T Get(TId id);
    public void AddRange(IEnumerable<T> item);
}