using BookApp.Business;
using BookApp.Data;
using BookApp.Data.Model;
using Castle.Core.Resource;
using Moq;
using System;
using System.Data.Entity;
using System.Net;
using System.Net.Sockets;

namespace TestProject1
{
    [TestFixture]
    internal class BookServiseTest
    {

        private BookServes BookService;
        private Mock<DbSet<Book>> mockBookSet;

        private Mock<DbSet<Writter>> mockWritterSet;

        private Mock<BookContext> BookContext;

        [SetUp]
        public void SetUp()
        {
            var writter = new Writter() { Id = 3, FirstName = "FirstName", LastName = "LastName", Nicname = "Nickname" };

            mockWritterSet = new Mock<DbSet<Writter>>();

            var writters = new List<Writter>();
            writters.Add(writter);
            var writtersAs = writters.AsQueryable();

            mockWritterSet.As<IQueryable<Writter>>().Setup(m => m.Provider).Returns(writtersAs.Provider);
            mockWritterSet.As<IQueryable<Writter>>().Setup(m => m.Expression).Returns(writtersAs.Expression);
            mockWritterSet.As<IQueryable<Writter>>().Setup(m => m.ElementType).Returns(writtersAs.ElementType);
            mockWritterSet.As<IQueryable<Writter>>().Setup(m => m.GetEnumerator()).Returns(writtersAs.GetEnumerator());




            mockBookSet = new Mock<DbSet<Book>>();
            var books = new List<Book>
            {
                new Book() { Id=1,Writter=writter,Title="Title", Genre="Genre", Date="2023", Price=20.99, Pages=360, Description="Description"},
                new Book() { Id=2,Writter=writter,Pages=10}
            }.AsQueryable();

            mockBookSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(books.Provider);
            mockBookSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
            mockBookSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
            mockBookSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());
            mockBookSet.Setup(m => m.Include("Writter")).Returns(mockBookSet.Object);
            mockBookSet.Setup(m => m.Find(10)).Returns(new Book());



            BookContext = new Mock<BookContext>();
            BookContext.Setup(m => m.Set<Book>()).Returns(mockBookSet.Object);
            BookContext.Setup(m => m.Set<Writter>()).Returns(mockWritterSet.Object);




            BookService = new BookServes(BookContext.Object);
        }


        [Test]
        public void TestGetaAll()
        {
            
            List<Book> resultBooks = BookService.GetAll();

            Assert.NotNull(resultBooks);
            Assert.AreEqual(resultBooks.First().Id, 1);
            Assert.AreEqual(resultBooks.First().Writter.FirstName, "FirstName");
        }

        [Test]
        public void TestMethodGetByIdAndExists()
        {

            Book book= BookService.GetById(1);
            Assert.NotNull(book);
            Assert.AreEqual(book.Id, 1);
        }

        [Test]
        public void TestMethodGetByIdAndNotExists()
        {
            Book book = BookService.GetById(10);
            Assert.IsNull(book);
        }


        [Test]
        public void AddBook()
        {
            BookService.Add(new Book());
            mockBookSet.Verify(m => m.Add(It.IsAny<Book>()), Times.Once());
            BookContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void DeleteBook()
        {
            BookService.Delete(10);
            mockBookSet.Verify(m => m.Remove(It.IsAny<Book>()), Times.Once());
            BookContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void DeleteNotSavedBook()
        {
            BookService.Delete(100);
            mockBookSet.Verify(m => m.Remove(It.IsAny<Book>()), Times.Never());
            BookContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestMethodGetByGenreAndExists()
        {
            List<Book> books = BookService.GetByGenre("Genre");
            Assert.IsNotNull(books);
            Assert.IsTrue(books.Any());
            Assert.AreEqual(1, books.Count());
        }

        [Test]
        public void TestGetByDateAndIsExists() 
        {
            List<Book> books = BookService.GetByDate("2023");
            Assert.IsNotNull(books);
            Assert.IsTrue(books.Any());
            Assert.AreEqual(1, books.Count());
        }
    }
}
