using Microsoft.AspNetCore.Mvc;
using WoodpelletAPI.Models;
using WoodpelletAPI.Repositories;

namespace WoodpelletAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class WoodpelletController : ControllerBase
{
    private readonly WoodpelletRepository _repository = new WoodpelletRepository();

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var woodpellet = _repository.GetById(id);
        if (woodpellet == null) return NotFound();
        return Ok(woodpellet);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Add([FromBody] Woodpellet woodpellet)
    {
        try
        {
            _repository.Add(woodpellet);
            return CreatedAtAction(nameof(GetById), new { id = woodpellet.Id }, woodpellet);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] Woodpellet woodpellet)
    {
        if (id != woodpellet.Id) return BadRequest("Id mismatch.");

        try
        {
            _repository.Update(woodpellet);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Remove(int id)
    {
        try
        {
            _repository.Remove(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
