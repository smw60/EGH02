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
    public partial class EGHMAPController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.EGHLayout = "MAP";
            RGEContext db = null;
            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            db = new RGEContext();
            view = View("Index", db);
            return view;
        }

        [HttpPost]
        public ActionResult IndexCreate(EGH01.Models.EGHMAP.MapPointView mp)
        {
            ViewBag.EGHLayout = "MAP";
            RGEContext db = null;

            ActionResult view = View("Index");
            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            view = View("Index", db);
            return view;
            }
        




    }
    }
