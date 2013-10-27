using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NerdDinner.Core.Common
{
    //Repurpose Generic Repository Class for PCL
    // from http://www.tugberkugurlu.com/archive/generic-repository-pattern-entity-framework-asp-net-mvc-and-unit-testing-triangle
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();
    }


    public abstract class CoreGenericRepository<C, T> : IGenericRepository<T>
        where T : class
        where C : class, new()
    {
        //Deciding whether to use this Platform Abstraction Pattern in 
        //the CoreGenericRepository since I am now building separate Repository objects 
        //for each Objects in the Model that will inheirit from CoreGenericRepository
        public static CoreGenericRepository<C, T> PlatformInstance{get; set;}
 
        private C _entities = new C();
        public C DataContext
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = PlatformInstance.GetAll();
            return query;
        }

        public virtual IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
           IQueryable<T> query = PlatformInstance.FindBy(predicate);
            return query;
        }

        public virtual void Add(T entity)
        {
            PlatformInstance.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            PlatformInstance.Delete(entity);
        }

        public virtual void Edit(T entity)
        {
            PlatformInstance.Edit(entity);
        }

        public virtual void Save()
        {
            PlatformInstance.Save();
        }

    }
}
