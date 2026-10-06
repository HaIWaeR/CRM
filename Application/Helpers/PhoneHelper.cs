namespace Application.Helpers
{
    /// <summary>
    /// Вспомогательные методы для работы с номерами телефонов.
    /// </summary>
    public static class PhoneHelper
    {
        /// <summary>
        /// Приводит российский номер к виду <c>+7 000 000 00 00</c>.
        /// </summary>
        /// <remarks>
        /// Из строки удаляются все символы, кроме цифр и <c>+</c>. Затем:
        /// <list type="bullet">
        /// <item>11 цифр с ведущей <c>8</c> — <c>8</c> заменяется на <c>+7</c>;</item>
        /// <item>10 цифр — добавляется <c>+7</c>;</item>
        /// <item><c>+7</c> и 10 цифр — только расставляются пробелы.</item>
        /// </list>
        /// Номер, не подходящий ни под одно правило (иностранный, неполный), возвращается
        /// без форматирования — только цифры и <c>+</c>.
        /// </remarks>
        /// <param name="phone">Номер в произвольном формате; может быть <c>null</c> или пустым.</param>
        /// <returns>
        /// Отформатированный номер; номер из цифр без форматирования, если он не распознан как российский;
        /// <c>null</c>, если строка пустая или в ней нет цифр.
        /// </returns>
        /// <example>
        /// <code>
        /// PhoneHelper.FormatPhone("8 (999) 123-45-67"); // "+7 999 123 45 67"
        /// PhoneHelper.FormatPhone("9991234567");        // "+7 999 123 45 67"
        /// PhoneHelper.FormatPhone("  ");                // null
        /// </code>
        /// </example>
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
