using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    public class PasswordChecker
    {
        private IUserRepository userRepository;

        public PasswordChecker(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Tuple<bool, string> Verify(string password, bool isAdmin = false)
        {
            Tuple<bool, string> result;
            if (password == null)
            {
                throw new System.ArgumentNullException("password", "Password is null.");
            }
            if (isAdmin)
            {
                result = VerifyCases(password, IsLengthMoreThenTen, IsContainsDigit, IsContainsLetter,IsContainsSpecialСharacter);
            }
            else
            {
                result = VerifyCases(password, IsLengthMoreThenSeven, IsContainsDigit, IsContainsLetter);
            }
            if (result.Item1) this.userRepository.CreateUser(password);
            return result;
        }

        #region Private methods

        private Tuple<bool, string> IsContainsSpecialСharacter(string str)
        {
            Tuple<bool, string> result = new Tuple<bool, string>(true, "OK");
            if (str.Where(c => IsSpecialСharacter(c)).Count() < 1)
            {
                result = new Tuple<bool, string>(false, "Number of special characters is less then one.");
            }
            return result;
        }
        private Tuple<bool, string> IsContainsDigit(string str)
        {
            Tuple<bool, string> result = new Tuple<bool, string>(true, "OK");
            if (str.Where(c => char.IsDigit(c)).Count() < 1)
            {
                result = new Tuple<bool, string>(false, "Number of digits is less then one.");
            }
            return result;
        }
        private Tuple<bool, string> IsContainsLetter(string str)
        {
            Tuple<bool, string> result = new Tuple<bool, string>(true, "OK");
            if (str.Where(c => char.IsLetter(c)).Count() < 1)
            {
                result = new Tuple<bool, string>(false, "Number of letters is less then one.");
            }
            return result;
        }
        private Tuple<bool, string> IsLengthMoreThenSeven(string str)
        {
            Tuple<bool, string> result = new Tuple<bool, string>(true, "OK");
            if (str.Length < 7)
            {
                result = new Tuple<bool, string>(false, "Length is less then seven.");
            }
            return result;
        }
        private Tuple<bool, string> IsLengthMoreThenTen(string str)
        {
            Tuple<bool, string> result = new Tuple<bool, string>(true, "OK");
            if (str.Length < 10)
            {
                result = new Tuple<bool, string>(false, "Length is less then ten.");
            }
            return result;
        }
        private bool IsSpecialСharacter(char c)
        {
            bool result = false;
            switch (c)
            {
                case '!':
                case '@':
                case '#':
                case '$':
                case '%':
                case '^':
                case '&':
                case '*':
                    {
                        result = true;
                    }
                    break;
                default:
                    {
                        result = false;
                    }
                    break;
            }
            return result;
        }

        private Tuple<bool, string> VerifyCases(string str, params Func<string, Tuple<bool, string>>[] stringCases)
        {
            Tuple<bool, string> result;
            foreach (var stringCase in stringCases)
            {
                result = stringCase(str);
                if(result.Item1 == false)
                {
                    return result;
                }
            }
            return new Tuple<bool, string>(true, "OK");
        }

        #endregion
    }
}
