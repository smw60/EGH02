using System;
using System.Web.Mvc;
using EGH01DB;
using EGH01DB.Primitives;
using EGH01.Models.EGHORT;


namespace EGH01.Controllers
{
    public partial class EGHORTController : Controller
    {
        // GET: EGHORTController_Report
        public ActionResult Report()
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.Report";
            ActionResult view = View("Index");
            ViewBag.stage = "Т";

            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                ViewBag.msg = "Соединение с базой данных установлено";
                view = View("Report", db);


                if (menuitem.Equals("Report.Watch"))
                {

                    string id = this.HttpContext.Request.Params["id"];
                    string comment = this.HttpContext.Request.Params["comment"];
                    if (id != null)
                    {
                        int c = 0;
                        if (int.TryParse(id, out c))
                        {
                            EGH01DB.Primitives.Report report = new EGH01DB.Primitives.Report();
                            if (EGH01DB.Primitives.Report.GetById(db, c, out report, out comment))
                            {
                                EGH01.Models.EGHRGE.ReportView rv = new Models.EGHRGE.ReportView();
                                rv.rep = report.ToHTML();
                                ViewBag.msg = rv.rep;
                                view = View("ReportWatch", report);
                            }
                        }
                    }

                }

                else if (menuitem.Equals("Report.Delete"))
                {

                    string id = this.HttpContext.Request.Params["id"];
                    string comment = this.HttpContext.Request.Params["comment"];
                    if (id != null)
                    {
                        int c = 0;
                        if (int.TryParse(id, out c))
                        {
                            EGH01DB.Primitives.Report report = new EGH01DB.Primitives.Report();
                            if (EGH01DB.Primitives.Report.GetById(db, c, out report, out comment))
                            {
                                view = View("ReportDelete", report);
                            }
                        }
                    }

                }


                else if (menuitem.Equals("Report.SaveComment"))
                {
                    string id = this.HttpContext.Request.Params["id"];
                    string comment;

                    if (id != null)
                    {
                        int c = 0;
                        if (int.TryParse(id, out c))
                        {
                            Report report = new Report();
                            if (EGH01DB.Primitives.Report.GetById(db, c, out report, out comment))
                            {
                                comment = this.HttpContext.Request.Params["comment"];
                                EGH01DB.Primitives.Report.UpdateCommentById(db, c, comment);
                                view = View("Report", db);
                            }
                        }
                    }
                }
               
            }

            catch (RGEContext.Exception e)
            {
                ViewBag.msg = e.message;
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
            }

            return view;
        }


        [HttpPost]
        public ActionResult ReportDelete(int id)
        {
            ORTContext db = null;
            ViewBag.EGHLayout = "ORT.Report";
            ActionResult view = View("Index");
            ViewBag.stage = "Т";

            string menuitem = this.HttpContext.Request.Params["menuitem"] ?? "Empty";
            try
            {
                db = new ORTContext();
                if (menuitem.Equals("Report.Delete.Delete"))
                {
                    if (EGH01DB.Primitives.Report.DeleteById(db, id))
                        view = View("Report", db);
                }
                else if (menuitem.Equals("Report.Delete.Cancel"))
                    view = View("Report", db);

            }
            catch (RGEContext.Exception e)
            {
                ViewBag.msg = e.message;
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
            }

            return view;
        }


    }
}
