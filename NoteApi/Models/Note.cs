using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoteApi.Models
{
    public class Note
    {
        public Note(string title)
        {
            Title = title;
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Title}";
        }
    }
}
