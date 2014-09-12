using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Identity_test_1.Models
{
    public class UserRepository : IUserRepository
    {

        private ApplicationDbContext db;
        private OppslagRepository owner;
        private UserManager<MyUser> manager;


        public UserRepository(OppslagRepository owner, ApplicationDbContext dataContext)
        {
            this.owner = owner;
            db = dataContext;
            manager = new UserManager<MyUser>(new UserStore<MyUser>(db));
        }

        public IQueryable<MyUser> visAlleBrukere()
        {
            return manager.Users;
        }
        
        public MyUser GetUser(string username)
        {
            try
            {
               // MyUser user = await manager.FindByNameAsync(username);
               // return user;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new MyUser { UserName = "kc" };
        }

        public MyUser GetUser(int id)
        {
            try
            {
                //eturn manager.FindById(<MyUser>,id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new MyUser{ UserName="kc"};
        }
        
        public String GetUserName(MyUser user)
        {
            try
            {
                return user.UserName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

/*
        public void leggTilStudentAksess(student nyStudent, String userName)
        {
            //MembershipUser user = Membership.GetUser();
            //System.Guid userid = (System.Guid)user.ProviderUserKey;
            student_aspnetuser aksessdb = new student_aspnetuser();
            aspnet_User user = GetUser(userName);
            aksessdb.aspnetUserId = user.UserId;
            aksessdb.studentId = nyStudent.id;
            //User.Identity.
            db.student_aspnetusers.InsertOnSubmit(aksessdb);
            db.SubmitChanges();


        }
  */  
    
    }
}