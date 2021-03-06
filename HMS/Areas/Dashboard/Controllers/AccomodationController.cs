﻿using HMS.Areas.Dashboard.ViewModels;
using HMS.Entities;
using HMS.Services;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    
    public class AccomodationController : Controller
    {
        AccomodationService accomodationService = new AccomodationService();
        AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();
        DashboardService dashboardService = new DashboardService();


        public ActionResult Index(string searchTerm, int? accomodationPackageID, int? page)
        {

            int recordSize = 3;
            page = page ?? 1;

            AccomodastionListingModel model = new AccomodastionListingModel();

            model.SearchTerm = searchTerm;
            model.AccomodationPackageID = accomodationPackageID;

            model.Accomodations = accomodationService.SearchAccomodations(searchTerm, accomodationPackageID, page.Value, recordSize);

            model.Accomodationpackages = accomodationPackagesService.GetAllAccomodationPackages();

            var totalRecords = accomodationPackagesService.SearchAccomodationPackagesCount(searchTerm, accomodationPackageID);


            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationActionModel model = new AccomodationActionModel();

            if (ID.HasValue)//we are trying to edit a record
            {
                var accomodation = accomodationService.GetAccomodationByID(ID.Value);
                model.ID = accomodation.ID;
                model.AccomodationPackageID = accomodation.AccomodationPackageID;
                model.Name = accomodation.Name;
                model.Description = accomodation.Description;


                //picture upload work
                model.AccomodationPictures = accomodationService.GetPicturesByAccomodationID(accomodation.ID);

            }

            model.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackages();

            return PartialView("_Action", model);
        }


        [HttpPost]
        public JsonResult Action(AccomodationActionModel model)
        {

            JsonResult json = new JsonResult();

            var result = false;


            //picture upload work
            //model.PictureIDs = "90,67,23" = ["90", "67", "23"] = {90, 67, 23}
            List<int> pictureIDs = !string.IsNullOrEmpty(model.pictureIDs) ? model.pictureIDs.Split(',').Select(x => int.Parse(x)).ToList() : new List<int>();
            var pictures = dashboardService.GetPicturesByIDs(pictureIDs);



            if (model.ID > 0)//we are trying to edit a record
            {
                var accomodation = accomodationService.GetAccomodationByID(model.ID);
                accomodation.AccomodationPackageID = model.AccomodationPackageID;
                accomodation.Name = model.Name;
                accomodation.Description = model.Description;


                accomodation.AccomodationPictures.Clear();
                accomodation.AccomodationPictures.AddRange(pictures.Select(x => new AccomodationPicture() {AccomodationID =accomodation.ID, PictureID = x.ID }));




                result = accomodationService.UpdateAccomodation(accomodation);
            }
            else  //we are trying to create a record
            {
                Accomodation accomodation = new Accomodation();
                 
                accomodation.AccomodationPackageID = model.AccomodationPackageID;
                accomodation.Name = model.Name;
                accomodation.Description = model.Description;


                //picture upload work
                accomodation.AccomodationPictures = new List<AccomodationPicture>();
                accomodation.AccomodationPictures.AddRange(pictures.Select(x => new AccomodationPicture() { PictureID = x.ID }));




                result = accomodationService.SaveAccomodation(accomodation);
            }



            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation" };
            }

            return json;
        }


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccomodationActionModel model = new AccomodationActionModel();

            var accomodation = accomodationService.GetAccomodationByID(ID);
            model.ID = accomodation.ID;
            return PartialView("_Delete", model);
        }


        [HttpPost]
        public JsonResult Delete(AccomodationActionModel model)
        {

            JsonResult json = new JsonResult();

            var result = false;

            var accomodation = accomodationService.GetAccomodationByID(model.ID);

            result = accomodationService.DeleteAccomodation(accomodation);



            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation " };
            }

            return json;
        }


    }
}