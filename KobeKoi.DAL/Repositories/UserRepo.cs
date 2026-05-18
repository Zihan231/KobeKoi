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

        public IQueryable<User> GetAll()
        {
            return _db.Users;
        }
    }
}
