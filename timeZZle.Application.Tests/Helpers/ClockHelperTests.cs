using Shouldly;
using timeZZle.Application.Helpers;

namespace timeZZle.Application.Tests.Helpers;

public class ClockHelperTests
{
    private readonly ClockHelper _clockHelper;

    public ClockHelperTests()
    {
        _clockHelper = new ClockHelper();
    }

    [Fact]
    public void GenerateRandomSolvable_Always_ResultIsSolvable()
    {
        // Arrange
        const int size = 8;
        const int maxValue = 4;

        // Act
        var result = _clockHelper.GenerateRandomSolvable(size, maxValue);

        // Assert
        result.ShouldNotBeNull();
        _clockHelper.IsSolvable(result).ShouldBeTrue();
    }

}

/** 
    Useful code : 
    
    
    public static void OneTimeMap()
    {
        TypeAdapterConfig<Clock, ClockHelper.PrintableClock>.NewConfig();
        TypeAdapterConfig<Box, ClockHelper.PrintableBox>.NewConfig();
    }
    
    internal record PrintableBox(int Position, int Value);
    internal record PrintableClock(int Size, PrintableBox[] Boxes);
    
    public void dbfds()
    {
        var result = new List<Clock>();
        
        for (var i = 0; i < 5; i++)
        {
            var size = i % 3 + 6;
            result.Add(GenerateRandomSolvable(size, 4));
        }

        var array = result.Adapt<PrintableClock[]>();
        var options = new JsonSerializerOptions
        {
            WriteIndented = true 
        };
        var json = JsonSerializer.Serialize(array, options);
        
        File.WriteAllText(@"C:\Users\Ted\Documents\seedData.json", json);
    }
    
    
    via le test : 
    
    [Fact]
    public void isahdsa()
    {
        ClockHelper.OneTimeMap();
        _clockHelper.dbfds();
    }
**/