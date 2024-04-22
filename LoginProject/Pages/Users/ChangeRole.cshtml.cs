using LoginProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginProject.Pages.Users {
    public class ChangeRoleModel : PageModel {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<User> _users;

        public ChangeRoleModel(Data.ApplicationDbContext context, UserManager<User> users) {
            _context = context;
            _users = users;
        }
        public async Task<IActionResult> OnPost(Guid id, string role) {
            if (!User.Identity!.IsAuthenticated)
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            if (!User.IsInRole("admin"))
                return Forbid();

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            var roles = await _users.GetRolesAsync(user);

            if (roles.Contains("admin"))
                return Forbid();

            switch (role) {
                case "moderator":
                    if (roles.Contains("moderator"))
                        await _users.RemoveFromRoleAsync(user, "moderator");
                    else
                        await _users.AddToRoleAsync(user, "moderator");
                    break;
            }

            return RedirectToPage("./User", new { username = user.UserName });
        }
    }
}
