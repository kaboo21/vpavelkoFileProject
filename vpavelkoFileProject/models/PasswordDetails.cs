using System.Drawing;
using System.Text.RegularExpressions;

namespace vpavelkoFileProject.models
{
    public class PasswordDetails
    {
        const int _validNumberOfWhiteSpace = 2;
        private Regex _regex = new Regex(@"^.{1}\s\d*d?-\d*d?: .+");
        private int _repeats;
        public string Password { get; set; }
        public int MinRepeats { get; set; }
        public int MaxRepeats { get; set; }
        public char Char { get; set; }
        public bool IsValidImputLine { get; set; }
        public bool IsValidPassword { get; set; }

        public PasswordDetails(string passwordLine)
        {
            ParseStringLine(passwordLine);
            PasswordValidation();
        }

        private void ParseStringLine(string passwordLine)
        {
            if (!_regex.IsMatch(passwordLine)) 
                return;

            IsValidImputLine = true;
            
            var passTrimed = passwordLine.Trim();
            if(passTrimed.Count(Char.IsWhiteSpace) > _validNumberOfWhiteSpace)
            {
                IsValidImputLine = false;
                return;
            }

            var separetedStrArr = passwordLine.Split();

            if (separetedStrArr[0].Length > 1)
            {
                IsValidImputLine = false;
                return;
            }

            //take char for validation
            Char = separetedStrArr[0][0];

            //take Min and Max number of repeats
            var repeatsValuesStrArr = separetedStrArr[1].Split('-');

            var strNum1 = repeatsValuesStrArr[0];
            var strNum2 = repeatsValuesStrArr[1].TrimEnd(':');

            if (int.TryParse(strNum1, out var num1) 
                && int.TryParse(strNum2, out var num2))
            {
                if (num1 >= num2)
                {
                    IsValidImputLine = false;
                    return;
                }

                MinRepeats = num1;
                MaxRepeats = num2;
            }

            //take password
            Password = separetedStrArr[2];
        }

        private void PasswordValidation()
        {
            if (!IsValidImputLine)
                return;
            _repeats = Password.Where(c => c == Char).Count();
            if (_repeats >= MinRepeats && _repeats <= MaxRepeats) 
                IsValidPassword = true;
        }
    }
}
