using System;
using System.Collections.Generic;

namespace MvcDBFirst.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string? ProductName { get; set; }

    public DateTime OrderDate { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; }
}
