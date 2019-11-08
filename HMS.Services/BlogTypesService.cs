using HMS.Data;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class BlogTypesService
    {
        public IEnumerable<BlogType> GetAllBlogTypes()
        {

            var context = new HMSContext();

            return context.BlogTypes.ToList();
        }


        //Search Functionality 
        public IEnumerable<BlogType> SearchBlogTypes(string searchTerm)
        {

            var context = new HMSContext();

            var blogTypes = context.BlogTypes.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                blogTypes = blogTypes.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return blogTypes.ToList();
        }


        public BlogType GetBlogTypeByID(int ID)
        {

            var context = new HMSContext();

            return context.BlogTypes.Find(ID);
        }



        public bool SaveBlogType(BlogType blogType)
        {

            var context = new HMSContext();

            context.BlogTypes.Add(blogType);

            return context.SaveChanges() > 0;

        }


        public bool UpdateBlogType(BlogType blogType)
        {

            var context = new HMSContext();

            context.Entry(blogType).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;

        }

        public bool DeleteBlogType(BlogType blogType)
        {

            var context = new HMSContext();

            context.Entry(blogType).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;

        }
    }
}
