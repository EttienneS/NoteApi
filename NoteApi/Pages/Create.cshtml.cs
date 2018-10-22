﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NoteApi.Controllers;
using NoteApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteApi.Pages
{
    public class CreateModel : PageModel
    {
        private readonly NoteContext _db;

        [BindProperty]
        public Note Note { get; set; }


        public CreateModel(NoteContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            new NotesController(_db).Post(Note);
            return RedirectToPage("/Create");
        }
    }
}