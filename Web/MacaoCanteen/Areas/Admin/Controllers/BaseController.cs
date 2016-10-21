using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MacaoCanteen.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Admin/Base/

        public BaseController() {
            
            string kc = "";
            RedirectToAction("Index","Main");
        }

        public void GetUser() { 
        
        
        }

    }
}
