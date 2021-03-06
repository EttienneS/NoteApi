﻿using Microsoft.EntityFrameworkCore;
using NoteApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteApi.Models
{
    public class NoteContext : DbContext
    {

        public NoteContext(DbContextOptions<NoteContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Note> Notes { get; set; }
    }

}
