using LoginProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LoginProject.Areas.Admin.Pages {
    public class EditModel : PageModel {
        private readonly LoginProject.Data.ApplicationDbContext _context;

        public EditModel(LoginProject.Data.ApplicationDbContext context) {
            _context = context;
        }

        [BindProperty]
        public UserIM Input { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id) {
            if (id == null) {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null) {
                return NotFound();
            }
            Input = new UserIM {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                LockoutEnd = user.LockoutEnd,
                LockoutEnabled = user.LockoutEnabled,
                AccessFailedCount = user.AccessFailedCount,
                ConcurrencyStamp = user.ConcurrencyStamp
            };

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == Input.Id);
            if (user == null) {
                return NotFound();
            }

            user.FullName = Input.FullName;
            user.UserName = Input.UserName;
            user.Email = Input.Email;
            user.EmailConfirmed = Input.EmailConfirmed;
            user.PhoneNumber = Input.PhoneNumber;
            user.PhoneNumberConfirmed = Input.PhoneNumberConfirmed;
            user.TwoFactorEnabled = Input.TwoFactorEnabled;
            user.LockoutEnd = Input.LockoutEnd;
            user.LockoutEnabled = Input.LockoutEnabled;
            user.AccessFailedCount = Input.AccessFailedCount ?? 0;
            user.NormalizedEmail = (Input.Email ?? "").ToUpper();
            user.NormalizedUserName = (Input.UserName ?? "").ToUpper();
            if (Input.Password != null)
                user.PasswordHash = new PasswordHasher<User>().HashPassword(user, Input.Password);
            user.SecurityStamp = Guid.NewGuid().ToString("D");
            user.ConcurrencyStamp = Guid.NewGuid().ToString("D");

            _context.Attach(user).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!UserExists(user.Id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserExists(Guid id) {
            return _context.Users.Any(e => e.Id == id);
        }
    }
    public class UserIM {
        [Required]
        public Guid Id { get; set; } = default!;
        public string? FullName { get; set; } = default!;
        public string? UserName { get; set; } = default!;
        public string? Email { get; set; } = default!;
        [Required]
        public bool EmailConfirmed { get; set; } = default!;
        public string? Password { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
        [Required]
        public bool PhoneNumberConfirmed { get; set; } = default!;
        [Required]
        public bool TwoFactorEnabled { get; set; } = default!;
        public DateTimeOffset? LockoutEnd { get; set; } = default!;
        [Required]
        public bool LockoutEnabled { get; set; } = default!;
        public int? AccessFailedCount { get; set; } = default!;
        public string? ConcurrencyStamp { get; set; } = default!;
    }
}
