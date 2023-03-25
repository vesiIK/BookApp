using BookApp.Data;
using BookApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Business
{
    public class BookServes
    {
        private BookContext context;
        public BookServes(BookContext bookContext)
        {
            context = bookContext;
        }
        public Book GetById(int id)
        {
            return context.book.Where(x => x.Id == id).Include("Writter").SingleOrDefault();
        }

        public List<Book> GetAll()
        {
            return context.book.Include("Writter").ToList();
        }

        public List<Book> GetByDate(string date)
        {
            return context.book.Where(x => x.Date == date).Include("Writter").ToList();
        }

        public List<Book> GetByGenre(string genre)
        {
            return context.book.OrderBy(x => x.Price).Where(x => x.Genre == genre).Take(3).Include("Writter").ToList();
        }

        public void Add(Book book)
        {
            context.book.Add(book);
            context.SaveChanges();
        }

        public void Update(Book book)
        {
            var item = context.book.Find(book.Id);
            if (item != null)
            {
                context.Entry(item).CurrentValues.SetValues(book);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var book = context.book.Find(id);
            if (book != null)
            {
                context.book.Remove(book);
                context.SaveChanges();
            }
        }
    }
}
