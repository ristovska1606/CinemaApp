namespace EShopAdminApplication.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string OwnerId { get; set; }
        public virtual EShopApplicationUser Owner { get; set; }

        public virtual ICollection<ProductInOrder> ProductInOrders { get; set; }
    }
}
