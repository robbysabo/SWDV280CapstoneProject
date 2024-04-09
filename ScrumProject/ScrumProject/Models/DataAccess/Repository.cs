using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using ScrumProject.Models.DataLayer;

namespace ScrumProject.Models.DataAccess
{
    public class Repository<T>: IRepository<T> where T : class
    {
        //dependency injection
        protected ScrumProjectContext context { get; set; }
        private DbSet<T> dbset {  get; set; }
        public Repository(ScrumProjectContext ctx) 
        {
            context = ctx;
            dbset = context.Set<T>();
        }

        //overrides
        //retrieves data with dynamically built query
        public virtual IEnumerable<T> List(QueryOptions<T> options) 
        {
            IQueryable<T> query = dbset;
            foreach(string include in options.GetIncludes())
            {
                query = query.Include(include); //include any related entities
            }
            //filtering options
            if(options.HasWhere) { query.Where(options.Where); }
            if(options.HasOrderBy) { query.OrderBy(options.OrderBy); }

            return query.ToList();
        }

        //CRUD methods
        public virtual T Get(int id) => dbset.Find(id);
        public virtual void Insert(T entity) => dbset.Add(entity);
        public virtual void Update(T entity) => dbset.Update(entity);
        public virtual void Delete(T entity) => dbset.Remove(entity);
        public virtual void Save() => context.SaveChanges();
    }
}
