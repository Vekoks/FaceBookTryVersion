using MeetLife.Data.Repository;
using MeetLife.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestServices.FakeResors
{
    public class FekaRepository<T> : IRepository<T> where T : class
    {
        public List<Post> posts;

        public FekaRepository()
        {
            posts = new List<Post>();
        }

        public void Add(T entity)
        {
            var newPost = new Post
            {
                Id = 1,
            };

            if (entity != null)
            {
                posts.Add(newPost);
            }
        }

        public IEnumerable<T> All()
        {
            return null;
        }

        public void Delete(object id)
        {
            
        }

        public void Delete(T entity)
        {
            
        }

        public T GetById(object id)
        {
            return null;
        }

        public int SaveChanges()
        {
            return 0;
        }

        public void Update(T entity)
        {
           
        }
    }
}
