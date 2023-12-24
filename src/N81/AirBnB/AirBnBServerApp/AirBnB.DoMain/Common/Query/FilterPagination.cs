namespace AirBnB.DoMain.Common.Query;

public class FilterPagination
{
    public uint PageToken { get; set; } = 1;
    public uint PageSize { get; set; } = 10;

    public FilterPagination()
    {
        
    }

    public FilterPagination(uint pageToken, uint pageSize)
    {
        PageSize = pageSize;
        PageToken = pageToken;
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        hashCode.Add(PageToken);
        hashCode.Add(PageSize);

        return hashCode.ToHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is FilterPagination filterPagination && filterPagination.GetHashCode() == GetHashCode();
    }
}
