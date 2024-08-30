using System;
using System.Collections.Generic;

namespace MvcDBFirst.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? Email { get; set; }

    public long ContactNo { get; set; }

    public string Location { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
