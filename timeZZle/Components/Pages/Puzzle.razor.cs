using Microsoft.AspNetCore.Components;
using timeZZle.Dtos.Clocks;

namespace timeZZle.Components.Pages;

public partial class Puzzle(
    HttpClient httpClient) : ComponentBase
{
    [Parameter] public ClockDto? CurrentClock { get; set; }
    public Dictionary<int, BoxDto>? BoxByPosition { get; set; }

    private int? _selectedPosition;
    private HashSet<BoxDto> _consumedBoxes = [];

    private HashSet<BoxDto> _nextPossibilities = [];
    // private HashSet<int> _nextPossibilities = [];
    // private int _clockwiseIndex;
    // private int _counterClockwiseIndex;

    protected override async Task OnInitializedAsync()
    {
        if (this.CurrentClock is not null)
        {
            return;
        }

        using var httpResponseMessage = await httpClient.GetAsync("http://localhost:5299/api/clocks/random");

        this.CurrentClock = await httpResponseMessage.Content.ReadFromJsonAsync<ClockDto>();
        this.BoxByPosition = this.CurrentClock?.Boxes.ToDictionary(o => o.Position);
    }


    private void Reset()
    {
        _consumedBoxes = [];
        _selectedPosition = null;
    }

    private void Select(BoxDto clickedBox)
    {
        if (_consumedBoxes.Contains(clickedBox) ||
            (_selectedPosition is not null) && !_nextPossibilities.Contains(clickedBox))
        {
            return;
        }

        _selectedPosition = clickedBox.Position;
        _consumedBoxes.Add(clickedBox);

        var step = clickedBox.Value;
        var position = clickedBox.Position;

        var nextClockwisePosition = (position + step) % this.CurrentClock!.Size;
        var nextCounterClockwisePosition = (position - step + this.CurrentClock.Size) % this.CurrentClock.Size;

        var clockwisePossibility = this.BoxByPosition![nextClockwisePosition];
        var counterClockwisePossibility = this.BoxByPosition![nextCounterClockwisePosition];

        if (nextClockwisePosition == nextCounterClockwisePosition)
        {
            _nextPossibilities = [clockwisePossibility];
            return;
        }

        _nextPossibilities = [clockwisePossibility, counterClockwisePossibility];
    }

    private string GetClass(BoxDto box)
    {
        if (_consumedBoxes.Contains(box))
        {
            return "consumed";
        }

        if (_selectedPosition is null || _nextPossibilities.Contains(box))
        {
            return "selectable";
        }

        return "disabled";
    }
}