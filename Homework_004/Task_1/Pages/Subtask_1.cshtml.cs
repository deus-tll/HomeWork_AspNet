using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Task_1.Pages
{
    public class Subtask_1Model : PageModel
    {
        private string? _isLeap;
        public string Conditions { get; set; } = "Output: the number of the current day in the year, " +
            "as well as information about what year it is now, " +
            "indicating whether it is a leap year or not.";

        public int Year { get; set; }
        public string? Isleap
        {
            get
            {
                return _isLeap;
            }
            set
            {
                if(value == "Yes" || value == "No")
                {
                    _isLeap = value;
                }
            }
        }

        public void OnGet()
        {
            Year = DateTime.Now.Year;

            if (IsLeapYear(Year)) Isleap = "Yes";
            else Isleap = "No";
        }

        public bool IsLeapYear(int year)
        {
            if(!(year % 4 == 0)) return false;
            if(!(year % 100 == 0)) return true;
            if (!(year % 400 == 0)) return false;
            return true;
        }
    }
}
