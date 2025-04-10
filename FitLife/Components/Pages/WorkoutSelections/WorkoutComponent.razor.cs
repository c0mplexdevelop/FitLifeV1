using FitLife.Models.Exercises;
using Microsoft.AspNetCore.Components;

namespace FitLife.Components.Pages.WorkoutSelections;

public partial class WorkoutComponent
{
    [Parameter]
    public Exercise Exercise { get; set; } = default!;

}
