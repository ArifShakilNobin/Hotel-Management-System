using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class BlogTypesListingModel
    {
        public IEnumerable<BlogType> BlogTypes { get; set; }
        public string SearchTerm { get; set; }
    }

    public class BlogTypeActionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }   
        public string Description { get; set; }
    }
}