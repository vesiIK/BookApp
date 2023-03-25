using BookApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Data
{
    public class BookContext : DbContext
    {
        private static BookContext instance;

        public BookContext()
        {
            // for testing
        }

        private BookContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public static BookContext GetContext()
        {
            if (instance == null)
            {
                //"Server=.\\SQLEXPRESS; Database=BooksDB; Integrated Security=true"
                instance = new BookContext("name=BookConectionString");
                instance.Database.Connection.Open();
            }
            return instance;
        }
        public DbSet<Book> book { get; set; }
        public DbSet<Writter> writter { get; set; }

    }
}
