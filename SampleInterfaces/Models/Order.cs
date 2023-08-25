using System;

namespace SampleInterfaces.Models;

public record Order(int Id, DateTime TimeStamp, int[] ProductIds);