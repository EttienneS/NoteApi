using NoteApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteApi.Services
{
    public interface INoteRepository
    {
        IEnumerable<Note> GetNotes();
        Note GetNote(int noteId);

        void AddNote(Note note);

        bool Save();

        void Delete(Note note);

    }
}
