using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace EGH01DB
{
  [Serializable]
   public  class EGHDBException:Exception
    {
 
       public string ehgmessage { get; private set;}
       SqlConnection con = DB.Connect();

       public EGHDBException(string message)
       {
           this.ehgmessage = message;
       }
       
    }
}
