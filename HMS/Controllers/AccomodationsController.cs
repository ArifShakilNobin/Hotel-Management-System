using HMS.Services;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class AccomodationsController : Controller
    {

        AccomodationTypesService accomodationTypeservice = new AccomodationTypesService();
        AccomodationPackagesService accomodationPackageservice = new AccomodationPackagesService();
        AccomodationService accomodationservice = new AccomodationService();
        public ActionResult Index(int accomodationTypeID,int? accomodationPackageID)
        {

            AccomodationsViewModel model = new AccomodationsViewModel();

            model.AccomodationType = accomodationTypeservice.GetAAccomodationTypeByID(accomodationTypeID);

            model.AccomodationPackages = accomodationPackageservice.GetAllAccomodationPackagesByAccomodationType(accomodationTypeID);

            model.SelectedAccomodationPackageID = accomodationPackageID.HasValue ? accomodationPackageID.Value : model.AccomodationPackages.First().ID;

            model.Accomodations = accomodationservice.GetAllAccomodationsByAccomodationPackage(model.SelectedAccomodationPackageID);

            return View(model);
        }
    }
}