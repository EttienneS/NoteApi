﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NoteApi.Models;

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

            if (_context.Notes.Count() == 0)
            {
                // Create a new Note if collection is empty,
                // which means you can't delete all Notes.
                Post(new Note { Title = "Test Note 1" });
            }
        }

        /// <summary>
        /// Get all notes.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Note>> Get()
        {
            return _context.Notes;
        }


        /// <summary>
        /// Deletes a specific Note.
        /// </summary>
        /// <param name="id"></param>    
        [HttpGet("{id}")]
        public ActionResult<Note> Get(int id)
        {
            return _context.Notes.First(n => n.Id == id);
        }

        /// <summary>
        /// Creates a Note.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
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
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public void Post([FromBody] Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();


            //return note;
          // return CreatedAtRoute("Get", new { id = note.Id }, note);
        }

        /// <summary>
        /// Insert a Note at a given ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "title": "Note title",
        ///        "content": "Content of the note"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="note"></param>
        /// <returns>A newly created Note</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();

            //return note;
           
        }


        /// <summary>
        /// Deletes a specific Note.
        /// </summary>
        /// <param name="id"></param>     
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.Notes.Remove(Get(id).Value);
            _context.SaveChanges();
        }
    }
}
 