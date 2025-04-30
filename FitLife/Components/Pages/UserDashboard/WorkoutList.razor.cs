using FitLife.Models.Exercises;
using FitLife.Models.Intermediary;
using Microsoft.AspNetCore.Components;

namespace FitLife.Components.Pages.UserDashboard;

public partial class WorkoutList
{
    [Parameter]
    public UserExerciseSubscription Subscription { get; set; } = default!;

    [Parameter]
    public EventCallback<UserExerciseSubscription> OnCompleteClick { get; set; }

    [Parameter]
    public EventCallback<UserExerciseSubscription> OnDeleteClick { get; set; }

    private Exercise Exercise => Subscription.Exercise ?? new Exercise();

    private async Task CompleteExercise()
    {
        if (Subscription != null)
        {
            await OnCompleteClick.InvokeAsync(Subscription);
        }
    }

    private async Task DeleteExercise()
    {
        if (Subscription != null)
        {
            await OnDeleteClick.InvokeAsync(Subscription);
        }
    }
}
