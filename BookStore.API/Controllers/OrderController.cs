using BookStore.API.DTOs.OrderDto;
using BookStore.Core;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore.Core.Models.Enums;
namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOrderDto dto)
        {
            
            var basicOrderInfo = new Order()
            {
                CustomerId = dto.CustomerId,
                OrderDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                Status = Status.Processing,
                TotalPrice = 0
            };

            await _unitOfWork.Repository<Order, int>().AddAsync(basicOrderInfo);
            await _unitOfWork.SaveChangesAsync();

            decimal totalPrice = 0;
            foreach (var item in dto.OrderDetailsList)
            {
                var book = await _unitOfWork.Repository<Book, int>().GetByIdAsync(item.BookId);

                totalPrice = totalPrice + (book.Price * item.Quantity);
                var orderDetails = new OrderDetails()
                {
                    OrderId = basicOrderInfo.Id,
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    UnitPrice = book.Price,
                    CreatedAt = DateTime.Now
                };
                if (book.UnitsInStock >= orderDetails.Quantity)
                {
                    await _unitOfWork.Repository<OrderDetails, int>().AddAsync(orderDetails);

                    book.UnitsInStock -= orderDetails.Quantity;
                    _unitOfWork.Repository<Book, int>().Update(book);
                }
                else
                {
                    _unitOfWork.Repository<Order, int>().HardDelete(basicOrderInfo);
                    return BadRequest("Invalid Quantity!");

                }
               
            }
            basicOrderInfo.TotalPrice = totalPrice;
            _unitOfWork.Repository<Order, int>().Update(basicOrderInfo);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
