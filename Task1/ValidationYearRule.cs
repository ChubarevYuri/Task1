using System.Windows.Controls;

namespace Task1
{
    internal class ValidationYearRule : ValidationRule
    {

        //Контроль что введено число
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int result;
            if (int.TryParse((string)value, out result))
            {
                if (result > 0 && result < 10000)
                {
                    return new ValidationResult(true, null);
                }
            }
            return new ValidationResult(false, "Введите число.");
        }
}
}
