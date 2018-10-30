using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NoteApi.Entities;
using NoteApi.Models;
using NoteApi.Services;
using System;
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
        private readonly INoteRepository _noteRepository;

        /// <summary>
        /// Constructs the note controller class and sets up
        /// some basic notes if there are none
        /// </summary>
        /// <param name = "context" ></ param >
        public NotesController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        /// <summary>
        /// Deletes a specific Note.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var note = _noteRepository.GetNote(id);
            if (note == null)
            {
                return NotFound();
            }

            _noteRepository.Delete(note);

            if (!_noteRepository.Save())
            {
                return StatusCode(500, "A problem happened while saving your request");
            }

            return NoContent();
        }

        /// <summary>
        /// Get all notes.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<NoteDto>> Get()
        {
            var notes = _noteRepository.GetNotes();
            if (notes == null || !notes.Any())
            {
                return NotFound();
            }

            return Ok(Mapper.Map<IEnumerable<NoteDto>>(notes));
        }

        /// <summary>
        /// Get a specific Note.
        /// </summary>
        /// <param name = "id" ></ param >
        [HttpGet("{id}", Name = "GetNote")]
        public ActionResult<NoteDto> Get(int id)
        {
            var note = _noteRepository.GetNote(id);
            if (note == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(Mapper.Map<NoteDto>(note));
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

            var originalNote = _noteRepository.GetNote(id);
            if (originalNote == null)
            {
                return NotFound();
            }

            var noteToPatch = Mapper.Map<UpdateNoteDto>(originalNote);
            patchDoc.ApplyTo(noteToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Mapper.Map(noteToPatch, originalNote);

            if (!_noteRepository.Save())
            {
                return StatusCode(500, "A problem happened while saving your request");
            }

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

            var finalNote = Mapper.Map<Note>(note);
            _noteRepository.AddNote(finalNote);
            if (!_noteRepository.Save())
            {
                return StatusCode(500, "A problem happened while saving your request");
            }

            return CreatedAtRoute("GetNote", new { id = finalNote.Id }, finalNote);
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

            var originalNote = _noteRepository.GetNote(id);
            if (originalNote == null)
            {
                return NotFound();
            }

            Mapper.Map(note, originalNote);

            if (!_noteRepository.Save())
            {
                return StatusCode(500, "A problem happened while saving your request");
            }

            return NoContent();
        }

    }
}