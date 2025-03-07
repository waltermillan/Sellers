using Core.Entities;
using Core.Interfases;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/sales")]
public class SaleDTOController : ControllerBase
{
    private readonly SaleDTOService _saleDTOService;
    private readonly ILoggingService _loggingService;

    public SaleDTOController(SaleDTOService saleDTOService, ILoggingService loggingService)
    {
        _saleDTOService = saleDTOService;
        _loggingService = loggingService;
    }

    [HttpGet("{id}/dto")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSaleDTO(int id)
    {
        var message = string.Empty;
        try
        {
            var saleDTO = await _saleDTOService.GetSaleAsync(id);

            if (saleDTO is null)
                return NotFound();

            return Ok(saleDTO);
        }
        catch (Exception ex)
        {
            message = "There was an issue retrieving the sale. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpGet("dto")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllSales()
    {
        var message = string.Empty;
        try
        {
            var sales = await _saleDTOService.GetAllSalesAsync();

            if (sales is null || !sales.Any())
            {
                return NoContent();
            }

            return Ok(sales);
        }
        catch (Exception ex)
        {
            message = "There was an issue retrieving the sales. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }
}
