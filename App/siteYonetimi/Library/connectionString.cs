using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siteYonetimi.Library
{
    public class connectionString
    {
        //sql veri tabanına bağlantı için connection string tanımlaması yapıyoruz
        public static string sqlConnect()
        {
            return @"Data Source=DESKTOP-Q59NC1B\SQLOMER;Initial Catalog=site;Integrated Security=True";
           
        }
    }
}
