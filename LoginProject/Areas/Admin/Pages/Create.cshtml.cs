using LoginProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginProject.Areas.Admin.Pages {
    public class CreateModel : PageModel {
        private readonly LoginProject.Data.ApplicationDbContext _context;

        public CreateModel(LoginProject.Data.ApplicationDbContext context) {
            _context = context;
        }

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        public UserIM Input { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            User user = new User {
                Id = Input.Id,
                FullName = Input.FullName,
                UserName = Input.UserName,
                Email = Input.Email,
                EmailConfirmed = Input.EmailConfirmed,
                PhoneNumber = Input.PhoneNumber,
                PhoneNumberConfirmed = Input.PhoneNumberConfirmed,
                TwoFactorEnabled = Input.TwoFactorEnabled,
                LockoutEnd = Input.LockoutEnd,
                LockoutEnabled = Input.LockoutEnabled,
                AccessFailedCount = Input.AccessFailedCount ?? 0,
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                NormalizedEmail = (Input.Email ?? "").ToUpper(),
                NormalizedUserName = (Input.UserName ?? "").ToUpper()
            };
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, Input.Password);
            user.SecurityStamp = Guid.NewGuid().ToString("D");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
