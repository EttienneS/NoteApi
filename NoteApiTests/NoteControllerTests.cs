using Microsoft.EntityFrameworkCore;
using NoteApi.Controllers;
using NoteApi.Entities;
using NoteApi.Models;
using System;
using System.Linq;
using Xunit;

namespace NoteApiTests
{
    public class NoteControllerTests
    {



        [Fact]
        public void ReturnNothingIfNoNotesConfigured()
        {
            var options = new DbContextOptionsBuilder<NoteContext>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using (var context = new NoteContext(options))
            {
                var controller = new NotesController(context);
                Assert.Empty(controller.Get().Value);
            }
        }

        [Fact]
        public void ReturnMultipleNodesIfSupplied()
        {
            var options = new DbContextOptionsBuilder<NoteContext>()
                           .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using (var context = new NoteContext(options))
            {
                var controller = new NotesController(context);
                controller.Post(new Note("Test1"));
                controller.Post(new Note("Test2"));
                controller.Post(new Note("Test3"));
            }

            using (var context = new NoteContext(options))
            {
                Assert.Equal(3, new NotesController(context).Get().Value.Count());
            }
        }

        [Fact]
        public void ReturnNodeById()
        {
            var options = new DbContextOptionsBuilder<NoteContext>()
                           .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            int id1, id2, id3;
            using (var context = new NoteContext(options))
            {
                var controller = new NotesController(context);
                id1 = controller.Post(new Note("Test1")).Value.Id;
                id2 = controller.Post(new Note("Test2")).Value.Id;
                id3 = controller.Post(new Note("Test3")).Value.Id;
            }

            using (var context = new NoteContext(options))
            {
                Assert.Equal("Test3", new NotesController(context).Get(id3).Value.Title);
                Assert.Equal("Test1", new NotesController(context).Get(id1).Value.Title);
                Assert.Equal("Test2", new NotesController(context).Get(id2).Value.Title);
            }
        }

        [Fact]
        public void UpdateNote()
        {
            var options = new DbContextOptionsBuilder<NoteContext>()
                           .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using (var context = new NoteContext(options))
            {
                var controller = new NotesController(context);
                controller.Post(new Note("Test1"));
                controller.Post(new Note("Test2"));
                controller.Post(new Note("Test3"));
                controller.Put(1, new Note("TestX"));
            }

            using (var context = new NoteContext(options))
            {
                Assert.Equal("TestX", new NotesController(context).Get(1).Value.Title);
                Assert.Equal(3, new NotesController(context).Get().Value.Count());
            }
        }

        [Fact]
        public void DeleteNote()
        {
            var options = new DbContextOptionsBuilder<NoteContext>()
                           .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using (var context = new NoteContext(options))
            {
                var controller = new NotesController(context);
                controller.Post(new Note("Test1"));
                controller.Post(new Note("Test2"));
                controller.Post(new Note("Test3"));
            }

            using (var context = new NoteContext(options))
            {
                var controller = new NotesController(context);
                controller.Delete(controller.Get().Value.First().Id);

                Assert.Equal(2, controller.Get().Value.Count());
            }
        }

    }
}
