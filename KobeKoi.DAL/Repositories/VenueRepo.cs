using KobeKoi.DAL.EF;
using KobeKoi.DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace KobeKoi.DAL.Repositories
{
    public class VenueRepo
    {
        private readonly KobeKoiContext _db;

        public VenueRepo(KobeKoiContext db)
        {
            _db = db;
        }
        public IQueryable<Venue> GetAll()
        {
            return _db.Venues;
        }
    }
}
