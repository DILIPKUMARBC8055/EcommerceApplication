using Basket.Application.Commads;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Entities;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    public class BasketController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IMediator mediator, IPublishEndpoint publishEndpoint,ILogger<BasketController> logger)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }
        [HttpGet]
        [Route("getShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> getShopppingCart(string username)
        {
            var query = new GetBasketByUserNameQuery(username);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound(new ApiResponse { Success = true, Message = "Cart is Empty" });
            }
            return Ok(new ApiResponse<ShoppingCartResponse> { Success = true, Message = "Cart Found", Data = result });
        }
        [HttpPost]
        [Route("CreateShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> CreateShoppingCart([FromBody] CreateShoppingCartCommand cart)
        {
            var result = await _mediator.Send(cart);
            if (result == null)
            {
                return BadRequest(new ApiResponse { Success = false, Message = "Failed to add product" });
            }
            return Ok(new ApiResponse<ShoppingCartResponse> { Success = true, Message = "Types Found", Data = result });

        }

        [HttpPut]
        [Route("UpadateShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpadateShoppingCart([FromBody] UpdateShoppingCartCommand cart)
        {
            var result = await _mediator.Send(cart);
            if (result == null)
            {
                return BadRequest(new ApiResponse { Success = false, Message = "Failed to update cart " });
            }
            return Ok(new ApiResponse { Success = true, Message = "Cart updated successfully" });

        }

        [HttpDelete]
        [Route("DeleteCart/{username}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteProduct(string username)
        {
            var query = new DeleteShoppingCartCommand(username);
            var result = await _mediator.Send(query);
            if (!result)
            {
                return BadRequest(new ApiResponse { Success = false, Message = "Cart not found to delete" });
            }
            return Ok(new ApiResponse { Success = true, Message = "Cart deleted successfully" });

        }
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basket)
        {
            //getting Basket
            var query = new GetBasketByUserNameQuery(basket.UserName);
            var resBasket = await _mediator.Send(query);
            if (resBasket == null)
            {
                return BadRequest();
            }

            //sending to RabitMq
            var eventBasket = BasketMapper.Mapper.Map<BasketCheckoutEvent>(basket);
            eventBasket.TotalPrice = basket.TotalPrice;
            try
            {
                 await _publishEndpoint.Publish(eventBasket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish event to RabbitMQ.");
                return BadRequest("Error publishing message.");
            }
            _logger.LogInformation($"Basket Published for {basket.UserName}");

            //Deleting after checkout of basket
            var deleteCommand = new DeleteShoppingCartCommand(basket.UserName);
            await _mediator.Send(deleteCommand);
            return Accepted();

        }
    }
}

