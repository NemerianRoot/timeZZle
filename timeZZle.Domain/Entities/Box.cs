using System.ComponentModel.DataAnnotations;

namespace timeZZle.Domain.Entities;

public class Box
{
    [Key]
    public Guid Id { get; set; }
    public int Position { get; set; }
    public int Value { get; set; }
    
    public Guid ClockId { get; set; }
    public Clock? Clock { get; set; }
    
    public Box[] GetNextPossibilities()
    {
        var size = Clock!.Size;
        var after = Clock.Boxes!.First(o => o.Position == (Position + Value + size) % size);
        var before = Clock.Boxes!.First(o => o.Position == (Position - Value + size) % size);

        return before.Position == after.Position ? [after] : [before, after];
    }
}