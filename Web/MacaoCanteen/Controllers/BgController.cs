using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MacaoCanteen.Models;
namespace MacaoCanteen.Controllers
{
    public class BgController : Controller
    {
        //
        // GET: /Bg/

        public ActionResult Index()
        {
           
            MacaoCanteenEntities Me = new MacaoCanteenEntities();
            Test Tst=Me.Test.ToList()[0];
            return View(Tst);
        }

    }
}
