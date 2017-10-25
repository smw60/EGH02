using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using EGH01.Models.EGHRGE;
using EGH01DB;
using EGH01DB.Primitives;
using EGH01DB.Types;
namespace EGH01.Controllers
{
    public partial class EGHRGEController : Controller
    {
        public ActionResult MapPoint()
        {
            RGEContext db = null;
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            db = new RGEContext();
            view = View("MapPoint", db);
            return view;
        }

        [HttpPost]
        public ActionResult MapPointCreate(EGH01.Models.EGHRGE.MapPointView mp)
        {
            RGEContext db = null;

            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";

                return view;
            }






        }
    }
