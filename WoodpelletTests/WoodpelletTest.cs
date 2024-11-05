using WoodpelletAPI.Models;

namespace WoodpelletTests;

[TestClass]
public class WoodpelletTests
{
    // Test a valid Woodpellet object
    [TestMethod]
    public void Validate_ValidWoodpellet_ReturnsTrue()
    {
        // Arrange
        var pellet = new Woodpellet
        {
            Id = 1,
            Brand = "TopQuality",
            Price = 250.0,
            Quality = QualityEnum.Premium
        };

        // Act & Assert
        Assert.IsTrue(pellet.Validate());
    }

    // Test Id boundary values
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Validate_InvalidId_ThrowsException()
    {
        // Arrange
        var pellet = new Woodpellet
        {
            Id = 0, // Boundary value
            Brand = "EcoPellet",
            Price = 200.0,
            Quality = QualityEnum.Medium
        };

        // Act
        pellet.Validate();
    }

    [TestMethod]
    public void Validate_MinimumValidId_ReturnsTrue()
    {
        // Arrange
        var pellet = new Woodpellet
        {
            Id = 1, // Boundary value
            Brand = "EcoPellet",
            Price = 200.0,
            Quality = QualityEnum.Medium
        };

        // Act & Assert
        Assert.IsTrue(pellet.Validate());
    }

    // Test Price boundary values
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Validate_InvalidPrice_ThrowsException()
    {
        // Arrange
        var pellet = new Woodpellet
        {
            Id = 1,
            Brand = "EcoPellet",
            Price = 0, // Boundary value
            Quality = QualityEnum.Medium
        };

        // Act
        pellet.Validate();
    }

    [TestMethod]
    public void Validate_MinimumValidPrice_ReturnsTrue()
    {
        // Arrange
        var pellet = new Woodpellet
        {
            Id = 1,
            Brand = "EcoPellet",
            Price = 0.01, // Minimum positive value
            Quality = QualityEnum.Medium
        };

        // Act & Assert
        Assert.IsTrue(pellet.Validate());
    }

    // Test Brand boundary values
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Validate_EmptyBrand_ThrowsException()
    {
        // Arrange
        var pellet = new Woodpellet
        {
            Id = 1,
            Brand = "", // Empty string
            Price = 200.0,
            Quality = QualityEnum.Medium
        };

        // Act
        pellet.Validate();
    }

    [TestMethod]
    public void Validate_WhitespaceBrand_ThrowsException()
    {
        // Arrange
        var pellet = new Woodpellet
        {
            Id = 1,
            Brand = "   ", // Whitespace only
            Price = 200.0,
            Quality = QualityEnum.Medium
        };

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => pellet.Validate());
    }

    [TestMethod]
    public void Validate_NonEmptyBrand_ReturnsTrue()
    {
        // Arrange
        var pellet = new Woodpellet
        {
            Id = 1,
            Brand = "BasicPellet",
            Price = 200.0,
            Quality = QualityEnum.Medium
        };

        // Act & Assert
        Assert.IsTrue(pellet.Validate());
    }

    // Test Quality boundary values
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Validate_InvalidQuality_ThrowsException()
    {
        // Arrange
        var pellet = new Woodpellet
        {
            Id = 1,
            Brand = "EcoPellet",
            Price = 200.0,
            Quality = (QualityEnum)999 // Invalid enum value
        };

        // Act
        pellet.Validate();
    }

    [TestMethod]
    public void Validate_ValidQuality_ReturnsTrue()
    {
        // Arrange
        var pellet = new Woodpellet
        {
            Id = 1,
            Brand = "EcoPellet",
            Price = 200.0,
            Quality = QualityEnum.High
        };

        // Act & Assert
        Assert.IsTrue(pellet.Validate());
    }

    // Test ToString method
    [TestMethod]
    public void ToString_ReturnsExpectedString()
    {
        // Arrange
        var pellet = new Woodpellet
        {
            Id = 1,
            Brand = "EcoPellet",
            Price = 200.0,
            Quality = QualityEnum.Premium
        };
        var expectedString = "Id: 1, Brand: EcoPellet, Price: 200, Quality: Premium";

        // Act
        var result = pellet.ToString();

        // Assert
        Assert.AreEqual(expectedString, result);
    }

    // Test Equals method
    [TestMethod]
    public void Equals_SameProperties_ReturnsTrue()
    {
        // Arrange
        var pellet1 = new Woodpellet
        {
            Id = 1,
            Brand = "EcoPellet",
            Price = 200.0,
            Quality = QualityEnum.Medium
        };
        var pellet2 = new Woodpellet
        {
            Id = 1,
            Brand = "EcoPellet",
            Price = 200.0,
            Quality = QualityEnum.Medium
        };

        // Act & Assert
        Assert.IsTrue(pellet1.Equals(pellet2));
    }

    [TestMethod]
    public void Equals_DifferentProperties_ReturnsFalse()
    {
        // Arrange
        var pellet1 = new Woodpellet
        {
            Id = 1,
            Brand = "EcoPellet",
            Price = 200.0,
            Quality = QualityEnum.Medium
        };
        var pellet2 = new Woodpellet
        {
            Id = 2,
            Brand = "TopPellet",
            Price = 250.0,
            Quality = QualityEnum.High
        };

        // Act & Assert
        Assert.IsFalse(pellet1.Equals(pellet2));
    }

    // Test GetHashCode method
    [TestMethod]
    public void GetHashCode_SameProperties_ReturnsSameHashCode()
    {
        // Arrange
        var pellet1 = new Woodpellet
        {
            Id = 1,
            Brand = "EcoPellet",
            Price = 200.0,
            Quality = QualityEnum.Medium
        };
        var pellet2 = new Woodpellet
        {
            Id = 1,
            Brand = "EcoPellet",
            Price = 200.0,
            Quality = QualityEnum.Medium
        };

        // Act & Assert
        Assert.AreEqual(pellet1.GetHashCode(), pellet2.GetHashCode());
    }
}