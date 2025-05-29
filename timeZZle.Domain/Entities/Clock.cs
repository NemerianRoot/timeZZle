using System.ComponentModel.DataAnnotations;
using timeZZle.Domain.Utils;
using timeZZle.Shared.Enums;

namespace timeZZle.Domain.Entities;

public class Clock : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public int Size { get; set; }
    public ICollection<Box>? Boxes { get; set; }
    public Difficulty Difficulty { get; set; }
}