using Core.Entities;
using Core.Interfases;
using Corer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ILoggingService _loggingService;

    public UserController(IUserRepository userRepository, ILoggingService loggingService)
    {
        _userRepository = userRepository;
        _loggingService = loggingService;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Auth(string usr, string psw)
    {
        try
        {
            var user = await _userRepository.GetByUsrAsync(usr);

            if (user is null)
            {
                _loggingService.LogInformation("Intento de inicio de sesión fallido para el usuario: " + usr);
                return Unauthorized(new { Code = 1, Message = "Invalid username or password" });
            }

            // Verify if the provided password matches the stored hash
            var isAuthenticated = PasswordHasher.VerifyPassword(psw, user.PasswordHash);

            if (!isAuthenticated)
            {
                _loggingService.LogInformation("Intento de inicio de sesión fallido para el usuario: " + usr);
                return Unauthorized(new { Code = 1, Message = "Invalid username or password" });
            }

            _loggingService.LogInformation($"Usuario '{usr}' autenticado correctamente.");

            return Ok(new { Code = 0, Message = "User authenticated successfully" });

        }
        catch (Exception ex)
        {
            var message = "There was an issue authenticating the user. Please try again later.";
            _loggingService.LogError(message, ex);

            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpGet]
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
            message = "There was an issue retrieving the users. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpGet("{id}")]
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
            message = "There was an issue retrieving the user. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create(User user)
    {
        var message = string.Empty;
        try
        {
            await _userRepository.AddAsync(user);

            _loggingService.LogInformation($"Usuario creado: {JsonConvert.SerializeObject(user)}");
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            message = "There was an issue creating the user. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update(int id, [FromBody] User user)
    {
        var message = string.Empty;
        try
        {
            if (user is null || user.Id != id)
                return BadRequest();

            await _userRepository.UpdateAsync(user);

            _loggingService.LogInformation($"Usuario actualizado: {JsonConvert.SerializeObject(user)}");
            return NoContent();
        }
        catch (Exception ex)
        {
            message = "There was an issue updating the user. Please try again later.";
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
            await _userRepository.DeleteAsync(id);

            _loggingService.LogInformation($"Usuario eliminado: {id}");
            return NoContent();
        }
        catch (Exception ex)
        {
            message = "There was an issue deleting the user. Please try again later.";
            _loggingService.LogError(message, ex);
            return StatusCode(500, new { Message = message, Details = ex.Message });
        }
    }
}
