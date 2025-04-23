using FitLife.Data;
using FitLife.Models.Exercises;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FitLife.Components.Pages.WorkoutSelections;

public partial class WorkoutSelections
{
    private List<Exercise> Exercises { get; set; } = new();

    [Inject]
    private DatabaseContext dbContext { get; set; } = default!;

    private bool isLoading = true;

    private WorkoutTypeFilter selectedWorkoutType = WorkoutTypeFilter.All;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await Task.Delay(TimeSpan.FromSeconds(2));
        Exercises = await dbContext.Exercises.AsNoTracking().ToListAsync();

        isLoading = false;
        await InvokeAsync(StateHasChanged);
    }

    private async Task SetWorkoutTypeFilter(WorkoutTypeFilter workoutType)
    {
        selectedWorkoutType = workoutType;
        isLoading = true;
        Exercises.Clear();
        if (workoutType == WorkoutTypeFilter.All)
        {
            Exercises = await dbContext.Exercises.AsNoTracking().ToListAsync();
        }
        else
        {
            Exercises = await dbContext.Exercises
            .AsNoTracking()
            .Where(e => e.Type.Equals(workoutType.ToString()))
            .ToListAsync();
        }
        await Task.Delay(TimeSpan.FromMilliseconds(500));
        isLoading = false;
        StateHasChanged();
    }
}

public enum WorkoutTypeFilter
{
    All,
    Cardio,
    Strength,
    Flexibility,
    Core,
    Plyometrics,
    Power,
    Mobility
}
