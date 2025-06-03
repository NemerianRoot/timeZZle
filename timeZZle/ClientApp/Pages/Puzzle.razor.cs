using Microsoft.AspNetCore.Components;
using timeZZle.ClientApp.Enums;
using timeZZle.ClientApp.Services;
using timeZZle.Dtos.Clocks;
using timeZZle.Dtos.Puzzles;

namespace timeZZle.ClientApp.Pages;

public partial class Puzzle(AppHttpClient httpClient) : ComponentBase
{
    private HashSet<PlayerPickDto> _consumedBoxes = [];
    private int? _selectedPosition;
    private ClockDto? _currentClock;
    private Dictionary<int, BoxDto>? _boxByPosition;
    private PuzzleStatus _puzzleStatus;
    private HashSet<BoxDto> _nextPossibilities = [];

    // blazor events
    protected override async Task OnInitializedAsync()
    {
        if (_currentClock is not null)
        {
            return;
        }

        using var httpResponseMessage = await httpClient.GetAsync("clocks/random");

        _currentClock = await httpResponseMessage.Content.ReadFromJsonAsync<ClockDto>();
        _boxByPosition = _currentClock?.Boxes.ToDictionary(o => o.Position);
        _puzzleStatus = PuzzleStatus.Active;
    }

    // user interactions
    private void Reset()
    {
        _consumedBoxes = [];
        _selectedPosition = null;
    }
    
    private async Task Select(BoxDto clickedBox)
    {
        if (_consumedBoxes.Any(o => o.BoxId == clickedBox.Id) ||
            (_selectedPosition is not null) && !_nextPossibilities.Contains(clickedBox))
        {
            return;
        }

        _selectedPosition = clickedBox.Position;
        _consumedBoxes.Add(new PlayerPickDto(clickedBox.Id, _consumedBoxes.Count));

        var step = clickedBox.Value;
        var position = clickedBox.Position;

        var nextClockwisePosition = (position + step) % _currentClock!.Size;
        var nextCounterClockwisePosition = (position - step + _currentClock.Size) % _currentClock.Size;

        var clockwisePossibility = _boxByPosition![nextClockwisePosition];
        var counterClockwisePossibility = _boxByPosition![nextCounterClockwisePosition];

        if (nextClockwisePosition == nextCounterClockwisePosition)
        {
            _nextPossibilities = [clockwisePossibility];
            return;
        }

        _nextPossibilities = [clockwisePossibility, counterClockwisePossibility];

        await this.UpdateGameStatus();
    }

    // private methods
    private async Task UpdateGameStatus()
    {
        if (_selectedPosition is null || this.NextPossibilitiesArePlayable())
        {
            _puzzleStatus = PuzzleStatus.Active;
            return;
        }

        if (_consumedBoxes.Count != _currentClock!.Size)
        {
            _puzzleStatus = PuzzleStatus.GameOver;
            return;
        }

        using var response = await httpClient.PostAsync("verify", _consumedBoxes);
        var isVerified = await response.Content.ReadFromJsonAsync<bool>();

        _puzzleStatus = isVerified ? PuzzleStatus.Victory : PuzzleStatus.GameOver;
    }

    private bool NextPossibilitiesArePlayable()
    {
        return !_nextPossibilities.All(o => _consumedBoxes.Any(c => c.BoxId == o.Id));
    }
}