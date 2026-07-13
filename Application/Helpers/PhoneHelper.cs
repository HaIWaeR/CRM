namespace Application.Helpers
{
    public static class PhoneHelper
    {
        /// <summary>
        /// Приводит номер к формату +7 000 000 00 00 
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static string? FormatPhone(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return null;

            string digits = new string(phone.Where(c => char.IsDigit(c) || c == '+').ToArray());

            if (digits.Length == 0)
                return null;

            if (digits.StartsWith("+"))
            {
                if (digits.Length == 12 && digits.StartsWith("+7"))
                    return $"{digits[..2]} {digits[2..5]} {digits[5..8]} {digits[8..10]} {digits[10..12]}";

                return digits;
            }

            if (digits.Length == 11 && digits.StartsWith("8"))
            {
                digits = "+7" + digits[1..];
                return $"+7 {digits[2..5]} {digits[5..8]} {digits[8..10]} {digits[10..12]}";
            }

            if (digits.Length == 10)
            {
                digits = "+7" + digits;
                return $"+7 {digits[2..5]} {digits[5..8]} {digits[8..10]} {digits[10..12]}";
            }

            return digits;
        }
    }
}
