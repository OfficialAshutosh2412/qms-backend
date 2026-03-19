using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace QualityWebSystem.Hubs
{
    [Authorize(Roles = "Admin")]
    public class AdminReviewAlertHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            if (!Context.User.IsInRole("Admin"))
            {
                Context.Abort();
                return;
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
            await base.OnConnectedAsync();
        }
    }
}
