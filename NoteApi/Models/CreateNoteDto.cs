using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoteApi.Models
{

    public class CreateNoteDto
    {
        [Required, MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(250)]
        public string Content { get; set; }
    }
}
