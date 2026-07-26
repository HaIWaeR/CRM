using System.Text;

namespace Application.Helpers
{
    public static class OrderNumberGenerator
    {
        public static string Generate(string serviceName)
        {
            string prefix = GetPrefix(serviceName);

            string datePart = DateTime.UtcNow.ToString("yy") + DateTime.UtcNow.Day.ToString("D2");

            string ticks = DateTime.UtcNow.Ticks.ToString()[^4..];

            return $"{prefix}-{datePart}-{ticks}";
        }

        private static string GetPrefix(string serviceName)
        {
            string[] words = serviceName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            StringBuilder prefix = new StringBuilder();

            foreach (string word in words)
            {
                if (word.Length <= 2)
                    continue;

                prefix.Append(char.ToUpper(word[0]));

                if (prefix.Length >= 4)
                    break;
            }

            if (prefix.Length == 0)
                return "ZERO";

            return prefix.ToString();
        }
    }
}