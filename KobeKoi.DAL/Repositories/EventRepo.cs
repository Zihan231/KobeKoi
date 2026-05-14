using KobeKoi.DAL.EF;
using KobeKoi.DAL.EF.Tables;
using System.Collections.Generic;
using System.Linq;
using System;

namespace KobeKoi.DAL.Repositories
{
    public class EventRepo
    {
        private readonly KobeKoiContext _db;

        public EventRepo(KobeKoiContext db)
        {
            _db = db;
        }

        // Return IQueryable so callers can compose EF queries (Include/Where) before materializing
        public IQueryable<Event> GetAll()
        {
            return _db.Events;
        }
    }
}