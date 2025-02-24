using Core.Entities;
using Core.Interfases;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

[ApiController]
[Route("api/[controller]")]
public class BuyerController : ControllerBase
{
    private readonly IBuyerRepository _buyerRepository;
    private readonly ILoggingService _loggingService;

    public BuyerController(IBuyerRepository buyerRepository, ILoggingService loggingService)
    {
        _buyerRepository = buyerRepository;
        _loggingService = loggingService;
    }

    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Buyer>>> GetAll()
    {
        var message = string.Empty;
        try
        {
            var buyers = await _buyerRepository.GetAllAsync();
            return Ok(buyers);
        }
        catch (Exception ex)
        {
            // Loggeamos el error para fines de depuración y luego devolvemos una respuesta con un mensaje amigable
            message = "There was an issue retrieving the States. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpGet("Get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Buyer>> GetById(int id)
    {
        var message = string.Empty;
        try
        {
            var buyer = await _buyerRepository.GetByIdAsync(id);
            if (buyer == null)
                return NotFound();
          
            return Ok(buyer);
        }
        catch (Exception ex)
        {
            // Loggeamos el error para fines de depuración y luego devolvemos una respuesta con un mensaje amigable
            message = "There was an issue retrieving the States. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }


    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create(Buyer buyer)
    {
        var message = string.Empty;
        try
        {
            await _buyerRepository.AddAsync(buyer);
            _loggingService.LogInformation($"Comprador alta efectuada: {JsonConvert.SerializeObject(buyer)}");
            return CreatedAtAction(nameof(GetById), new { id = buyer.Id }, buyer);
        }
        catch (Exception ex)
        {
            // Loggeamos el error para fines de depuración y luego devolvemos una respuesta con un mensaje amigable
            message = "There was an issue retrieving the States. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }

    }

    [HttpPut("Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update([FromBody] Buyer buyer)
    {
        var message = string.Empty;
        try
        {
            if (buyer is null)
                return BadRequest();

            await _buyerRepository.UpdateAsync(buyer);
            _loggingService.LogInformation($"Comprador actualización efectuada: {JsonConvert.SerializeObject(buyer)}");
            return NoContent();
        }
        catch (Exception ex)
        {
            // Loggeamos el error para fines de depuración y luego devolvemos una respuesta con un mensaje amigable
            message = "There was an issue retrieving the States. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpDelete("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        var message = string.Empty;
        try
        {
            await _buyerRepository.DeleteAsync(id);
            _loggingService.LogInformation($"Comprador baja efectuada: {id}");
            return NoContent();
        }
        catch (Exception ex)
        {
            // Loggeamos el error para fines de depuración y luego devolvemos una respuesta con un mensaje amigable
            message = "There was an issue retrieving the States. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }
}
