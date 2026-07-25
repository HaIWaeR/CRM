namespace Application.Helpers
{
    public static class ArticleGenerator
    {
        public static string Generate(string name, string categoryCode)
        {
            string prefix = new string(name
                .Where(char.IsLetter)
                .Take(3)
                .Select(char.ToUpper)
                .ToArray());
                
            if (prefix.Length < 3)
                prefix = prefix.PadRight(3, 'X');

            string category = categoryCode.Length >= 2
                ? categoryCode[..2].ToUpper()
                : "GN";

            string suffix = DateTime.UtcNow.Ticks.ToString()[^4..];

            return $"{category}-{prefix}-{suffix}";
        }
    }
}
