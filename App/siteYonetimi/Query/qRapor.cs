using siteYonetimi.DataModels;
using siteYonetimi.Library;
using siteYonetimi.SQLTables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siteYonetimi.Query
{
    //rapor ile ilgili tüm sorgulamalarımızı bu class içinde tanımlıyoruz Formun code kısmında da burdaki sorgularımızı çalıştırıyoruz
    public class qRapor
    {
        public int siteSayisi()
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    return (from s in db.Sites select s).Count(); //site sayısını döndürüyoruz
                }
            }
        }
        public int blokSayisi()
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    return (from b in db.Bloks select b).Count(); //blok sayısını döndürüyoruz
                }
            }
        }
        public int daireSayisi()
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    return (from d in db.Daires select d).Count(); //daire sayısını döndürüyoruz
                }
            }
        }
        public int kisiSayisi()
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    return (from k in db.Kisilers select k).Count(); //kişi sayısını döndürüyoruz
                }
            }
        }
        public class _kiraci
        {
            public string kiracimi { get; set; }
            public int sayi { get; set; }
        }
        public List<_kiraci> listKisiTur()
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    return db.Kisilers.GroupBy(x => new { x.kiracimi })
                        .Select(y => new _kiraci()
                        {
                            kiracimi = y.Key.kiracimi ? "Evet" : "Hayır",
                            sayi = y.Count()
                        }
                        ).ToList();
                }
            }
        }
        public class _medeniDurum
        {
            public string medeniDurum { get; set; }
            public int sayi { get; set; }
        }
        public List<_medeniDurum> listMedeniDurum()
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    //linq farklı bir yazım türüyle hem join hemde group by yapıyoruz
                    return db.Kisilers.Join(db.Parametrelers, k => k.medeniDurumId, p => p.Id, (k, p) => new { medeniDurum = p.aciklama }).GroupBy(x => new { x.medeniDurum })
                        .Select(y => new _medeniDurum()
                        {
                            medeniDurum = y.Key.medeniDurum,
                            sayi = y.Count()
                        }
                        ).ToList();
                }
            }
        }
        public int yas18buyuk()
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    return (from k in db.Kisilers
                            select new
                            {
                                yas = DateTime.Now.Year - k.dogumTarihi.Year
                            }).Where(a => a.yas >= 18).Count();
                }
            }
        }
    }
}
