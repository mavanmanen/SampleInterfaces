namespace SampleInterfaces.Models;

public class Product
{
    public Product(int Id, string Name, decimal Price)
    {
        this.Id = Id;
        this.Name = Name;
        this.Price = Price;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}