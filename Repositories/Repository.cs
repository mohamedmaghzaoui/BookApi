using BookLibrary.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
//generic class
namespace Repositories
{
    public class Repository<T> where T : class
    {
        private readonly BookContext _context;
        private readonly DbSet<T> dbTable;
        

        public Repository(BookContext context)
        {
            
            _context = context;
            dbTable = context.Set<T>();  
        }
        //add a new item to db
        public void Add(T newEntity)
        {
            dbTable.Add(newEntity);
         
            _context.SaveChanges();
        }
        //delete a item 
        public void Delete(object id)
        {
            T entity = dbTable.Find(id);
            
            

            dbTable.Remove(entity);
            _context.SaveChanges();
            
        }
        //list all items
        public List<T> GetAll()
        {
            return dbTable.ToList();
        }
        //return item by id

        public T GetWithId(object id)
        {
            return dbTable.Find(id);

        }
        //update a entity
        public void Update(T entity)
        {
            dbTable.Update(entity);

            _context.SaveChanges();

        }
    }
}