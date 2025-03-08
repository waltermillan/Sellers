using Core.Entities;
using Core.Interfases;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

[ApiController]
[Route("api/buyers")]
public class BuyerController : ControllerBase
{
    private readonly IBuyerRepository _buyerRepository;
    private readonly ILoggingService _loggingService;

    public BuyerController(IBuyerRepository buyerRepository, ILoggingService loggingService)
    {
        _buyerRepository = buyerRepository;
        _loggingService = loggingService;
    }

    [HttpGet]
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
            message = "There was an issue retrieving the buyers. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Buyer>> GetById(int id)
    {
        var message = string.Empty;
        try
        {
            var buyer = await _buyerRepository.GetByIdAsync(id);

            if (buyer is null)
                return NotFound();

            return Ok(buyer);
        }
        catch (Exception ex)
        {
            message = "There was an issue retrieving the buyer. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create(Buyer buyer)
    {
        var message = string.Empty;
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _buyerRepository.AddAsync(buyer);
            _loggingService.LogInformation($"Buyer created: {JsonConvert.SerializeObject(buyer)}");
            return CreatedAtAction(nameof(GetById), new { id = buyer.Id }, buyer);
        }
        catch (Exception ex)
        {
            message = "There was an issue creating the buyer. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update(int id, [FromBody] Buyer buyer)
    {
        var message = string.Empty;
        try
        {
            if (buyer is null || id != buyer.Id)
                return BadRequest("Buyer data is invalid.");

            await _buyerRepository.UpdateAsync(buyer);
            _loggingService.LogInformation($"Buyer updated: {JsonConvert.SerializeObject(buyer)}");
            return NoContent();
        }
        catch (Exception ex)
        {
            message = "There was an issue updating the buyer. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        var message = string.Empty;
        try
        {
            await _buyerRepository.DeleteAsync(id);
            _loggingService.LogInformation($"Buyer deleted: {id}");
            return NoContent();
        }
        catch (Exception ex)
        {
            message = "There was an issue deleting the buyer. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }
}
