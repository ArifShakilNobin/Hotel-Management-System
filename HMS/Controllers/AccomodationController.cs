using HMS.Services;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class AccomodationController : Controller
    {
        AccomodationTypesService accomodationTypeservice = new AccomodationTypesService();
        AccomodationPackagesService accomodationPackageservice = new AccomodationPackagesService();
        AccomodationService accomodationservice = new AccomodationService();
        public ActionResult Index(int accomodationTypeID, int? accomodationPackageID)
        {

            AccomodationViewModel model = new AccomodationViewModel();

            model.AccomodationType = accomodationTypeservice.GetAccomodationTypeByID(accomodationTypeID);

            model.AccomodationPackages = accomodationPackageservice.GetAllAccomodationPackagesByAccomodationType(accomodationTypeID);

            model.SelectedAccomodationPackageID = accomodationPackageID.HasValue ? accomodationPackageID.Value : model.AccomodationPackages.First().ID;

            model.Accomodations = accomodationservice.GetAllAccomodationsByAccomodationPackage(model.SelectedAccomodationPackageID);

            return View(model);
        }


        public ActionResult Details(int accomodationPackageID)
        {
            AccomodationPackageDetailsViewModel model = new AccomodationPackageDetailsViewModel();

            model.AccomodationPackage = accomodationPackageservice.GetAccomodationPackageByID(accomodationPackageID);

            return View(model);
        }

    }
}