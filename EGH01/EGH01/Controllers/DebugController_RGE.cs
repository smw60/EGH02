using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using EGH01DB.Primitives;
using EGH01DB.Types;
using EGH01DB.Points;
using EGH01DB.Objects;
using EGH01DB;

namespace EGH01.Controllers
{
    public partial  class DebugController:Controller
    {


        public ActionResult RGECalc()
        {

            {
                RGEContext db = new RGEContext();
                RGEContext.ECOForecast f = new RGEContext.ECOForecast();
                int k = 1;
            }

            //{
            //    Coordinates c1 = new Coordinates(53.663388f, 27.143777f);
            //    Coordinates c2 = new Coordinates(53.663229f, 27.143831f);
            //    Coordinates c3 = new Coordinates(53.663267f, 27.143574f);
            //    Coordinates c4 = new Coordinates(53.663394f, 27.143552f);

            //}

            //{
            //    List<RiskObject> o = new List<RiskObject>();

            //    if (RiskObjectsList.GetListRiskObjectByLike(db, "Брест", ref o))
            //    {
            //        int k = 1;

            //    }

            //}





            return View();
        }




    }
}