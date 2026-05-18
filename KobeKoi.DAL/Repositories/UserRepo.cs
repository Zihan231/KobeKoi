using KobeKoi.DAL.EF;
using KobeKoi.DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace KobeKoi.DAL.Repositories
{
    public class UserRepo
    {
        private readonly KobeKoiContext _db;

        public UserRepo(KobeKoiContext db)
        {
            _db = db;
        }

        //get all user
        public IQueryable<User> GetAll()
        {
            return _db.Users;
        }

        //add new user
        public void Add(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        //Update
        public void Update(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }
    }
}
