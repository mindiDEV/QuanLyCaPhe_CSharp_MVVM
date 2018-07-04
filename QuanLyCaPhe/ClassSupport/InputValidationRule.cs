using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace QuanLyCaPhe.ClassSupport
{
    public class InputValidationRule : ValidationRule
    {
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value.ToString();

            bool rt = Regex.IsMatch(input, @"(^(0?)(\.\d{2}))|(^([1-9]\d*)(\.\d{2})$)");
            if (!rt)
            {
                return new ValidationResult(false, this.ErrorMessage);
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}