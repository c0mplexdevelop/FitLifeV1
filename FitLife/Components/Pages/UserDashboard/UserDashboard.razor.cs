using FitLife.Auth;
using FitLife.Data;
using FitLife.Models.Exercises;
using FitLife.Models.Intermediary;
using FitLife.Models.Intermediary.Interfaces;
using FitLife.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Components.Pages.UserDashboard;

public partial class UserDashboard
{
    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;

    private string userName = string.Empty;

    [Inject]
    private ILogger<UserDashboard> Logger { get; set; } = null!;

    [Inject]
    private DatabaseContext DbContext { get; set; } = null!;

    [Inject]
    private AuthService AuthService { get; set; } = null!;

    private bool IsHistory { get; set; } = false;
    private List<UserExerciseSubscription> UserExercisesSubscriptions { get; set; } = new();
    private List<UserExerciseHistory> UserExerciseHistory { get; set; } = new();

    private int completedWorkoutsCount = 0;
    protected override async Task OnInitializedAsync()
    {
        if (AuthenticationStateTask is null)
        {
            throw new InvalidOperationException("AuthenticationStateTask is null, ensure the routes is wrapped in CascadingAuthenticationState");
        }

        var user = await AuthService.GetCurrentUser();
        userName = user.UserName ?? "N/A";

        UserExercisesSubscriptions = await DbContext.UserExerciseSubscriptions
                .Include(ues => ues.Exercise)
                .Where(ues => ues.UserId == user.Id)
                .ToListAsync();

        completedWorkoutsCount = await DbContext.UserExerciseHistory
            .CountAsync(ueh => ueh.UserId == user.Id && ueh.IsCompleted);

        UserExerciseHistory = await DbContext.UserExerciseHistory.AsNoTracking()
            .Include(ueh => ueh.Exercise)
            .Where(ueh => ueh.UserId == user.Id)
            .ToListAsync();
    }

    public async Task CompleteExercise(IIntermediaryBase intermediary)
    {
        var subscription = intermediary as UserExerciseSubscription 
            ?? new UserExerciseSubscription();
        var user = await AuthService.GetCurrentUser();
        var exercise = subscription.Exercise;
        if (exercise == null)
        {
            Logger.LogError($"Exercise with ID {subscription.ExerciseId} not found.");
            return;
        }

        var refetchedSubscription = await DbContext.UserExerciseSubscriptions
            .Include(ues => ues.Exercise)
            .FirstOrDefaultAsync(ues => ues.UserId == subscription.UserId && ues.ExerciseId == subscription.ExerciseId);
        if(refetchedSubscription == null)
        {
            UserExercisesSubscriptions.Remove(subscription);
            StateHasChanged();
            return;
        }
        var userExerciseHistory = CreateExerciseHistory(subscription, user, true);
        DbContext.UserExerciseSubscriptions.Remove(refetchedSubscription);
        DbContext.UserExerciseHistory.Add(userExerciseHistory);
        await DbContext.SaveChangesAsync();
        UserExercisesSubscriptions.Remove(subscription);
    }

    public async Task DeleteExercise(IIntermediaryBase intermediary)
    {
        var subscription = intermediary as UserExerciseSubscription 
            ?? new UserExerciseSubscription();
        var user = await AuthService.GetCurrentUser();
        var exercise = subscription.Exercise;
        if (exercise == null)
        {
            Logger.LogError($"Exercise with ID {subscription.ExerciseId} not found.");
            return;
        }
        var refetchedSubscription = await DbContext.UserExerciseSubscriptions
                    .Include(ues => ues.Exercise)
                    .FirstOrDefaultAsync(ues => ues.UserId == subscription.UserId && ues.ExerciseId == subscription.ExerciseId);
        if (refetchedSubscription == null)
        {
            UserExercisesSubscriptions.Remove(subscription);
            StateHasChanged();
            return;
        }
        var userExerciseHistory = CreateExerciseHistory(subscription, user, false);
        DbContext.UserExerciseSubscriptions.Remove(refetchedSubscription);
        DbContext.UserExerciseHistory.Add(userExerciseHistory);
        await DbContext.SaveChangesAsync();
        UserExercisesSubscriptions.Remove(subscription);
    }

    private UserExerciseHistory CreateExerciseHistory(UserExerciseSubscription subscription, User user, bool isComplete)
    {
        return new UserExerciseHistory
        {
            UserId = user.Id,
            ExerciseId = subscription.Exercise.Id,
            IsCompleted = isComplete
        };
    }
}

public enum ViewMode
{
    Ongoing,
    Completed,
    Cancelled
}