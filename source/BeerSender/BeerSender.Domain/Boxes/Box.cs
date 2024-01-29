using BeerSender.Domain.Beers;

namespace BeerSender.Domain.Boxes;

/// <summary>
/// Defines the number of items that can fit into a box
/// Capacity could be invalid in the future, as we could change the available sizes of boxes. example we could remove a 6 pack for an 8 pack
/// </summary>
/// <param name="NumberOfSpots">Number of items in the box</param>
public record Capacity(int NumberOfSpots)
{
    /// <summary>
    /// Creates a box that can only hold 6,12,24 items.
    /// </summary>
    /// <param name="desiredSize"></param>
    /// <returns></returns>
    public static Capacity Create(int desiredSize)
    {
        return desiredSize switch
        {
            <= 6 => new Capacity(6),
            <= 12 => new Capacity(12),
            <= 24 => new Capacity(24),
            _ => new Capacity(24)
        };
    }
};

/// <summary>
/// A command to get a new box
/// </summary>
/// <param name="DesiredNumberOfSpots"></param>
public record GetBox(int DesiredNumberOfSpots);

/// <summary>
/// An event to represent a new box was successfully created.
/// </summary>
/// <param name="NumberOfSpots"></param>
public record BoxCreated(int NumberOfSpots);

/// <summary>
/// Adds a beer to a box
/// </summary>
public record AddBeer(Beer Beer, Box Box);

/// <summary>
/// A beer has been added to a box
/// </summary>
public record BeerAdded(Beer Beer);

/// <summary>
/// The box is full, no more beers can be added.
/// </summary>
/// <param name="NumberOfBeers">The number of beers in the box.</param>
public record BoxFull(int NumberOfBeers);

public record CloseBox();

public record BoxClosed(Box Box);

public record FailedToCloseBox(Box Box);

/// <summary>
/// A box holds beer
/// </summary>
public class Box
{
    public Capacity Capactity { get; private set; }
}

