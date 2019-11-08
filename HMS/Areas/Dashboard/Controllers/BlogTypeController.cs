using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using HMS.Areas.Dashboard.ViewModels;
using HMS.Entities;
using HMS.Services;

namespace HMS.Areas.Dashboard.Controllers
{
    public class BlogTypeController : Controller
    {
        BlogTypesService blogTypesService = new BlogTypesService();

        // GET: Dashboard/AccomodationTypes
        public ActionResult Index(string searchTerm)
        {
            //BlogTypesListingModel model = new BlogTypesListingModel();

            //model.SearchTerm = searchTerm;

            //model.BlogTypes = blogTypesService.SearchBlogTypes(searchTerm);

            //return PartialView("_Listing", model);

            return View();
        }

        public ActionResult Listing()
        {
            BlogTypesListingModel model = new BlogTypesListingModel();
            model.BlogTypes = blogTypesService.GetAllBlogTypes();
            return PartialView("_Listing", model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            BlogTypeActionModel model = new BlogTypeActionModel();

            if (ID.HasValue)//we are trying to edit a record
            {
                var blogType = blogTypesService.GetBlogTypeByID(ID.Value);
                model.ID = blogType.ID;
                model.Name = blogType.Name;
                model.Title = blogType.Title;
                model.Description = blogType.Description;
            }
            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(BlogTypeActionModel model)
        {

            JsonResult json = new JsonResult();
            
            var result = false;


            if (model.ID > 0)//we are trying to edit a record
            {
                var blogType = blogTypesService.GetBlogTypeByID(model.ID);
                blogType.Name = model.Name;
                blogType.Title = model.Title;
                blogType.Description = model.Description;

                result = blogTypesService.UpdateBlogType(blogType);
            }
            else  //we are trying to create a record
            {
                BlogType blogType = new BlogType();

                blogType.Name = model.Name;
                blogType.Title = model.Title;
                blogType.Description = model.Description;
                result = blogTypesService.SaveBlogType(blogType);
            }



            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on blog" };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            BlogTypeActionModel model = new BlogTypeActionModel();

            var blogType = blogTypesService.GetBlogTypeByID(ID);
            model.ID = blogType.ID;
            return PartialView("_Delete", model);
        }


        [HttpPost]
        public JsonResult Delete(BlogTypeActionModel model)
        {

            JsonResult json = new JsonResult();

            var result = false;

            var blogType = blogTypesService.GetBlogTypeByID(model.ID);

            result = blogTypesService.DeleteBlogType(blogType);



            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Blog" };
            }

            return json;
        }

    }
}