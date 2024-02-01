﻿using Microsoft.AspNetCore.Mvc;
using PriceCheck.BusinessLogic.Dtos.ATB;
using PriceCheck.BusinessLogic.Interfaces;
using PriceCheck.BusinessLogic.Services;

namespace PriceCheck.API.Controllers
{
    [Route ("api/ATB")]
    public class ATBController : BaseController
    {
        private readonly IATBService _ATBService;
        private readonly ICrawlerService _crawlerService;
        private readonly IParserService _parserService;
        public ATBController(IATBService ATBService, ICrawlerService crawlerService, IParserService parserService)
        {
            _ATBService = ATBService;
            _crawlerService = crawlerService;
            _parserService = parserService;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ATBDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _ATBService.GetShopProduct(id);
            if (product == null)
            {
                return NotFound("No product with such ID");
            }
            return Ok(product);
        }

        [HttpGet("/SearchByName{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ATBDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductsByName(string name)
        {
            var product = await _ATBService.GetShopProductsByFuzzyName(name);
            if (product == null)
            {
                return NotFound("No product with such name");
            }
            return Ok(product);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ATBDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ATBDto>>> GetAllProducts()
        {
            var products = await _ATBService.GetAllShopProducts();
            if (!products.Any())
            {
                return NotFound("Theres no products");
            }
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ATBDto>> CreateProduct(ATBDto productDto)
        {
            var _productDto = await _ATBService.CreateShopProduct(productDto);
            if (_productDto == null)
            {
                return BadRequest("Cant create product");
            }
            return CreatedAtAction(nameof(GetProduct), new { id = _productDto.Id }, _productDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct(int id, ATBUpdateDto ATBUpdateDto)
        {
            try
            {
                await _ATBService.UpdateShopProduct(id, ATBUpdateDto);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _ATBService.DeleteShopProduct(id);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("/Crawl")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Crawl()
        {
            await _crawlerService.Run();

        }

        [HttpGet("/Parse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Parse()
        {
            await _parserService.Run();

        }
    }
}
