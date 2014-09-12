using System;
using System.Collections.Generic;
namespace Identity_test_1.Models
{
    public interface IOppslagRepository
    {
        Oppslag hentOppslag(int id);
        void leggTilOppslag(Oppslag nyttOppslag, System.Security.Principal.IIdentity identity);
        void oppdaterOppslag(Oppslag o);
        void slettOppslag(Oppslag slettOppslag);
        List<kategori> visAlleKategorier();
        List<Oppslag> visAlleOppslag();
        List<kommentar> visKommentarer(Oppslag parent);
        kategori visKategori(Oppslag oppslag);
        //void leggTilKategori(kategori nyKategori);
        //void oppdaterKategori(kategori k);
        //void slettKategori(kategori slettKategori);
        String GetUserName(MyUser user);

    }
}
