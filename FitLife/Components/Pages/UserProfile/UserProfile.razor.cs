using FitLife.Auth;
using FitLife.Data;
using FitLife.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Components.Pages.UserProfile
{
    public partial class UserProfile
    {
        private string emailOverlay = "hidden";
        private string passwordOverlay = "hidden";

        private bool isOverlayOpen = false;

        private string userName = string.Empty;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;

        [Inject]
        private AuthService AuthService { get; set; } = null!;

        [Inject]
        private DatabaseContext DbContext { get; set; } = null!;

        [Inject]
        private ILogger<UserProfile> _logger { get; set; } = null!;

        private User currentUser { get; set; } = new();

        private EmailModel emailModel { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            userName = await AuthService.ReturnUserName();
            currentUser = await DbContext.Users.FirstAsync(u => u.UserName == userName);
            if (currentUser == null)
            {
                throw new InvalidOperationException("User not found in the database.");
            }
            await base.OnInitializedAsync();
        }

        private void editEmail(MouseEventArgs e)
        {
            if (!isOverlayOpen)
            {
                emailOverlay = string.Empty;
            }
            else
            {
                emailOverlay = "hidden";
            }
            isOverlayOpen = !isOverlayOpen;

            emailModel.OldEmail = currentUser.Email!;
        }

        private void closeEditEmail(MouseEventArgs e)
        {
            if (isOverlayOpen)
            {
                emailOverlay = "hidden";
            } else
            {
                emailOverlay = string.Empty;
            }
            isOverlayOpen = !isOverlayOpen;
            emailModel.OldEmail = string.Empty;

        }

        private void editPassword(MouseEventArgs e)
        {
            if (!isOverlayOpen)
            {
                passwordOverlay = string.Empty;
            }
            else
            {
                passwordOverlay = "hidden";
            }
            isOverlayOpen = !isOverlayOpen;
        }

        private void closeEditPassword(MouseEventArgs e)
        {
            if (isOverlayOpen)
            {
                passwordOverlay = "hidden";
            }
            else
            {
                passwordOverlay = string.Empty;
            }
            isOverlayOpen = !isOverlayOpen;

        }

        private void OnValidEmailSubmit()
        {
            _logger.LogInformation("Valid email submitted");
            _logger.BeginScope("EmailModel: {@EmailModel}", emailModel);
            DbContext.Entry(currentUser).State = EntityState.Modified;
            currentUser.Email = emailModel.NewEmail;
            currentUser.NormalizedEmail = emailModel.NewEmail.ToUpper();
            DbContext.Update(currentUser);
            DbContext.SaveChanges();
        }
    }
}
