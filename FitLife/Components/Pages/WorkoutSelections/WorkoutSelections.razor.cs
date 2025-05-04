using FitLife.Auth;
using FitLife.Data;
using FitLife.Models.Exercises;
using FitLife.Models.Intermediary;
using FitLife.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FitLife.Components.Pages.WorkoutSelections;

[Authorize]
public partial class WorkoutSelections
{
    private List<Exercise> Exercises { get; set; } = new();

    [Inject]
    private DatabaseContext dbContext { get; set; } = default!;

    [Inject]
    private AuthService authService { get; set; } = default!;

    [Inject]
    private ILogger<WorkoutSelections> _logger { get; set; } = default!;

    private User? User { get; set; }

    private bool isLoading = true;

    private WorkoutTypeFilter selectedWorkoutType = WorkoutTypeFilter.All;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await Task.Delay(TimeSpan.FromSeconds(2));
        Exercises = await dbContext.Exercises.AsNoTracking().ToListAsync();
        var userName = await authService.ReturnUserName();
        User = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == userName); // Replace with actual user ID
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

    //TODO: REFACTOR FOR ABSTRACTION (DRY)
    private async Task AddExerciseToUser(Exercise exercise)
    {
        _logger.LogCritical("PRESSED!");
        if (User == null)
        {
            _logger.LogError("User is null");
            return;
        }

        var userExerciseSub = new UserExerciseSubscription
        {
            UserId = User.Id,
            ExerciseId = exercise.Id
        };
        _logger.LogInformation($"User: {User.UserName} Exercise: {exercise.Name} Type: {exercise.Type}");
        if (await dbContext.UserExerciseSubscriptions
            .AnyAsync(ues => ues.UserId == User.Id && ues.ExerciseId == exercise.Id))
        {
            _logger.LogInformation($"User: {User.UserName} already subscribed to exercise: {exercise.Name}");
            return;
        }
        dbContext.UserExerciseSubscriptions.Add(userExerciseSub);
        await dbContext.SaveChangesAsync();
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
