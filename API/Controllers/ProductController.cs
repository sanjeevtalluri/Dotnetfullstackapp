using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using CoreData.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreData.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepoistory productRepoistory;
        public ProductsController(IProductRepoistory productRepoistory)
        {
            this.productRepoistory = productRepoistory;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await productRepoistory.GetProductsAsync();
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await productRepoistory.GetProductByIdAsync(id);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await productRepoistory.GetProductBrandsAsync();
            return Ok(productBrands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var productTypes = await productRepoistory.GetProductTypesAsync();
            return Ok(productTypes);
        }
    }
}