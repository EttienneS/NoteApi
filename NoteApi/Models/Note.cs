using System.ComponentModel.DataAnnotations;

namespace NoteApi.Models
{
    public class Note
    {
        public Note(string title)
        {
            Title = title;
        }

        public Note(CreateNoteDto note)
        {
            Title = note.Title;
            Content = note.Content;
        }

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