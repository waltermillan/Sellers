using Core.Entities;
using Core.Interfases;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

[ApiController]
[Route("api/products")] // Cambié la ruta para que sea plural, api/products
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ILoggingService _loggingService;

    public ProductController(IProductRepository productRepository, ILoggingService loggingService1)
    {
        _productRepository = productRepository;
        _loggingService = loggingService1;
    }

    [HttpGet] // GET api/products
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var message = string.Empty;
        try
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            message = "There was an issue retrieving the States. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpGet("{id}")] // GET api/products/{id}
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var message = string.Empty;
        try
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }
        catch (Exception ex)
        {
            message = "There was an issue retrieving the States. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpPost] // POST api/products
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create(Product product)
    {
        var message = string.Empty;
        try
        {
            await _productRepository.AddAsync(product);
            _loggingService.LogInformation($"Producto alta efectuada: {JsonConvert.SerializeObject(product)}");
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        catch (Exception ex)
        {
            message = "There was an issue retrieving the States. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpPut("{id}")] // PUT api/products/{id}
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update(int id, [FromBody] Product product)
    {
        var message = string.Empty;
        try
        {
            if (product == null || id != product.Id)
                return BadRequest();

            await _productRepository.UpdateAsync(product);
            _loggingService.LogInformation($"Producto actualización efectuada: {JsonConvert.SerializeObject(product)}");
            return NoContent();
        }
        catch (Exception ex)
        {
            message = "There was an issue retrieving the States. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpDelete("{id}")] // DELETE api/products/{id}
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        var message = string.Empty;
        try
        {
            await _productRepository.DeleteAsync(id);
            _loggingService.LogInformation($"Producto baja efectuada: {id}");
            return NoContent();
        }
        catch (Exception ex)
        {
            message = "There was an issue retrieving the States. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }
}
