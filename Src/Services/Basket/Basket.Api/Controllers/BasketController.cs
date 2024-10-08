﻿ using Basket.Api.Data.Entities;
using Basket.Api.Data.Repositories;
using Basket.Api.GrpcServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _discountRepository;
        private readonly DiscountGrpcServices _discountGrpcService;
        //private readonly IPublishEndpoint _publishEndpoint;
        //private readonly IMapper _mapper;

        public BasketController(IBasketRepository repository, DiscountGrpcServices discountGrpcService/*, IPublishEndpoint publishEndpoint, IMapper mapper*/)
        {
            _discountRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            //_discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
            //_publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            //_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _discountRepository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }
         
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            // TODO : Communicate with Discount.Grpc
            // and Calculate latest prices of product into shopping cart
            // consume Discount Grpc
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return Ok(await _discountRepository.UpdateBasket(basket));
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _discountRepository.DeleteBasket(userName);
            return Ok();
        }

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType((int)HttpStatusCode.Accepted)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        //{
        //    // get existing basket with total price 
        //    // Create basketCheckoutEvent -- Set TotalPrice on basketCheckout eventMessage
        //    // send checkout event to rabbitmq
        //    // remove the basket

        //    // get existing basket with total price
        //    var basket = await _discountRepository.GetBasket(basketCheckout.UserName);
        //    if (basket == null)
        //    {
        //        return BadRequest();
        //    }

        //    // send checkout event to rabbitmq
        //    var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
        //    eventMessage.TotalPrice = basket.TotalPrice;
        //    await _publishEndpoint.Publish(eventMessage);

        //    // remove the basket
        //    await _discountRepository.DeleteBasket(basket.UserName);

        //    return Accepted();
        //}
    }
}
