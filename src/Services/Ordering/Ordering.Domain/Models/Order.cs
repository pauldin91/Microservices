using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        public CustomerId CustomerId { get; private set; } = default!;
        public OrderName OrderName { get; private set; } = default!;
        private readonly List<OrderItem> _orderItems = [];
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public Address ShippingAddress { get; set; } = default!;
        public Address BillingAddress { get; set; } = default!;

        public Payment Payment { get; set; } = default!;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public decimal TotalPrice
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { }
        }

        public void AddOrderItem(OrderItem item)
        {
            _orderItems.Add(item);
        }

        public void RemoveOrderItem(OrderItemId itemId)
        {
            var itemToRemove = _orderItems.FirstOrDefault(s => s.Id == itemId);
            if (itemToRemove != null)
            {
                _orderItems.Remove(itemToRemove);
            }
        }
    }
}