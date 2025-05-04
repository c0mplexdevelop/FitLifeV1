using Microsoft.AspNetCore.Authorization;

namespace FitLife.Components.Pages.MealPlanPage
{
    [Authorize]
    public partial class MealPlan
    {
        private string showTypeOfMeal = "vegetarian-plan-container";

        private void viewVeganMeals()
        {
            showTypeOfMeal = "vegan-plan-container";
        }

        private void viewPescatarialMeals()
        {
            showTypeOfMeal = "pescatarian-plan-container";
        }

        private void viewVegetarianMeals()
        {
            showTypeOfMeal = "vegetarian-plan-container";
        }
    }
}
