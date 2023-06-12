using System.Text.RegularExpressions;

namespace MeLevaAi.Api.Helpers
{
    public class RemoveNonNumeric
    {
        public string RemoveNonNumericCharacters(string input)
        {
            Regex regex = new Regex(@"[^0-9]");
            return regex.Replace(input, "");
        }
    }
}
