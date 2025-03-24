using FitLife.Models.State;
using Microsoft.AspNetCore.Components;

namespace FitLife.Components.Pages.SignUp;

public partial class PersonalDetails
{
    private class DateModel
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
    [Inject]
    private NavigationManager Navigation { get; set; } = null!;

    [Inject]
    private UserSignUpState State { get; set; } = null!;

    private readonly DateModel dateModel = new()
    {
        Day = 1,
        Month = 1,
        Year = DateTime.Now.Year
    };

    private Dictionary<string, int> MonthDict = new Dictionary<string, int>()
    {
        { "January", 1 },
        { "February", 2 },
        { "March", 3 },
        { "April", 4 },
        { "May", 5 },
        { "June", 6 },
        { "July", 7 },
        { "August", 8 },
        { "September", 9 },
        { "October", 10 },
        { "November", 11 },

        { "December", 12}
    };

    private readonly int MIN_YEAR = 1900;

    // Get the days of the month based on the selected month and year
    private IEnumerable<int> days => Enumerable.Range(1, DateTime.DaysInMonth(dateModel.Year, dateModel.Month));

    private IEnumerable<int> years => Enumerable.Range(MIN_YEAR, DateTime.Now.Year - MIN_YEAR + 1);
}


