namespace MvcEF.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string? ProductName { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerID { get; set; }

        //Navigation property
        public Customer Customer { get; set; }




    }
}
