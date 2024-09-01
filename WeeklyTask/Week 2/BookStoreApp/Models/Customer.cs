using System;
using System.Collections.Generic;

namespace BookStoreApp.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? Password { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public int? Role { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
