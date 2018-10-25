using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NoteApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace NoteApi.Controllers
{
    /// <summary>
    /// CRUD operations for Note
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NoteContext _context;

        /// <summary>
        /// Constructs the note controller class and sets up
        /// some basic notes if there are none
        /// </summary>
        /// <param name="context"></param>
        public NotesController(NoteContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Deletes a specific Note.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var note = GetNoteById(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Get all notes.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Note>> Get()
        {
            if (_context.Notes == null || !_context.Notes.Any())
            {
                return NotFound();
            }

            return Ok(_context.Notes);
        }

        /// <summary>
        /// Deletes a specific Note.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}", Name = "GetNote")]
        public ActionResult<Note> Get(int id)
        {
            var note = GetNoteById(id);

            if (note == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(note);
            }
        }

        /// <summary>
        /// Update a Note with a given ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /UpdateNoteDto
        ///     {
        ///        "title": "Note title",
        ///        "content": "Content of the note"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <response code="204">Returns nothing</response>
        /// <response code="400">If the item is null</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] JsonPatchDocument<UpdateNoteDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }


            var originalNote = GetNoteById(id);

            if (originalNote == null)
            {
                return NotFound();
            }

            var noteToPatch = new UpdateNoteDto()
            {
                Title = originalNote.Title,
                Content = originalNote.Content,
            };

            patchDoc.ApplyTo(noteToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            originalNote.Title = noteToPatch.Title;
            originalNote.Content = noteToPatch.Content;

            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Creates a Note.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Note
        ///     {
        ///        "id": 1,
        ///        "title": "Note title",
        ///        "content": "Content of the note"
        ///     }
        ///
        /// </remarks>
        /// <param name="note"></param>
        /// <returns>A newly created Note</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null or invalid</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Note> Post([FromBody] CreateNoteDto note)
        {
            if (note == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var noteEntity = _context.Notes.Add(new Note(note));
            _context.SaveChanges();

            return CreatedAtRoute("GetNote", new { id = noteEntity.Entity.Id }, noteEntity.Entity);
        }

        /// <summary>
        /// Update a Note with a given ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Note
        ///     {
        ///        "title": "Note title",
        ///        "content": "Content of the note"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="note"></param>
        /// <response code="204">Returns nothing</response>
        /// <response code="400">If the item is null</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UpdateNoteDto note)
        {
            if (note == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var originalNote = GetNoteById(id);

            if (originalNote == null)
            {
                return NotFound();
            }

            originalNote.Title = note.Title;
            originalNote.Content = note.Content;

            _context.SaveChanges();

            return NoContent();
        }

        private Note GetNoteById(int id)
        {
            return _context.Notes.FirstOrDefault(n => n.Id == id);
        }
    }
}