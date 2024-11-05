namespace WoodpelletAPI.Models;

public class Woodpellet
{
    public int Id { get; set; }
    public string? Brand { get; set; }
    public double Price { get; set; }
    public QualityEnum Quality { get; set; } = QualityEnum.Undefined;

    public bool Validate()
    {
        if (Id <= 0)
        {
            throw new ArgumentException("Id must be greater than 0");
        }
        if (string.IsNullOrWhiteSpace(Brand))
        {
            throw new ArgumentException("Brand cannot be empty");
        }
        if (Price <= 0)
        {
            throw new ArgumentException("Price must be greater than 0");
        }
        if (!Enum.IsDefined(typeof(QualityEnum), Quality))
        {
            throw new ArgumentException("Quality must be a valid value");
        }
        else return true;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Brand: {Brand}, Price: {Price}, Quality: {Quality}";
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Woodpellet other = (Woodpellet)obj;
        return Id == other.Id && 
               Brand == other.Brand && 
               Price == other.Price && 
               Quality == other.Quality;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Brand, Price, Quality);
    }
}
