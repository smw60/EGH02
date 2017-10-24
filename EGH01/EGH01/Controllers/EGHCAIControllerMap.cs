using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using EGH01.Models.EGHCAI;
using EGH01DB;
using EGH01DB.Points;
using EGH01DB.Primitives;
using EGH01DB.Types;


namespace EGH01.Controllers
{
    public partial  class EGHCAIController:Controller
    {

        public ActionResult Map()
        {



            return View();
         }



    }
}