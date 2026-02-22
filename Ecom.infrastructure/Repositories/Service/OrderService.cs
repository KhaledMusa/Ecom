using Ecom.Core.Entities.Order;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Specifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositories.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWOrk _unitOfWork;

        public OrderService(IBasketRepository basketRepo, IUnitOfWOrk unitOfWork)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // 1. Get basket from the repo
            var basket = await _basketRepo.GetBasketAsync(basketId);
            if (basket == null) return null;

            // 2. Get items from the product repo
            var items = new List<OrderItem>();
            var productRepository = _unitOfWork.Repositry<Product>();
            foreach (var item in basket.Items)
            {
                var productItem = await productRepository.GetByIdAsync(item.Id);
                var photoKey = productItem.Photos?.FirstOrDefault()?.ImageName ?? "";
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, photoKey);
                var orderItem = new OrderItem(itemOrdered, productItem.NewPrice, item.Quantity);
                items.Add(orderItem);
            }

            // 3. Get delivery method from repo
            var deliveryMethod = await _unitOfWork.Repositry<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // 4. Calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            // 5. Create order
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal, "");
            
            await _unitOfWork.Repositry<Order>().AddAsync(order);

            // 6. Save to db
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // 7. delete basket
            await _basketRepo.DeleteBasketAsync(basketId);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repositry<DeliveryMethod>().GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            return await _unitOfWork.Repositry<Order>().GetByIdAsync(id); // Usually needs Specification with Includes
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            return await _unitOfWork.Repositry<Order>().GetAllAsync(); // Further filter by email needed with Specification
        }
    }
}
