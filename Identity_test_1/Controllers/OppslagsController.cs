using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Identity_test_1.Models;

namespace Identity_test_1.Controllers
{
    public class OppslagsController : ApiController
    {
        private IOppslagRepository myRepository;
        
        public OppslagsController()
        {
            myRepository = new OppslagRepository();
        }
        
        public OppslagsController(IOppslagRepository repository)
        {
            myRepository = repository;
        }      
        //
        // GET api/<controller>
        // GET api/Oppslags

        public IEnumerable<Oppslag> Get()
        {
            var Oppslagene = myRepository.visAlleOppslag();

            return  Oppslagene;
        }

        /*
        // GET api/Contacts
        public IEnumerable<Contact> GetContacts()
        {
            return myRepository.GetAll();
        }
        */
        // GET api/Oppslags/5
        public Oppslag  GetOppslag(int id)
        {
            Oppslag o = myRepository.hentOppslag(id);
            if (o == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return o;
        }

        // PUT api/Oppslags/5
        public HttpResponseMessage PutOppslag(int id, Oppslag o)
        {
            if (ModelState.IsValid && id == o.oppslagId)
            {
                myRepository.oppdaterOppslag(o);
                /*
                if (!ok)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else 
                */ 
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Contacts
        public HttpResponseMessage PostOppslag(Oppslag o)
        {
            if (ModelState.IsValid )
            {
                myRepository.leggTilOppslag(o, "kc");
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, o);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { oppslagId = o.oppslagId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Oppslags/5
        public HttpResponseMessage DeleteOppslag(int id)
        {
            Oppslag o = myRepository.hentOppslag(id);
            myRepository.slettOppslag(o);
            return Request.CreateResponse(HttpStatusCode.NoContent);



            //else return Request.CreateResponse(HttpStatusCode.NotFound);

        }

    }
}