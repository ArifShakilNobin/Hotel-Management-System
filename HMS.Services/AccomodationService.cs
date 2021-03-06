﻿using HMS.Data;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class AccomodationService
    {

        public IEnumerable<Accomodation> GetAllAccomodations()
        {

            var context = new HMSContext();

            return context.Accomodations.ToList();
        }

        public IEnumerable<Accomodation> GetAllAccomodationsByAccomodationPackage(int accomodationPackageID)
        {
            var context = new HMSContext();

            return context.Accomodations.Where(x => x.AccomodationPackageID == accomodationPackageID).ToList();
        }


        //Search
        public IEnumerable<Accomodation> SearchAccomodations(string searchTerm, int? accomodationPackageID, int page, int recordSize)
        {

            var context = new HMSContext();

            var accomodations = context.Accomodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodations = accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }


            if (accomodationPackageID.HasValue && accomodationPackageID.Value > 0)
            {
                accomodations = accomodations.Where(a => a.AccomodationPackageID == accomodationPackageID.Value);
            }

            var skip = (page - 1) * recordSize;

            return accomodations.OrderBy(x => x.AccomodationPackageID).Skip(skip).Take(recordSize).ToList();
        }



        //pagination
        public int SearchAccomodationsCount(string searchTerm, int? accomodationPackageID)
        {

            var context = new HMSContext();

            var accomodations = context.Accomodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodations = accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }


            if (accomodationPackageID.HasValue && accomodationPackageID.Value > 0)
            {
                accomodations = accomodations.Where(a => a.AccomodationPackageID == accomodationPackageID.Value);
            }

            return accomodations.Count();
        }


        public Accomodation GetAccomodationByID(int ID)
        {

            var context = new HMSContext();
            return context.Accomodations.Find(ID);

        }


        //save
        public bool SaveAccomodation(Accomodation accomodation)
        {

            var context = new HMSContext();

            context.Accomodations.Add(accomodation);

            return context.SaveChanges() > 0;

        }


        //Update 
        public bool UpdateAccomodation(Accomodation accomodation)
        {

            var context = new HMSContext();

            var existingAccomodation = context.Accomodations.Find(accomodation.ID);

            context.AccomodationPictures.RemoveRange(existingAccomodation.AccomodationPictures);
            context.Entry(existingAccomodation).CurrentValues.SetValues(accomodation);
            context.AccomodationPictures.AddRange(accomodation.AccomodationPictures);

           // context.Entry(accomodation).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;


        }


        //Delete
        public bool DeleteAccomodation(Accomodation accomodation)
        {

            var context = new HMSContext();

            context.Entry(accomodation).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;

        }


        public List<AccomodationPicture> GetPicturesByAccomodationID(int accomodationID)
        {
            var context = new HMSContext();

            return context.Accomodations.Find(accomodationID).AccomodationPictures.ToList();
        }


    }
}
