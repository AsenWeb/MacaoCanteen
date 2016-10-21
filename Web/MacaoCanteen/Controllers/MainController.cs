using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MacaoCanteen.Models;

namespace MacaoCanteen.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Index/

        public ActionResult Index()
        {

            MacaoCanteenEntities Mc = new MacaoCanteenEntities();
            
            
            return View();
        }

    }
}
