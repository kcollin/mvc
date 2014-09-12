using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Identity_test_1.Models
{
    public class OppslagFormViewModel
    {

        private OppslagRepository OppslagRepository;
        public Oppslag Oppslag { get; private set; }
        public SelectList  Kategorier { get; private set; }
        public String eier { get; private set; }


        public OppslagFormViewModel(Oppslag o, String user)
        {
            setup(o);
            if (eier == null) eier = user;

        }
        public OppslagFormViewModel(Oppslag o)
        {
            setup(o);
            eier = OppslagRepository.Userdb.GetUserName(o.eier);
        }

        private void setup(Oppslag o)
        {
            OppslagRepository = new OppslagRepository();
            Oppslag = o;
            Kategorier = new SelectList(OppslagRepository.visAlleKategorier(), "kategoriId", "navn");
            //Kategorier = new SelectList(OppslagRepository.visAlleKategorier(), "kategoriId", "navn", o.kategori.kategoriId);
        }
    }
}