using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Task_1.Pages
{
    public class Subtask_2Model : PageModel
    {
        private static Random random = new();
        public string Conditions { get; set; } = "Depending on the random value, display a phrase " +
            "that consists of 5 to 10 characters of the English alphabet, " +
            "letters can be uppercase and lowercase.";

        public string Phrase { get; set; } = String.Empty;
        public void OnGet()
        {
            Phrase = GenerateRandomPhrase();
        }

        public static string GenerateRandomPhrase()
        {
            int length = random.Next(5, 11);
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                int randomNum = random.Next(0, 52);
                char randomChar;

                if (randomNum < 26)
                    randomChar = (char)('a' + randomNum);
                else
                    randomChar = (char)('A' + (randomNum - 26));

                chars[i] = randomChar;
            }

            return new string(chars);
        }
    }
}
