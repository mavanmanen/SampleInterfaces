using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SampleInterfaces.Core.Data;
using SampleInterfaces.Models;

namespace SampleInterfaces.Repositories;

public interface IProductRepository : IRepository<Product, int> { }

[ExcludeFromCodeCoverage]
public class ProductRepository : IProductRepository
{
    private static readonly List<Product> Items = new()
    {
        new Product(0, "Item 1", 10.50m)
    };

    public Product Get(int id) => Items.Single(p => p.Id.Equals(id));

    public void AddRange(IEnumerable<Product> items) => Items.AddRange(items);
}