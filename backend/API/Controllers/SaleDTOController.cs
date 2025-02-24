using Core.Entities;
using Core.Interfases;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SaleDTOController : ControllerBase
{
    private readonly SaleDTOService _saleDTOService;
    private readonly ILoggingService _loggingService;

    public SaleDTOController(SaleDTOService saleDTOService, ILoggingService loggingService)
    {
        _saleDTOService = saleDTOService;
        _loggingService = loggingService;
    }

    // Obtener un CustomerDTO por id
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSaleDTO(int id)
    {
        var message = string.Empty;
        try
        {
            var customerDTO = await _saleDTOService.GetSaleAsync(id);

            if (customerDTO == null)
            {
                return NotFound(); // o algún otro manejo de error
            }

            return Ok(customerDTO);
        }
        catch (Exception ex)
        {
            // Loggeamos el error para fines de depuración y luego devolvemos una respuesta con un mensaje amigable
            message = "There was an issue retrieving the States. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpGet("GetAll")]
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
                return NoContent(); // Retorna 204 No Content si no hay ventas
            }

            return Ok(sales); // Retorna 200 OK con las ventas
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
