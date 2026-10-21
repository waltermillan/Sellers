using Core.Entities;
using Core.Interfases;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

[ApiController]
[Route("api/sales")]
public class SaleController : ControllerBase
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleProcessedRepository _saleProcessedRepository;
    private readonly ILoggingService _loggingService;

    public SaleController(ISaleRepository saleRepository, ISaleProcessedRepository saleProcessedRepository, ILoggingService loggingService)
    {
        _saleRepository = saleRepository;
        _saleProcessedRepository = saleProcessedRepository;
        _loggingService = loggingService;
    }

    [HttpGet]
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

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Sale>> GetById(int id)
    {
        var message = string.Empty;
        try
        {
            var sale = await _saleRepository.GetByIdAsync(id);

            if (sale is null)
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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create(Sale sale)
    {
        var message = string.Empty;
        try
        {
            if (sale is null)
                return BadRequest("Sale object is null");

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



    [HttpPost("bulk")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateBulk()
    {
        try
        {
            var salesProcessed = await _saleProcessedRepository.GetAllAsync();
            var sales = salesProcessed.Select(saleProcessed => new Sale
            {
                IdBuyer = saleProcessed.IdBuyer,
                IdSeller = saleProcessed.IdSeller,
                IdProduct = saleProcessed.IdProduct,
                Date = saleProcessed.Date,
                Amount = saleProcessed.Amount,
                Quantity = saleProcessed.Quantity
            }).ToList();

            if (sales.Count == 0)
                return Ok(new { Message = "No processed sales to copy.", Count = 0 });

            foreach (var sale in sales)
            {
                await _saleRepository.AddAsync(sale);
            }

            _loggingService.LogInformation($"Bulk insert completed: {sales.Count} sales.");

            return StatusCode(StatusCodes.Status201Created, new
            {
                Message = "Sales created successfully.",
                Count = sales.Count
            });
        }
        catch (Exception ex)
        {
            const string message = "There was an issue creating the sales.";
            _loggingService.LogError(message, ex);

            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Message = message,
                Details = ex.Message
            });
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update(int id, [FromBody] Sale sale)
    {
        var message = string.Empty;
        try
        {
            if (sale is null || id != sale.Id)
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

    [HttpDelete("{id}")]
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

    [HttpDelete("bulk")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteBulk()
    {
        var message = string.Empty;

        try
        {
            var sales = (await _saleRepository.GetAllAsync()).ToList();

            foreach (var sale in sales)
            {
                await _saleRepository.DeleteAsync(sale.Id);
            }

            _loggingService.LogInformation(
                $"Bulk delete completed: {sales.Count} records.");

            return NoContent();
        }
        catch (Exception ex)
        {
            message = "There was an issue deleting the sales records.";
            _loggingService.LogError(message, ex);

            return StatusCode(500, new
            {
                Message = message,
                Details = ex.Message
            });
        }
    }
}
