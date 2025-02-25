using Core.Entities;
using Core.Interfases;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[ApiController]
[Route("api/sellers")] // Se cambió para seguir la convención RESTful, ahora la ruta es /api/sellers
public class SellerController : ControllerBase
{
    private readonly ISellerRepository _sellerRepository;
    private readonly ILoggingService _loggingService;

    public SellerController(ISellerRepository sellerRepository, ILoggingService loggingService)
    {
        _sellerRepository = sellerRepository;
        _loggingService = loggingService;
    }

    // Obtener todos los vendedores
    [HttpGet] // GET api/sellers
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Seller>>> GetAll()
    {
        var message = string.Empty;
        try
        {
            var sellers = await _sellerRepository.GetAllAsync();
            return Ok(sellers);
        }
        catch (Exception ex)
        {
            message = "There was an issue retrieving the sellers. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    // Obtener un vendedor por ID
    [HttpGet("{id}")] // GET api/sellers/{id}
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Seller>> GetById(int id)
    {
        var message = string.Empty;
        try
        {
            var seller = await _sellerRepository.GetByIdAsync(id);
            if (seller is null)
                return NotFound();

            return Ok(seller);
        }
        catch (Exception ex)
        {
            message = "There was an issue retrieving the seller. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    // Crear un nuevo vendedor
    [HttpPost] // POST api/sellers
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create(Seller seller)
    {
        var message = string.Empty;
        try
        {
            await _sellerRepository.AddAsync(seller);
            _loggingService.LogInformation($"Seller created: {JsonConvert.SerializeObject(seller)}");
            return CreatedAtAction(nameof(GetById), new { id = seller.Id }, seller);
        }
        catch (Exception ex)
        {
            message = "There was an issue creating the seller. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    // Actualizar un vendedor existente
    [HttpPut("{id}")] // PUT api/sellers/{id}
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update(int id, [FromBody] Seller seller)
    {
        var message = string.Empty;
        try
        {
            if (seller is null || seller.Id != id)
                return BadRequest();

            await _sellerRepository.UpdateAsync(seller);
            _loggingService.LogInformation($"Seller updated: {JsonConvert.SerializeObject(seller)}");
            return NoContent();
        }
        catch (Exception ex)
        {
            message = "There was an issue updating the seller. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    // Eliminar un vendedor
    [HttpDelete("{id}")] // DELETE api/sellers/{id}
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        var message = string.Empty;
        try
        {
            await _sellerRepository.DeleteAsync(id);
            _loggingService.LogInformation($"Seller deleted: {id}");
            return NoContent();
        }
        catch (Exception ex)
        {
            message = "There was an issue deleting the seller. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }
}
