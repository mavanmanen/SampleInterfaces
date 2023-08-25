using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using SampleInterfaces.Core.Parsing;
using SampleInterfaces.Models;

namespace SampleInterfaces.Parsing;

public interface IOrderParser : IParser<IEnumerable<Order>> { }

public class OrderParser : IOrderParser
{
    public IEnumerable<Order> Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentNullException(nameof(input));
        }
        
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            NewLine = Environment.NewLine
        };

        using var reader = new StringReader(input);
        using var csv = new CsvReader(reader, config);

        var retVal = new List<Order>();

        while (csv.Read())
        {
            var id = csv.GetField<int>(0);
            var timeStamp = DateTime.Parse(csv.GetField<string>(1));
            var productIds = csv.GetField<string>(2).Split(' ').Select(int.Parse).ToArray();

            retVal.Add(new Order(id, timeStamp, productIds));
        }

        return retVal;
    }
}