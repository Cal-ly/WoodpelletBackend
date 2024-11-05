using WoodpelletAPI.Models;

namespace WoodpelletAPI.Repositories;

/// <summary>
/// Repository for managing woodpellet data.
/// </summary>
public class WoodpelletRepository
{
    private readonly List<Woodpellet> _woodpellets = [];

    /// <summary>
    /// Gets the next available Id for a new woodpellet.
    /// </summary>
    public int NextId => FindNextId();

    /// <summary>
    /// Initializes a new instance of the <see cref="WoodpelletRepository"/> class.
    /// </summary>
    public WoodpelletRepository()
    {
        _woodpellets.Add(new Woodpellet { Id = 1, Brand = "EcoPellet", Price = 200.0, Quality = QualityEnum.Medium });
        _woodpellets.Add(new Woodpellet { Id = 2, Brand = "TopPellet", Price = 300.0, Quality = QualityEnum.High });
        _woodpellets.Add(new Woodpellet { Id = 3, Brand = "GreenFire", Price = 250.0, Quality = QualityEnum.Low });
        _woodpellets.Add(new Woodpellet { Id = 4, Brand = "PureHeat", Price = 180.0, Quality = QualityEnum.Medium });
        _woodpellets.Add(new Woodpellet { Id = 5, Brand = "WarmGlow", Price = 220.0, Quality = QualityEnum.High });
        _woodpellets.Add(new Woodpellet { Id = 5, Brand = "KingHeat", Price = 220.0, Quality = QualityEnum.Premium });
        _woodpellets.Add(new Woodpellet { Id = 6, Brand = "ChernobylFire", Price = 190.0, Quality = QualityEnum.BasicallyNapalm });
    }

    /// <summary>
    /// Retrieves all woodpellets.
    /// </summary>
    /// <returns>A list of all woodpellets.</returns>
    public List<Woodpellet> GetAll()
    {
        return new List<Woodpellet>(_woodpellets);
    }

    /// <summary>
    /// Retrieves a specific woodpellet by Id.
    /// </summary>
    /// <param name="id">The Id of the woodpellet to retrieve.</param>
    /// <returns>The woodpellet with the specified Id, or null if not found.</returns>
    public Woodpellet? GetById(int id)
    {
        return _woodpellets.FirstOrDefault(w => w.Id == id);
    }

    /// <summary>
    /// Adds a new woodpellet.
    /// </summary>
    /// <param name="woodpellet">The woodpellet to add.</param>
    /// <exception cref="ArgumentException">Thrown when a woodpellet with the same Id already exists.</exception>
    public void Add(Woodpellet woodpellet)
    {
        if (_woodpellets.Any(w => w.Id == woodpellet.Id))
        {
            throw new ArgumentException($"A Woodpellet with Id {woodpellet.Id} already exists.");
        }

        woodpellet.Id = NextId;
        woodpellet.Validate();
        _woodpellets.Add(woodpellet);
    }

    /// <summary>
    /// Updates an existing woodpellet.
    /// </summary>
    /// <param name="updatedWoodpellet">The woodpellet with updated information.</param>
    /// <exception cref="KeyNotFoundException">Thrown when the woodpellet to update is not found.</exception>
    public void Update(Woodpellet updatedWoodpellet)
    {
        var existingWoodpellet = GetById(updatedWoodpellet.Id);

        if (existingWoodpellet == null)
        {
            throw new KeyNotFoundException($"Woodpellet with Id {updatedWoodpellet.Id} not found.");
        }

        updatedWoodpellet.Validate(); // Validate before updating

        existingWoodpellet.Brand = updatedWoodpellet.Brand;
        existingWoodpellet.Price = updatedWoodpellet.Price;
        existingWoodpellet.Quality = updatedWoodpellet.Quality;
    }

    /// <summary>
    /// Removes a woodpellet by Id.
    /// </summary>
    /// <param name="id">The Id of the woodpellet to remove.</param>
    /// <exception cref="KeyNotFoundException">Thrown when the woodpellet to remove is not found.</exception>
    public void Remove(int id)
    {
        var woodpellet = GetById(id);

        if (woodpellet == null)
        {
            throw new KeyNotFoundException($"Woodpellet with Id {id} not found.");
        }

        _woodpellets.Remove(woodpellet);
    }

    /// <summary>
    /// Calculates the next available Id for a new woodpellet.
    /// </summary>
    /// <returns>The next available Id integer.</returns>
    private int FindNextId()
    {
        return _woodpellets.Count == 0 ? 1 : _woodpellets.Max(wp => wp.Id) + 1;
    }
}
