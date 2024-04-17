﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoginProject.Data;
using LoginProject.Models;

namespace LoginProject.Pages.Posts
{
    public class DetailsModel : PageModel
    {
        private readonly LoginProject.Data.ApplicationDbContext _context;

        public DetailsModel(LoginProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Post Post { get; set; } = default!;
        public Guid? RootPostId { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            else
            {
                Post = post;
                if (Post.RootPostId != null) {
                    RootPostId = Post.RootPostId;
                } else {
                    RootPostId = Post.PostId;
                }
            }
            return Page();
        }
    }
}
