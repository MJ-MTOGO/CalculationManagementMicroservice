namespace CalculationManagementService.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid RestaurantId { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime OrderDateTime { get; private set; }
        public DateTime DeliveringDateTime { get; private set; }
        public Guid CustomerId { get; private set; }
        public string DeliveringAddress { get; private set; }

        public Order(Guid id, Guid restaurantId, decimal totalPrice, DateTime orderDateTime, DateTime deliveringDateTime, Guid customerId, string deliveringAddress)
        {
            Id = id;
            RestaurantId = restaurantId;
            TotalPrice = totalPrice;
            OrderDateTime = orderDateTime;
            DeliveringDateTime = deliveringDateTime;
            CustomerId = customerId;
            DeliveringAddress = deliveringAddress;
        }

    }
}
