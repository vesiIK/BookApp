using BookApp.Business;
using BookApp.Data;
using BookApp.Data.Model;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Presentation
{
    public class Display
    {
        private int closeOperationId = 8;
        private BookServes bookServes = new BookServes(BookContext.GetContext());
        private WritterService writterService = new WritterService(BookContext.GetContext());
        public Display() 
        {
            //bookServes = new BookServes(BookContext.GetContext());
            Input();
        }

        private void ShowMenu()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(' ', 18) + "MENU" + new string(' ', 18));
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("1. List all books");
            Console.WriteLine("2. Add new books");
            Console.WriteLine("3. Update book");
            Console.WriteLine("4. Search book by ID");
            Console.WriteLine("5. List of all novels");
            Console.WriteLine("6. Search book by date of issche");
            Console.WriteLine("7. Delete bi ID");
            Console.WriteLine("8. End");
        }

        private void Input()
        {
            var operation = -1;
            ShowMenu();
            do
            {
                operation = int.Parse(Console.ReadLine());
                switch (operation)
                {
                    case 1:
                        PrintAllBooks();
                        break;
                    case 2:
                        Add();
                        break;
                    case 3: 
                        Update();
                        break;
                    case 4:
                        PrindById();
                        break;
                    case 5:
                        PrintOfNovels();
                        break;
                    case 6:
                        PrintByDate();
                        break;
                    case 7:
                        Delete();
                        break;
                    default: 
                       // Console.WriteLine("Do not exist");
                        break;

                }
            }
            while(operation != closeOperationId);
        }

        public void PrintAllBooks()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(' ', 16) + "BOOKS" + new string(' ', 16));
            Console.WriteLine(new string('-', 40));
            var book = bookServes.GetAll();
            foreach (var bookItem in book)
            {
                Console.WriteLine("Id: {0}\nTitle: {1}\nWritter: {2} {3} {4}\nGenre: {5}\n" +
                    "Price: {6}\nDate of issue: {7}\nPages: {8}\nDescription: {9}", bookItem.Id, bookItem.Title, 
                    bookItem.Writter.FirstName, bookItem.Writter.LastName, bookItem.Writter.Nicname, 
                    bookItem.Genre, bookItem.Price, bookItem.Date, bookItem.Pages, bookItem.Description);
                Console.WriteLine();
            }
        }

        private void PrindById()
        {
            Console.WriteLine("Entire ID to search: ");
            int id  = int.Parse(Console.ReadLine());
            Book book = bookServes.GetById(id);
            if(book != null )
            {
                Console.WriteLine(new string('-', 40));
                Console.WriteLine("ID: " + book.Id);
                Console.WriteLine("Title: " + book.Title);
                Console.WriteLine("Writter: {0} {1} {2}", book.Writter.FirstName, book.Writter.LastName, book.Writter.Nicname);
                Console.WriteLine("Genre: " + book.Genre);
                Console.WriteLine("Price: {0:f2}", book.Price);
                Console.WriteLine("Date of issue: " + book.Date);
                Console.WriteLine("Pages: " + book.Pages);
                Console.WriteLine("Description: " + book.Description);
                Console.WriteLine(new string('-', 40));
            }
        }

        private void PrintOfNovels()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(' ', 16) + "BOOKS" + new string(' ', 16));
            Console.WriteLine(new string('-', 40));
            var book = bookServes.GetByGenre("Novel");
            foreach (var bookItem in book)
            {
                Console.WriteLine("Id: {0}\nTitle: {1}\nWritter: {2} {3}\nNickname: {4}\nGenre: {5}\n" +
                    "Price: {6}\nDate of issue: {7}\nPages: {8:f2}\nDescription: {9}", bookItem.Id, bookItem.Title,
                    bookItem.Writter.FirstName, bookItem.Writter.LastName, bookItem.Writter.Nicname,
                    bookItem.Genre, bookItem.Price, bookItem.Date, bookItem.Pages, bookItem.Description);
                Console.WriteLine();
            }
        }
        
        private void PrintByDate()
        {
            Console.WriteLine("Entire date to search");
            string date = Console.ReadLine();
            var book = bookServes.GetByDate(date);
            if (book != null)
            {
                Console.WriteLine(new string('-', 40));
                foreach (var bookItem in book)
                {
                    Console.WriteLine("Id: {0}\nTitle: {1}\nWritter: {2} {3}\nNickname: {4}\nGenre: {5}\n" +
                        "Price: {6}\nDate of issue: {7}\nPages: {8:f2}\nDescription: {9}", bookItem.Id, bookItem.Title,
                        bookItem.Writter.FirstName, bookItem.Writter.LastName, bookItem.Writter.Nicname,
                        bookItem.Genre, bookItem.Price, bookItem.Date, bookItem.Pages, bookItem.Description);
                    Console.WriteLine();
                }
                Console.WriteLine(new string('-', 40));
            }
        }
        private void Add()
        {
            var firstName = "";
            var lastName = "";
            string nicname = null;
            Book book = new Book();

            Console.WriteLine("Enter title: ");
            book.Title = Console.ReadLine();
            Console.WriteLine("Enter writter first name: ");
            firstName = Console.ReadLine();
            Console.WriteLine("Enter writter last name: ");
            lastName = Console.ReadLine();
            Console.WriteLine("Enter writter nickname: ");
            nicname = Console.ReadLine();
            Console.WriteLine("Enter genre: ");
            book.Genre = Console.ReadLine();
            Console.WriteLine("Enter price: ");
            book.Price = double.Parse(Console.ReadLine());
            Console.WriteLine("Entire date of issue (year): ");
            book.Date = Console.ReadLine();
            Console.WriteLine("Enter pages: ");
            book.Pages = int.Parse(Console.ReadLine());
            Console.WriteLine("Entire description: ");
            book.Description = Console.ReadLine();

           Writter writter = writterService.SearchBy(firstName,lastName, nicname);
            if(writter == null)
            {
                writter = new Writter();
                writter.Nicname = nicname;
                writter.FirstName= firstName;
                writter.LastName= lastName;
                writterService.Add(writter);
            }
            book.Writter= writter;
            bookServes.Add(book);
        }

        private void Update()
        {
            var firstName = "";
            var lastName = "";
            string nicname = null;
            Console.WriteLine("Entire ID to search: ");
            int id = int.Parse(Console.ReadLine());
            Book book = bookServes.GetById(id);
            if(book != null )
            {
                Console.WriteLine("Enter title: ");
                book.Title = Console.ReadLine();
                Console.WriteLine("Enter writter first name: ");
                firstName = Console.ReadLine();
                Console.WriteLine("Enter writter last name: ");
                lastName = Console.ReadLine();
                Console.WriteLine("Enter writter nickname: ");
                book.Writter.Nicname = Console.ReadLine();
                Console.WriteLine("Enter genre: ");
                book.Genre = Console.ReadLine();
                Console.WriteLine("Enter price: ");
                book.Price = double.Parse(Console.ReadLine());
                Console.WriteLine("Entire date of issue (year): ");
                book.Date = Console.ReadLine();
                Console.WriteLine("Enter pages: ");
                book.Pages = int.Parse(Console.ReadLine());
                Console.WriteLine("Entire description: ");
                book.Description = Console.ReadLine();

                Writter writter = writterService.SearchBy(firstName, lastName, nicname);
                if (writter == null)
                {
                    writter = new Writter();
                    writter.Nicname = nicname;
                    writter.FirstName = firstName;
                    writter.LastName = lastName;
                    writterService.Add(writter);
                }
                book.Writter = writter;
                bookServes.Update(book);
            }
            else
            {
                Console.WriteLine("Book is no found!");
            }
        }

        private void Delete()
        {
            Console.WriteLine("Entire ID to delete: ");
            int id = int.Parse(Console.ReadLine());
            bookServes.Delete(id);
            Console.WriteLine("Done!");
        }
    }
}
