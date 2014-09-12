using System;
using Identity_test_1.Models;
namespace Identity_test_1.Controllers
{
    public interface IOppslagController
    {
        System.Web.Mvc.ActionResult Create();
        System.Web.Mvc.ActionResult Create(Oppslag op);
        System.Web.Mvc.ActionResult Delete(int id);
        System.Web.Mvc.ActionResult Delete(int id, System.Web.Mvc.FormCollection collection);
        System.Web.Mvc.ActionResult Details(int id);
        System.Web.Mvc.ActionResult Edit(int id);
        System.Web.Mvc.ActionResult EditviaPost(int id, System.Web.Mvc.FormCollection collection);
        System.Web.Mvc.ActionResult Index();
        System.Web.Mvc.ActionResult Kommentarer(int id);
        System.Web.Mvc.JsonResult Oppslagene();
    }
}
