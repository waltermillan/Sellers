using Core.Entities;
using Core.Interfases;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SellerController : ControllerBase
{
    private readonly ISellerRepository _sellerRepository;

    public SellerController(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<Seller>>> GetAll()
    {
        var sellers = await _sellerRepository.GetAllAsync();
        return Ok(sellers);
    }

    [HttpGet("Get")]
    public async Task<ActionResult<Seller>> GetById(int id)
    {
        var seller = await _sellerRepository.GetByIdAsync(id);
        if (seller == null)
            return NotFound();

        return Ok(seller);
    }


    [HttpPost("Add")]
    public async Task<ActionResult> Create(Seller seller)
    {
        await _sellerRepository.AddAsync(seller);
        return CreatedAtAction(nameof(GetById), new { id = seller.Id }, seller);
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update([FromBody] Seller seller)
    {
        if (false)
            return BadRequest();

        await _sellerRepository.UpdateAsync(seller);
        return NoContent();
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete(int id)
    {
        await _sellerRepository.DeleteAsync(id);
        return NoContent();
    }
}
