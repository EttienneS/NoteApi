using NoteApi.Models;
using System.ComponentModel.DataAnnotations;

namespace NoteApi.Entities
{
    public class Note
    {

        [MaxLength(250)]
        public string Content { get; set; }

        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Title}";
        }
    }
}