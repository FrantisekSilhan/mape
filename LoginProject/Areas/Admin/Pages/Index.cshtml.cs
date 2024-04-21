using LoginProject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LoginProject.Areas.Admin.Pages {
    public class IndexModel : PageModel {
        private readonly LoginProject.Data.ApplicationDbContext _context;

        public IndexModel(LoginProject.Data.ApplicationDbContext context) {
            _context = context;
        }

        public IList<User> User { get; set; } = default!;
        public string? Order { get; set; }

        public async Task OnGetAsync(string? sort, string? order) {
            Order = order;
            IQueryable<User> users = from s in _context.Users select s;

            if (!string.IsNullOrEmpty(sort) && sort == order) {
                sort += "_desc";
            } else {
                Order = sort;
            }

            if (!string.IsNullOrEmpty(sort)) {
                switch (sort) {
                    case "Id":
                        users = users.OrderBy(s => s.Id);
                        break;
                    case "Id_desc":
                        users = users.OrderByDescending(s => s.Id);
                        break;
                    case "UserName":
                        users = users.OrderBy(s => s.UserName);
                        break;
                    case "UserName_desc":
                        users = users.OrderByDescending(s => s.UserName);
                        break;
                    case "FullName":
                        users = users.OrderBy(s => s.FullName);
                        break;
                    case "FullName_desc":
                        users = users.OrderByDescending(s => s.FullName);
                        break;
                    case "Email":
                        users = users.OrderBy(s => s.Email);
                        break;
                    case "Email_desc":
                        users = users.OrderByDescending(s => s.Email);
                        break;
                    default:
                        users = users.OrderBy(s => s.UserName);
                        break;
                }
            }

            User = await users.AsNoTracking().ToListAsync();
            Order = sort;
        }
    }
}
