using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Data.Model
{
    public class Book
    {
        public int Id { get; set; }
        public Writter Writter { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public string Date { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; }
    }
}
