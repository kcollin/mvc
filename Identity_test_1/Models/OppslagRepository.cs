using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Identity_test_1.Models
{
    public class OppslagRepository : IOppslagRepository
    {
        private ApplicationDbContext db;
        public UserRepository Userdb { get; private set; }

        public OppslagRepository()
        {

            db = new ApplicationDbContext();
            Userdb = new UserRepository(this, db);
        }

        public List<Oppslag> visAlleOppslag()
        {
            return db.Oppslag.ToList();
        }


        
        public List<kommentar> visKommentarer(Oppslag parent)
        {
 
            var kommentarer = ( from k in db.Kommentarer
                                where k.oppslag.oppslagId == parent.oppslagId
                                select k).ToList();

            return kommentarer;
        }


        public Oppslag hentOppslag(int id)
        {

          // Oppslag op = (from o in db.Oppslag
          //               where o.id == id
          //               select o).FirstOrDefault();
            //op.kommentarer = visKommentarer(id);
           Oppslag op1 = db.Oppslag.Find(id);
           return op1;
        }

        public void oppdaterOppslag(Oppslag o)
        {
            lagre();

        }
        //public void leggTilOppslag(Oppslag nyttOppslag, MyUser user)
        public void leggTilOppslag(Oppslag nyttOppslag,  System.Security.Principal.IIdentity identity )
        {

            //MembershipUser user = Membership.GetUser();
            //System.Guid userid = (System.Guid)user.ProviderUserKey;
            
            //UserProfile user = Userdb.GetUser(userName);
            //nyttOppslag.brukerID = user.UserId;
            var myUserManager = new MyUserManager(new UserStore<MyUser>(db));
            var currentUser = myUserManager.FindById(identity.GetUserId()); 

            nyttOppslag.dato = System.DateTime.Now;
            nyttOppslag.eier = currentUser;
            db.Oppslag.Add(nyttOppslag);
            lagre();
            

        }

        public void slettOppslag(Oppslag slettOppslag)
        {
            db.Oppslag.Remove(slettOppslag);
            lagre();

        }

        private void lagre()
        {
              db.SaveChanges();
        }

        public List<kategori> visAlleKategorier()
        {
            var query = (from b in db.Kategorier
                         select b).ToList();

            return query;

        }
        public kategori visKategori(Oppslag oppslag)
        {
            var query = (from b in db.Kategorier
                         join o in db.Oppslag
                         on b.kategoriId equals o.kategori.kategoriId
                         where o.oppslagId == oppslag.oppslagId 
                         select b).FirstOrDefault();
            return query;
        }

        public String GetUserName(MyUser user)
        {
            return user.UserName;
        }
    }
}