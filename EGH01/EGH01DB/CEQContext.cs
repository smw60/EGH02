using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using EGH01DB.Objects;


namespace EGH01DB
{
    public  partial class CEQContext : IDBContext
    {


        SqlConnection con = DB.Connect("EGHCEQ");
        Controller controller = null;
        public SqlConnection connection { get { return con; } }
        public CEQContext()
        {


        }


        List<ViewContextEntry> listviewcontext = null;
        public CEQContext(Controller controller)
        {
            this.controller = controller;
            this.listviewcontext = this.controller.Session["CEQ.viewcontext"] as List<ViewContextEntry>;
            if (this.listviewcontext == null) this.controller.Session["CEQ.viewcontext"] = this.listviewcontext = new List<ViewContextEntry>();

        }

        public class ViewContextEntry
        {
            public ViewContextEntry(string viewname, object viewcontext)
            {
                this.viewname = viewname;
                this.viewcontext = viewcontext;
            }
            public string viewname { get; set; }
            public object viewcontext { get; set; }
        }


        public bool SaveViewContext(string viewname, object viewcontext)
        {
            return SaveViewContext(new ViewContextEntry(viewname, viewcontext));
        }

        public bool SaveViewContext(ViewContextEntry viewcontextentry)
        {

            bool rc = false;
            if (rc = this.listviewcontext != null && !String.IsNullOrEmpty(viewcontextentry.viewname) && viewcontextentry.viewcontext != null)
            {
                ViewContextEntry entry = null;
                try
                {
                    entry = this.listviewcontext.First(m => m.viewname.Equals(viewcontextentry.viewname));
                    entry.viewname = viewcontextentry.viewname;
                    entry.viewcontext = viewcontextentry.viewcontext;
                }
                catch (System.InvalidOperationException)
                {
                    entry = null;
                }
                if (entry == null) this.listviewcontext.Add(viewcontextentry);
            }
            return rc;
        }

        public object GetViewContext(string viewname)
        {
            object rc = null;
            if (this.listviewcontext != null && !String.IsNullOrEmpty(viewname))
            {
                try
                {
                    ViewContextEntry entry = this.listviewcontext.First(m => m.viewname.Equals(viewname));
                    rc = entry.viewcontext;
                }
                catch (System.InvalidOperationException)
                {
                    rc = null;
                }


            }
            return rc;
        }

        public void Disconnect()
        {
            if (con != null) con.Close();
            con = null;
        }



    }

}

