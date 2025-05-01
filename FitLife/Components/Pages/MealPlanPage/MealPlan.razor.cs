using Microsoft.AspNetCore.Components.Web;

namespace FitLife.Components.Pages.MealPlanPage
{
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
