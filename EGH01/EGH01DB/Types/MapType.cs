using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGH01DB.Types
{
    public class MapType
    {
        public string x { get; private set; }     // долгота
        public string y { get; private set; }     // широта

        public MapType()
        {
            this.x = this.y = "";
        }
        public MapType(string x, string y)
        {
            this.x = x;
            this.y = y;
        }

        static public bool GetGroundType(MapType point, EGH01DB.IDBContext dbcontext, out GroundType ground_type)
        {
            bool rc = false;
            ground_type = new GroundType();
            using (SqlCommand cmd = new SqlCommand("EGH.InKoefMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point1", SqlDbType.VarChar);
                    parm.Value = point.x;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@point2", SqlDbType.VarChar);
                    parm.Value = point.y;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@OutId", SqlDbType.Int);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);    
                }
                {
                    SqlParameter parm = new SqlParameter("@OutVlagoemkost", SqlDbType.Real);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@OutFiltration", SqlDbType.Real);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@OutPoristost", SqlDbType.Real);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                    cmd.ExecuteNonQuery();
                    int id = (int)cmd.Parameters["@OutId"].Value;
                    float watercapacity = (float)cmd.Parameters["@OutVlagoemkost"].Value;
                    float waterfilter = (float)cmd.Parameters["@OutFiltration"].Value;
                    float porosity = (float)cmd.Parameters["@OutPoristost"].Value;
                    
                    // porosity          пористость     >0    <1, безразмерная , доля застрявшего  в грунте нефтепродукта       
                    // holdmigration    коэфф. задержки миграции нефтепродуктов 
                    // waterfilter      коэфф. фильтрации воды
                    // diffusion       коэфф. диффузии
                    // distribution   коэфф. распределения
                    // sorption       коэфф. сорбции
                    // watercapacity   капиллярная влагоемкость (от 0 до 1)
                    // soilmoisture     влажность грунта (от 0 до 1)
                    // аveryanovfactor  коэффициент Аверьянова (от 4 до 9)
                    // permeability    водопроницаемость м/с
                    // density   плотность м/с

                    ground_type = new GroundType(-1, "Получено из карты коэффициентов грунта", porosity, 85.2f, waterfilter, 0.0f, 0.0f, 0.0f, watercapacity, 0.4f, 6.0f, 0.0f, 1750.0f);
                    rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                }
                catch (Exception e)
                {
                    rc = false;
                };
            }
            return rc;
        }
        static public bool GetDistrict(MapType point, EGH01DB.IDBContext dbcontext, out District district)
        {
            bool rc = false;
            district = new District();
            using (SqlCommand cmd = new SqlCommand("EGH.InRegionMap", dbcontext.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                {
                    SqlParameter parm = new SqlParameter("@point1", SqlDbType.VarChar);
                    parm.Value = point.x;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@point2", SqlDbType.VarChar);
                    parm.Value = point.y;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@OutId", SqlDbType.Int);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@OutDistrict", SqlDbType.NVarChar);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);
                }
                {
                    SqlParameter parm = new SqlParameter("@OutRegion", SqlDbType.NVarChar);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);
                }
                                {
                    SqlParameter parm = new SqlParameter("@exitrc", SqlDbType.Int);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(parm);
                }
                try
                {
                    //cmd.ExecuteNonQuery();
                    //int id = (int)cmd.Parameters["@OutId"].Value;
                    //string district_name = (string)cmd.Parameters["@OutDistrict"].Value;
                    //string region_name = (string)cmd.Parameters["@OutRegion"].Value;

                    string district_name = "Взято из карты регионов";
                    string region_name = "Взято из карты регионов";

                    Region region = new Region(region_name);
                    district = new District(-1, region, district_name);

                    //rc = (int)cmd.Parameters["@exitrc"].Value > 0;
                    rc = true;
                }
                catch (Exception e)
                {
                    rc = false;
                };
            }
            return rc;
        }

    }
}
