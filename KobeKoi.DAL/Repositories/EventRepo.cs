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

        //get all events
        public IQueryable<Event> GetAll()
        {
            return _db.Events;
        }

        //Add new events
        public void Add(Event ev)
        {
            _db.Events.Add(ev);
            _db.SaveChanges();
        }

        //Update event
        public void Update(Event ev)
        {
            _db.Events.Update(ev);
            _db.SaveChanges();
        }

        //Delete Events
        public void Delete(Event ev)
        {
            _db.Events.Remove(ev);
            _db.SaveChanges();
        }
    }
}