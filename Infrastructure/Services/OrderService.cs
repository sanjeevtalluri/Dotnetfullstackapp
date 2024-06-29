using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreData.Entities;
using CoreData.Entities.OrderAggregate;
using CoreData.Interfaces;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService

    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketRepository basketRepo;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo)
        {
            this.unitOfWork = unitOfWork;
            this.basketRepo = basketRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            var basket = await basketRepo.GetBasketAsync(basketId);
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            var subtotal = items.Sum(item => item.Price * item.Quantity);
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);
            unitOfWork.Repository<Order>().Add(order);
            var result = await unitOfWork.Complete();
            if (result <= 0) return null;
            await basketRepo.DeleteBasketAsync(basketId);
            return order;

        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}