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
    public class WritterService
    {
        private BookContext context;
        public WritterService(BookContext bookContext)
        {
            context = bookContext;
        }
        public Writter GetById(int id)
        {
            return context.writter.SingleOrDefault(x => x.Id == id);
            
        }

        public List<Writter> GetAll()
        {
            return context.writter.ToList();
        }

        public Writter SearchBy(string firstName, string lastName, string nicname)
        {
            return context.writter.Where(x => x.FirstName == firstName && x.LastName==lastName && (nicname==null || nicname =="" ?true: x.Nicname==nicname)).SingleOrDefault();
        }

        public void Add(Writter writter)
        {
            context.writter.Add(writter);
            context.SaveChanges();
        }
        public void Update(Writter writter)
        {
            var item = context.writter.Find(writter.Id);
            if (item != null)
            {
                context.Entry(item).CurrentValues.SetValues(writter);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var writter = context.writter.Find(id);
            if (writter != null)
            {
                context.writter.Remove(writter);
                context.SaveChanges();
            }
        }
    }
}
