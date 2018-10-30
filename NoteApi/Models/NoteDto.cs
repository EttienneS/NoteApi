using System.ComponentModel.DataAnnotations;

namespace NoteApi.Models
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
    }
}