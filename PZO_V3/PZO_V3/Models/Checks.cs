using System.Globalization;
using System.Text.RegularExpressions;

namespace PZO_V3.Models
{
    public abstract class Checks
    {
        public static bool isValidString(string input, int lenght, bool allowedEmpty)
        {
            string rules = @"^[\w\s-,()\+]+$";
            if (allowedEmpty)
            {
                if (input == string.Empty)
                {
                    return true;
                }
                return isCorrectLenght(input, lenght) && Regex.IsMatch(input, rules);
            }

            return isCorrectLenght(input, lenght) && Regex.IsMatch(input, rules) && !string.IsNullOrWhiteSpace(input);
        }
        public static bool isValidDouble(string input)
        {
            return double.TryParse(input,NumberStyles.Any, CultureInfo.InvariantCulture, out double _);
        }
        public static bool isValidInt(string input)
        {
            if (isInt(input))
            {
                int num = int.Parse(input);
                if (num > -2147483648 && num < 2147483647)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool isInt(string input)
        {
            return int.TryParse(input, out var _);
        }
        public static bool isDouble(string input)
        {
            return double.TryParse(input, out double _);
        }

        public static bool isCorrectLenght(string input, int lenght)
        {
            return input.Length < lenght;
        }
    }
}
