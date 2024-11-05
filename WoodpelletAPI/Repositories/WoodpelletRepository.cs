using WoodpelletAPI.Models;

namespace WoodpelletAPI.Repositories;

public class WoodpelletRepository
{
    private readonly List<Woodpellet> _woodpellets = [];
    public int NextId => FindNextId();
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

    // Retrieve all Woodpellets
    public List<Woodpellet> GetAll()
    {
        return new List<Woodpellet>(_woodpellets);
    }

    // Retrieve a specific Woodpellet by Id
    public Woodpellet? GetById(int id)
    {
        return _woodpellets.FirstOrDefault(w => w.Id == id);
    }

    // Add a new Woodpellet
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

    // Update an existing Woodpellet
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

    // Remove a Woodpellet by Id
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
    /// Calculates the next available Id for a new wood pellet.
    /// </summary>
    /// <returns>The next available Id integer</returns>
    private int FindNextId()
    {
        return _woodpellets.Count == 0 ? 1 : _woodpellets.Max(wp => wp.Id) + 1;
    }
}
