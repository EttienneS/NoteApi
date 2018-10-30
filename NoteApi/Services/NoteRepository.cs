using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoteApi.Entities;
using NoteApi.Models;

namespace NoteApi.Services
{
    public class NoteRepository : INoteRepository
    {
        private NoteContext _context;
        public NoteRepository(NoteContext context)
        {
            _context = context;
        }

        public void AddNote(Note note)
        {
            _context.Notes.Add(note);
        }

        public void Delete(Note note)
        {
            _context.Notes.Remove(note);
        }

        public Note GetNote(int noteId)
        {
            return _context.Notes.FirstOrDefault(n => n.Id == noteId);
        }

        public IEnumerable<Note> GetNotes()
        {
            return _context.Notes.OrderBy(n => n.Title).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
