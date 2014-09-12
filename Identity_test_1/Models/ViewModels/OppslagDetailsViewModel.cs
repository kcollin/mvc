using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Identity_test_1.Models
{
    public class OppslagDetailsViewModel
    {

        private OppslagRepository OppslagRepository;
        public Oppslag Oppslag { get; private set; }
        public String kategori { get; private set; }
        public String eier { get; private set; }
        public List<KommentarDetails> kommentarer { get; private set; }

        public OppslagDetailsViewModel(Oppslag o)
        {
            OppslagRepository = new OppslagRepository();
            Oppslag = o;
            List<kommentar> k1 = OppslagRepository.visKommentarer(o);
            kommentarer = new List<KommentarDetails>();
            kategori = OppslagRepository.visKategori(Oppslag).navn;
            
            foreach(kommentar k in Oppslag.kommentarer) 
            {
                KommentarDetails details = new KommentarDetails(k, OppslagRepository);
                kommentarer.Add(details);

            }
            
            //eier = OppslagRepository.Userdb.GetUserName(o.brukerID);

        }


    }

    public class KommentarDetails
    {

        public kommentar kommentar;
        public String eier;

        public KommentarDetails(kommentar kommentar, OppslagRepository OppslagRepository)
        {
            this.kommentar = kommentar;
            eier = kommentar.eier.UserName;
        }

    }


}