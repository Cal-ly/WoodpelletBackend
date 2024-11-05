using Microsoft.AspNetCore.Mvc;
using WoodpelletAPI.Models;
using WoodpelletAPI.Repositories;

namespace WoodpelletAPI.Controllers;

/// <summary>
/// Controller for managing woodpellet operations.
/// </summary>
[Route("[controller]")]
[ApiController]
public class WoodpelletController : ControllerBase
{
    private readonly WoodpelletRepository _repository = new WoodpelletRepository();

    /// <summary>
    /// Retrieves all woodpellets.
    /// </summary>
    /// <returns>A list of all woodpellets.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(_repository.GetAll());
    }

    /// <summary>
    /// Retrieves a specific woodpellet by Id.
    /// </summary>
    /// <param name="id">The Id of the woodpellet to retrieve.</param>
    /// <returns>The woodpellet with the specified Id, or 404 if not found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var woodpellet = _repository.GetById(id);
        if (woodpellet == null) return NotFound();
        return Ok(woodpellet);
    }

    /// <summary>
    /// Adds a new woodpellet.
    /// </summary>
    /// <param name="woodpellet">The woodpellet to add.</param>
    /// <returns>The created woodpellet with its Id.</returns>
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

    /// <summary>
    /// Updates an existing woodpellet.
    /// </summary>
    /// <param name="id">The Id of the woodpellet to update.</param>
    /// <param name="woodpellet">The updated woodpellet data.</param>
    /// <returns>No content if successful, or an error message if failed.</returns>
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

    /// <summary>
    /// Removes a woodpellet by Id.
    /// </summary>
    /// <param name="id">The Id of the woodpellet to remove.</param>
    /// <returns>No content if successful, or an error message if failed.</returns>
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
