using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EGH01DB.Primitives;
using System.Web.Mvc;

namespace EGH01.Controllers
{
    public partial class DebugController: Controller 
    {

        
        public ActionResult Coord()
        {
            Coordinates CC = new Coordinates();
            float latitude = 0.0f, lngitude = 0.0f;
            int latd = 0, latm = 0, lngd = 0, lngm = 0;
            float lats = 0.0f, lngs = 0.0f;

            if (this.HttpContext.Request["convert"] != null && this.HttpContext.Request["convert"].Equals("todms"))
            {
                if (HttpContext.Request["latitude"] != null &&   Helper.FloatTryParse(HttpContext.Request["latitude"], out latitude))
                {
                    if (HttpContext.Request["lngitude"] != null && Helper.FloatTryParse(HttpContext.Request["lngitude"], out lngitude))
                    {
                     CC = new Coordinates(latitude, lngitude);
                    }
                }
            }

            if (this.HttpContext.Request["convert"] != null && this.HttpContext.Request["convert"].Equals("tod"))
            {


                bool rc1 = (HttpContext.Request["latd"] != null && int.TryParse(HttpContext.Request["latd"], out latd));
                bool rc2 = (HttpContext.Request["latm"] != null && int.TryParse(HttpContext.Request["latm"], out latm));
                bool rc3 = (HttpContext.Request["lats"] != null && Helper.FloatTryParse(HttpContext.Request["lats"], out lats));

                bool rc4 = (HttpContext.Request["lngd"] != null && int.TryParse(HttpContext.Request["lngd"], out lngd));
                bool rc5 = (HttpContext.Request["lngm"] != null && int.TryParse(HttpContext.Request["lngm"], out lngm));
                bool rc6 = (HttpContext.Request["lngs"] != null && Helper.FloatTryParse(HttpContext.Request["lngs"], out lngs));

                if (rc1 && rc2 && rc3 && rc4 && rc5 && rc6)
                {
                    CC = new Coordinates(latd, latm, lats, lngd, lngm, lngs);
                } 

            }

            
            return View(CC);
        }


    }
}

//                <input class="boxdata-input-10" type="number" name="latitude" value="@CC.latitude.ToString("0,000000")" step="0.000001" />
//            </p>
//            <p>
//                <label class="boxdata-input-10">Долгота</label>
//                <input class="boxdata-input-10" type="number" name="lngitude" value="@CC.lngitude.ToString("0,000000")" max="180" min="-180" step="0.000001" />
//            </p>
            
//            <input type="submit"  value="todms" name="convert" />

//            <fieldset>
//                <legend>Широта</legend>
//                <p>
//                    <label class="boxdata-input-10" >Градусы </label> 
//                    <input class="boxdata-input-10" type="number" name="latd" value="@CC.lat.d.ToString()" max="90" min="-90" step="1 " />
//                </p> 
//                <p>
//                    <label class="boxdata-input-10">Минуты </label>
//                    <input class="boxdata-input-10" type="number" name="latm" value="@CC.lat.m.ToString()" max="0" min="60" step="1 " />
//                </p> 
//                <p>
//                    <label class="boxdata-input-10">Секунды </label>
//                    <input class="boxdata-input-10" type="number" name="lats" value="@CC.lat.s.ToString("0.000")" max="0" min="60" step="0.001" />
//                </p> 
//            </fieldset>
            
           
//            <fieldset>
//                <legend>Долгота</legend>
//                <p>
//                    <label class="boxdata-input-10" >Градусы </label> 
//                    <input class="boxdata-input-10" type="number" name="lngd" value="@CC.lng.d.ToString()" max="90" min="-90" step="1" />
//                </p> 
//                <p>
//                    <label class="boxdata-input-10">Минуты </label>
//                    <input class="boxdata-input-10" type="number" name="lngm" value="@CC.lng.m.ToString()" max="0" min="60" step="1" />
//                </p> 
//                <p>
//                    <label class="boxdata-input-10">Секунды </label>
//                    <input class="boxdata-input-10" type="number" name="lngs" value="@CC.lng.s.ToString("0,000")" max="0" min="60" step="0.001" />
//                </p> 
//            </fieldset>
            