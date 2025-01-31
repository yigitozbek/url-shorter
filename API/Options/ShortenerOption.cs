namespace API.Options;

public class ShortenerOption
{
    public required string BaseUrl { get; set; }
    public required short ShortUrlLength { get; set; }
    public required string ShortUrlCharacters { get; set; }
    public required short ShortUrlExpiration { get; set; }
}