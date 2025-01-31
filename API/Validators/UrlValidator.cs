namespace API.Validators;

public static class UrlValidator
{
    public static bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}