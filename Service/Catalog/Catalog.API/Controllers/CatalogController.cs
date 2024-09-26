using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    public class CatalogController : ApiController
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> getProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {

                return NotFound(new ApiResponse { Success = false, Message = "Product not found" });
            }
            return Ok(new ApiResponse<ProductResponse> { Success = true, Message = "Product Found", Data = result });

        }

        [HttpGet]
        [Route("[action]/{name}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> getProductByName(string name)
        {
            var query = new GetProductByNameQuery(name);
            var result = await _mediator.Send(query);
            if (result == null)
            {

                return NotFound(new ApiResponse { Success = false, Message = $"Product with name {name} not found" });
            }
            return Ok(new ApiResponse<IList<ProductResponse>> { Success = true, Message = "Products Found", Data = result });

        }
        [HttpGet]
        [Route("[action]/{brand}", Name = "GetProductByBrand")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProductByBrand(string brand)
        {
            var query = new GetProductsByBrandQuery(brand);
            var result = await _mediator.Send(query);
            if (result == null)
            {

                return NotFound(new ApiResponse { Success = false, Message = $"Product with brand {brand} not found" });
            }
            return Ok(new ApiResponse<IList<ProductResponse>> { Success = true, Message = "Products Found", Data = result });

        }

        [HttpGet]
        [Route("[action]/{type}", Name = "GetProductByType")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProductByType(string type)
        {
            var query = new GetProductByTypeQuery(type);
            var result = await _mediator.Send(query);
            if (result == null)
            {

                return NotFound(new ApiResponse { Success = false, Message = $"Product with type {type} not found" });
            }
            return Ok(new ApiResponse<IList<ProductResponse>> { Success = true, Message = "Products Found", Data = result });

        }

        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductQuery();
            var result = await _mediator.Send(query);
            if (result == null)
            {

                return NotFound(new ApiResponse { Success = false, Message = $"Product not found" });
            }
            return Ok(new ApiResponse<IList<ProductResponse>> { Success = true, Message = "Products Found", Data = result });

        }

        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(IList<BrandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllBrands()
        {
            var query = new GetAllBrandQuery();
            var result = await _mediator.Send(query);
            if (result == null)
            {

                return NotFound(new ApiResponse { Success = false, Message = $"Brands not found" });
            }
            return Ok(new ApiResponse<IList<BrandResponse>> { Success = true, Message = "Brands Found", Data = result });

        }


        [HttpGet]
        [Route("GetAllTypes")]
        [ProducesResponseType(typeof(IList<TypesResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllTypes()
        {
            var query = new GetAllTypesQuery();
            var result = await _mediator.Send(query);
            if (result == null)
            {

                return NotFound(new ApiResponse { Success = false, Message = $"Types not found" });
            }
            return Ok(new ApiResponse<IList<TypesResponse>> { Success = true, Message = "Types Found", Data = result });

        }

        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand product)
        {
            var result = await _mediator.Send(product);
            if (result == null)
            {
                return BadRequest(new ApiResponse { Success = false, Message = "Failed to add product" });
            }
            return Ok(new ApiResponse<ProductResponse> { Success = true, Message = "Types Found", Data = result });

        }
        [HttpPut]
        [Route("UpadateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpadateProduct([FromBody] UpdateProductCommand product)
        {
            var result = await _mediator.Send(product);
            if (!result)
            {
                return BadRequest(new ApiResponse { Success = false, Message = "Failed to update product" });
            }
            return Ok(new ApiResponse { Success = true, Message = "Product updated successfully" });

        }



        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var query = new DeleteProductCommand(id);
            var result = await _mediator.Send(query);
            if (!result)
            {
                return BadRequest(new ApiResponse { Success = false, Message = "Product not found to delete" });
            }
            return Ok(new ApiResponse { Success = true, Message = "Product deleted successfully" });

        }



    }
}
