namespace API.Helpers;

public static class CodeGenerator
{
    public static string GenerateCode(int length, string chars)
    {
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}