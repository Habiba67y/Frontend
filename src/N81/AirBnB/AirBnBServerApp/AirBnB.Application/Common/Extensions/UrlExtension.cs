namespace AirBnB.Application.Common.Extensions;

public static class UrlExtension
{
    public static ValueTask<string> ToUrlAsync(this string relativePath) =>
    new(relativePath.Replace(@"\", "/"));
}
