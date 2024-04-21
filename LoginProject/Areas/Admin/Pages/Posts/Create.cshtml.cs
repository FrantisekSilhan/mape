using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LoginProject.Data;
using LoginProject.Models;

namespace LoginProject.Areas.Admin.Pages.Posts
{
    public class CreateModel : PageModel
    {
        private readonly LoginProject.Data.ApplicationDbContext _context;

        public CreateModel(LoginProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id");
        ViewData["ParentPostId"] = new SelectList(_context.Posts, "PostId", "PostId");
        ViewData["RootPostId"] = new SelectList(_context.Posts, "PostId", "PostId");
            return Page();
        }

        [BindProperty]
        public Post Post { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
