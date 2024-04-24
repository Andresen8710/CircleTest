using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaCycle.Application.Interfaces;
using PruebaTecnicaCycle.Domain.Dtos.Request;
using PruebaTecnicaCycle.Identity.Constants;
using PruebaTecnicaCycle.Identity.Helpers;

namespace PruebaTecnicaCycle.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IAppProductService _productService;

        public ProductController(IAppProductService productService)
        {
            _productService = productService;
        }

        [HttpGet, Route("getalldapper")]
        public async Task<IActionResult> GetAllDapperAsync()
        {
            try
            {
                var productList = await _productService.GetAllDapperAsync();

                if (productList == null) return NotFound();

                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("getall")]
        public async Task<IActionResult> GetAllEntityFrameworkAsync()
        {
            try
            {
                var productList = await _productService.GetAllEntityFrameworkAsync();

                if (productList == null) return NotFound();

                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            try
            {
                if (Id == null || Id == 0) return BadRequest();

                var product = await _productService.GetByIdAsync(Id);

                if (product == null) return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles =Roles.Administrator)]
        public async Task<IActionResult> AddAsync(ProductRequestModelDto productRequestModelDto)
        {
            try
            {
                if (productRequestModelDto == null) return BadRequest();

                var newProduct = await _productService.AddAsync(productRequestModelDto);

                if (newProduct == null) return BadRequest();

                return Ok(newProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ProductRequestModelDto product)
        {
            try
            {
                if (id == null || id == 0 || product == null) return BadRequest();

                var productEdited = await _productService.UpdateAsync(id, product);

                return Ok(productEdited);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                if (id == null || id == 0) return BadRequest();

                var result = await _productService.DeleteAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}