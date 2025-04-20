using Microsoft.AspNetCore.Components.Web;

namespace FitLife.Components.Pages.UserProfile
{
    public partial class UserProfile
    {
        private string emailOverlay = "hidden";
        private string passwordOverlay = "hidden";

        private bool isOverlayOpen = false;

        private void editEmail(MouseEventArgs e)
        {
            if (isOverlayOpen)
            {
                emailOverlay = string.Empty;
            }
            else
            {
                emailOverlay = "hidden";
            }
            isOverlayOpen = !isOverlayOpen;
        }

        private void closeEditEmail(MouseEventArgs e)
        {
            if (!isOverlayOpen)
            {
                emailOverlay = "hidden";
            } else
            {
                emailOverlay = string.Empty;
            }
        }

        private void editPassword(MouseEventArgs e)
        {
            if (isOverlayOpen)
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
            if (!isOverlayOpen)
            {
                passwordOverlay = "hidden";
            }
            else
            {
                passwordOverlay = string.Empty;
            }
        }
    }
}
