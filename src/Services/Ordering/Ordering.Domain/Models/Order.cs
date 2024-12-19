using Ordering.Domain.Enums;
using Ordering.Domain.Events;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        public CustomerId CustomerId { get; private set; } = default!;
        public OrderName OrderName { get; private set; } = default!;
        private readonly List<OrderItem> _orderItems = [];

        public static Order Create(OrderId orderId,
            CustomerId customerId,
            OrderName orderName,
            Address shippingAddress,
            Address billingAddress,
            Payment payment,
            OrderStatus status)
        {
            var order = new Order
            {
                Id = orderId,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment,
                Status = status,
                CustomerId = customerId,
                OrderName = orderName,
            };

            order.AddDomainEvent(new OrderCreatedEvent(order));
            return order;
        }

        public void Update(
            OrderName orderName,
            Address shippingAddress,
            Address billingAddress,
            Payment payment,
            OrderStatus status)
        {
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            Status = status;
            OrderName = orderName;

            AddDomainEvent(new OrderUpdatedEvent(this));
        }

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

        public void Add(ProductId productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
            var orderItem = new OrderItem(Id, productId, quantity, price);
            _orderItems.Add(orderItem);
        }

        public void Remove(ProductId productId)
        {
            _orderItems.Remove(_orderItems.FirstOrDefault(s => s.ProductId == productId));
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