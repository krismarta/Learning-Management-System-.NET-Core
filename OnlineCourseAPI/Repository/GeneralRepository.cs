using OnlineCourseAPI.Context;
using OnlineCourseAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OnlineCourseAPI.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> entities;

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }
        public int Delete(Key key)
        {
          
            var check = entities.Find(key);
            if (check != null)
            {
                entities.Remove(check);
            }
            var result = myContext.SaveChanges();
            return result;
        }
        public Entity Get(Key key)
        {
            return entities.Find(key);
        }
        public IEnumerable<Entity> Get()
        {
            return entities.ToList();
        }   

        public int Insert(Entity entity)
        {
            var result = 0;
            //try
            //{
                
                entities.Add(entity);
                result = myContext.SaveChanges();
                return result;
            //}
            //catch(Exception)
            //{
            //    return result;
            //}
        }
       
        public int Update(Entity entity, Key key)
        {
          var result = myContext.SaveChanges();
            try
            {
                myContext.Entry(entity).State = EntityState.Modified;
                result = myContext.SaveChanges();
            }
            catch
            {
                return result;
            }

            return result;
        }

        

    }
}
