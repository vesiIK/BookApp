using BookApp.Business;
using BookApp.Data;
using BookApp.Data.Model;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            //BookServes bookServes = new BookServes(BookContext.GetContext());
            
            ////Act
            //var result = bookServes.GetAll();

            //var expectedName = "The Fellowship of the Ring";
            //Assert.That(result, expression: Is.EqualTo(expectedName));

        }
        [Test]
        public void TestBookCreature()
        {
            Book book = new Book();
            Assert.IsNotNull(book);
        }
    }
}