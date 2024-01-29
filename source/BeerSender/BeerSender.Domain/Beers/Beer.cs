namespace BeerSender.Domain.Beers;

public record Volume(int Amount)
{
    public static Volume Create(int desiredVolume)
    { 
        return desiredVolume switch
        {
            >= 12 => new Volume(12), 
            _ => new Volume(12)
        };
    }
}

public record AlcoholContent(double Level)
{
    public static AlcoholContent Create(double level)
    {
        if (level > 12)
        {
            throw new AlcoholPoisoningException(level);
        }

        return new AlcoholContent(level);
    }
};

public record BrandName(string Name);

public record ProductName(string Name);

public class Beer(BrandName Brand, ProductName Product, Volume Volume, AlcoholContent Alcohol);

public class AlcoholPoisoningException : Exception
{
    public AlcoholPoisoningException(double level)
    :base($"This beer is way to strong at {level} and you will die if you drink it")
    {
        
    }
}


