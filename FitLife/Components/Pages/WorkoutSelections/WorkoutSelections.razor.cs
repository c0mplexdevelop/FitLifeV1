using FitLife.Data;
using FitLife.Models.Exercises;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Components.Pages.WorkoutSelections;

public partial class WorkoutSelections
{
    private List<Exercise> Exercises { get; set; } = new();

    [Inject]
    private DatabaseContext dbContext { get; set; } = default!;

    private bool isLoading = true;


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await Task.Delay(TimeSpan.FromSeconds(2));
        Exercises = await dbContext.Exercises.AsNoTracking().ToListAsync();

        isLoading = false;
        await InvokeAsync(StateHasChanged);
    }
}
