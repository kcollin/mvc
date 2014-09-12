using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Identity_test_1.Models;
using System.Web.Security;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Identity_test_1.Controllers
{
    public class OppslagController : Controller
    {
        private IOppslagRepository myRepository;
        
        public OppslagController()
        {
            myRepository = new OppslagRepository();
        }
        
        public OppslagController(IOppslagRepository repository)
        {
            myRepository = repository;
        }      
        //
        // GET: /Oppslag/

        public ActionResult Index()
        {
            var Oppslagene = myRepository.visAlleOppslag();

            return View("Index", Oppslagene);
        }
        // GET: /Oppslag/
        
        public ActionResult IndexDojo()
        {
            //var Oppslagene = myRepository.visAlleOppslag().ToList();
            //List<Oppslag> Oppslagene = new List<Oppslag>(myRepository.visAlleOppslag());
            //ViewData["Oppslagene"] = Oppslagene;

            return View();
        }
        
        public ActionResult IndexTest()
        {
            //var Oppslagene = myRepository.visAlleOppslag().ToList();
            //List<Oppslag> Oppslagene = new List<Oppslag>(myRepository.visAlleOppslag());
            //ViewData["Oppslagene"] = Oppslagene;

            return View(new List<Oppslag>());
        }         
        
        public ActionResult IndexCreate()
        {
            //var Oppslagene = myRepository.visAlleOppslag().ToList();
            List<Oppslag> Oppslagene = new List<Oppslag>(myRepository.visAlleOppslag());
            ViewData["Oppslagene"] = Oppslagene;

            return View("IndexCreate");
        }
        
        [OutputCache(Duration=15, VaryByParam="None")]
        public ActionResult IndexCached()
        {
            var Oppslagene = myRepository.visAlleOppslag().ToList();

            return View("Index", Oppslagene);
        }

        public JsonResult Oppslagene()
        {
            var Oppslagene = myRepository.visAlleOppslag().ToList();

            //return View("Index", Oppslagene);
            return Json(Oppslagene, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JS_Kommentarer(int id)
        {
            var Oppslag = myRepository.hentOppslag(id);
            var kommentarer = Oppslag.kommentarer;

            return Json(kommentarer, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Oppslag/Details/5

        public ActionResult Details(int id)
        {
            
            var Oppslag = myRepository.hentOppslag(id);
            if (Oppslag == null) return View("NotFound");
            return View(new OppslagDetailsViewModel(Oppslag));
        }

       //
       // GET: /Oppslag/Create
       // [Authorize(Roles="admin", Users="kc")]
        
        [Authorize]
        public ActionResult Create()
        {
            return View(new OppslagFormViewModel(new Oppslag(), User.Identity.Name));
        } 

        //
        // POST: /Oppslag/Create
        
        [Authorize]
        [HttpPost]
        //public ActionResult Create(FormCollection collection)
        //public async Task<ActionResult> Create(Oppslag nyttOppslag)
        public ActionResult Create(Oppslag nyttOppslag)
        {

            String[] whitelist = { "tittel", "ingress", "OppslagTekst", "kategori" };
            if (nyttOppslag == null) nyttOppslag = new Oppslag();
            TryUpdateModel(nyttOppslag, "Oppslag", whitelist);

            if(ModelState.IsValid)
            {
                myRepository.leggTilOppslag(nyttOppslag, User.Identity);
                //List<Oppslag> Oppslagene = new List<Oppslag>(myRepository.visAlleOppslag());
                //ViewData["Oppslagene"] = Oppslagene;
                return RedirectToAction("Details", new { oppslagId = nyttOppslag.oppslagId });
                //return View(new OppslagFormViewModel(nyttOppslag));
            }
            return View(new OppslagFormViewModel(nyttOppslag));
            
        }
        
        //
        // GET: /Oppslag/Edit/5
        //[Authorize]
        public ActionResult Edit(int id) {

                  
            Oppslag Oppslag = myRepository.hentOppslag(id);
            if (Oppslag == null) return View("NotFound");
            // sjekk om eier
            //MembershipUser user = Membership.GetUser(Oppslag.brukerID);
            //TBD
            //if (myRepository.GetUserName(Oppslag.brukerID) != User.Identity.Name) return View("FeilEier");
            //SelectList brukere = new SelectList(myRepository.visAlleBrukere(), "brukerID", "brukernavn", Oppslag.brukerID);
            //ViewData["Brukere"] = brukere;
            return View(new OppslagFormViewModel(Oppslag));
        }


        //
        // POST: /Oppslag/Edit/5
        [Authorize]
        [ActionName("Edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        //[ValidateAntiForgeryToken]
        public ActionResult EditviaPost(int id, FormCollection collection)
        //
        {
          
            var Oppslag = myRepository.hentOppslag(id);
            // sjekk eier
            //MembershipUser user = Membership.GetUser(Oppslag.brukerID);
            //if (user.UserName != User.Identity.Name) return View("FeilEier");
            if (myRepository.GetUserName(Oppslag.eier) != User.Identity.Name) return View("FeilEier");
            
                // TODO: Add update logic here

                // whitelist
                String[] whitelist = { "tittel", "ingress", "Oppslagtekst", "kategoriID" };

                if (TryUpdateModel(Oppslag, "Oppslag", whitelist))
                {
                    myRepository.oppdaterOppslag(Oppslag);
                    return RedirectToAction("Details", new { oppslagId = Oppslag.oppslagId });
                }

                //SelectList brukere = new SelectList(myRepository.visAlleBrukere(), "brukerID", "brukernavn", Oppslag.brukerID);
                //ViewData["Brukere"] = brukere;
                return View(new OppslagFormViewModel(Oppslag));
            
        }

        //
        // GET: /Oppslag/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            var Oppslag = myRepository.hentOppslag(id);
            // sjekk eier
            //MembershipUser user = Membership.GetUser(Oppslag.brukerID);
            //if (user.UserName != User.Identity.Name) return View("FeilEier");

            //TBD if (myRepository.GetUserName(Oppslag.brukerID) != User.Identity.Name) return View("FeilEier");
            return View(Oppslag);
        }

        //
        // POST: /Oppslag/Delete/5
       
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
           
                // TODO: Add delete logic here
                var Oppslag = myRepository.hentOppslag(id);
                // sjekk eier
                //MembershipUser user = Membership.GetUser(Oppslag.brukerID);
                //if (user.UserName != User.Identity.Name) return View("FeilEier");
                // TBD if (myRepository.GetUserName(Oppslag.brukerID) != User.Identity.Name) return View("FeilEier");
                myRepository.slettOppslag(Oppslag);
                return RedirectToAction("Index");
            
        }
        
        [HttpGet]
        public ActionResult Test(String message)
        {

            return View();

        }
        [HttpGet]
        public ActionResult TestJson(String message)
        {

            return View();

        }

        public JsonResult TestJsonPost()
        {

            //ViewData["melding"] = message;
            var Oppslagene = myRepository.visAlleOppslag();
            List<dynamic> olist = new List<dynamic>();
            foreach (Oppslag o in Oppslagene)
            {
                var myOp = new {
                    tittel = o.tittel,
                    ingress = o.ingress
                };
                
                olist.Add(myOp);
                
            }
           
            return Json(olist, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        
        public ActionResult TestPost(String message)
        {

            ViewData["melding"] = message;
            return PartialView();

        }

        [HttpPost]
        public ActionResult Kommentarer(int id)
        {

            var Oppslag = myRepository.hentOppslag(id);
            if (Oppslag == null) return View("NotFound");
            //return View("CommentsView", new OppslagDetailsViewModel(Oppslag));
            return Content("Hello !");
        }


    }
}

