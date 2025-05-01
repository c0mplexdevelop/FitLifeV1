using FitLife.Models.Exercises;
using FitLife.Models.Intermediary;
using FitLife.Models.Intermediary.Interfaces;
using Microsoft.AspNetCore.Components;

namespace FitLife.Components.Pages.UserDashboard;

public partial class WorkoutList
{
    [Parameter]
    public IIntermediaryBase IntermediaryBase { get; set; } = default!;

    [Parameter]
    public EventCallback<IIntermediaryBase> OnCompleteClick { get; set; }

    [Parameter]
    public EventCallback<IIntermediaryBase> OnDeleteClick { get; set; }

    [Parameter]
    public bool IsHistory { get; set; } = false;

    private Exercise Exercise => IntermediaryBase.Exercise ?? new Exercise();

    private async Task CompleteExercise()
    {
        if (IntermediaryBase != null)
        {
            await OnCompleteClick.InvokeAsync(IntermediaryBase);
        }
    }

    private async Task DeleteExercise()
    {
        if (IntermediaryBase != null)
        {
            await OnDeleteClick.InvokeAsync(IntermediaryBase);
        }
    }
}
