using HMS.Entities;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccomodastionListingModel
    {
        public IEnumerable<Accomodation> Accomodations { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<AccomodationPackage> Accomodationpackages { get; set; }
        public int? AccomodationPackageID { get; set; }
        public Pager Pager { get; set; }
    }

    public class AccomodationActionModel
    {

        public int ID { get; set; }
        public int AccomodationPackageID { get; set; }
        public AccomodationPackage AccomodationPackage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string pictureIDs { get; set; }



        public List<AccomodationPicture> AccomodationPictures { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
    }

}