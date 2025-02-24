using Core.Entities;
using Core.Interfases;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ILoggingService _loggingService;

    public UserController(IUserRepository userRepository, ILoggingService loggingService)
    {
        _userRepository = userRepository;
        _loggingService = loggingService;
    }

    [HttpGet("Auth")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Auth(string usr, string psw)
    {
        try
        {
            // Verificamos si el usuario existe y si la contraseña es correcta
            var isAuthenticated = await _userRepository.GetByUsrPwdAsync(usr, psw);

            if (!isAuthenticated)
            {
                // Si la autenticación falla, devolvemos 401 (Unauthorized)
                _loggingService.LogInformation("Intento de inicio de sesión fallido para el usuario: " + usr);
                return Unauthorized(new { Code = 1, Message = "Invalid username or password" });
            }

            // Si la autenticación es exitosa, se registra la acción y se retorna un 200 OK
            _loggingService.LogInformation($"Usuario '{usr}' autenticado correctamente.");

            // Aquí podrías generar un token JWT si es necesario para la autenticación basada en token
            // Ejemplo: var token = GenerateJwtToken(usr);

            // Retorna un OK con un mensaje (o token si lo generas)
            return Ok(new { Code = 0, Message = "User authenticated successfully" });

        }
        catch (Exception ex)
        {
            // Loggeamos el error para fines de depuración y luego devolvemos una respuesta con un mensaje amigable
            var message = "There was an issue authenticating the user. Please try again later.";
            _loggingService.LogError(message, ex);

            // Devolvemos un código 500 con detalles del error
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }


    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        var message = string.Empty;
        try
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            // Loggeamos el error para fines de depuración y luego devolvemos una respuesta con un mensaje amigable
            message = "There was an issue retrieving the Users. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpGet("Get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<User>> GetById(int id)
    {
        var message = string.Empty;
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                return NotFound();

            return Ok(user);
        }
        catch (Exception ex)
        {
            // Loggeamos el error para fines de depuración y luego devolvemos una respuesta con un mensaje amigable
            message = "There was an issue retrieving the Users. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }


    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create(User user)
    {
        var message = string.Empty;
        try
        {
            await _userRepository.AddAsync(user);

            _loggingService.LogInformation($"Vendedor alta efectuada: {JsonConvert.SerializeObject(user)}");
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            // Loggeamos el error para fines de depuración y luego devolvemos una respuesta con un mensaje amigable
            message = "There was an issue retrieving the Users. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpPut("Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update([FromBody] User user)
    {
        var message = string.Empty;
        try
        {
            if (user is null)
                return BadRequest();

            await _userRepository.UpdateAsync(user);

            _loggingService.LogInformation($"Vendedor actualización efectuada: {JsonConvert.SerializeObject(user)}");
            return NoContent();
        }
        catch (Exception ex)
        {
            // Loggeamos el error para fines de depuración y luego devolvemos una respuesta con un mensaje amigable
            message = "There was an issue retrieving the Users. Please try again later.";
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
            await _userRepository.DeleteAsync(id);

            _loggingService.LogInformation($"Vendedor baja efectuada: {id}");
            return NoContent();
        }
        catch (Exception ex)
        {
            // Loggeamos el error para fines de depuración y luego devolvemos una respuesta con un mensaje amigable
            message = "There was an issue retrieving the Users. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }
}
