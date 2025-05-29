namespace timeZZle.Shared.Comparers;

public class PropositionEqualityComparer : IEqualityComparer<int?[]>
{
    public bool Equals(int?[]? x, int?[]? y)
    {
        if (x == null && y == null)
        {
            return true;
        }

        if (x != null && y == null)
        {
            return false;
        }

        if (x == null && y != null)
        {
            return false;
        }
        
        if (x!.Length != y!.Length)
        {
            return false;
        }

        for (var i = 0; i < x.Length; i++)
        {
            if (!x[i].Equals(y[i]))
            {
                return false;
            }
        }

        return true;
    }

    public int GetHashCode(int?[] obj)
    {
        var result = 0;

        foreach (var nullableInt in obj)
        {
            if (!nullableInt.HasValue)
            {
                continue;
            }

            result += nullableInt!.Value.GetHashCode();
        }

        return result.GetHashCode();
    }
}