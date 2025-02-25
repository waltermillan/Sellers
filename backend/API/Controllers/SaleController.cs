using Core.Entities;
using Core.Interfases;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

[ApiController]
[Route("api/sales")] // Cambié la ruta para que sea plural, api/sales
public class SaleController : ControllerBase
{
    private readonly ISaleRepository _saleRepository;
    private readonly ILoggingService _loggingService;

    public SaleController(ISaleRepository saleRepository, ILoggingService loggingService)
    {
        _saleRepository = saleRepository;
        _loggingService = loggingService;
    }

    [HttpGet] // GET api/sales
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Sale>>> GetAll()
    {
        var message = string.Empty;
        try
        {
            var sales = await _saleRepository.GetAllAsync();
            return Ok(sales);
        }
        catch (Exception ex)
        {
            message = "There was an issue retrieving the States. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpGet("{id}")] // GET api/sales/{id}
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Sale>> GetById(int id)
    {
        var message = string.Empty;
        try
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
                return NotFound();

            return Ok(sale);
        }
        catch (Exception ex)
        {
            message = "There was an issue retrieving the States. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpPost] // POST api/sales
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create(Sale sale)
    {
        var message = string.Empty;
        try
        {
            if (sale == null)
            {
                return BadRequest("Sale object is null");
            }
            await _saleRepository.AddAsync(sale);

            _loggingService.LogInformation($"Sale alta efectuada: {JsonConvert.SerializeObject(sale)}");
            return CreatedAtAction(nameof(GetById), new { id = sale.Id }, sale);
        }
        catch (Exception ex)
        {
            message = "There was an issue retrieving the States. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpPut("{id}")] // PUT api/sales/{id}
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update(int id, [FromBody] Sale sale)
    {
        var message = string.Empty;
        try
        {
            if (sale == null || id != sale.Id)
                return BadRequest();

            await _saleRepository.UpdateAsync(sale);

            _loggingService.LogInformation($"Sale actualización efectuada: {JsonConvert.SerializeObject(sale)}");
            return NoContent();
        }
        catch (Exception ex)
        {
            message = "There was an issue retrieving the States. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpDelete("{id}")] // DELETE api/sales/{id}
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        var message = string.Empty;
        try
        {
            await _saleRepository.DeleteAsync(id);

            _loggingService.LogInformation($"Sale baja efectuada: {id}");
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
