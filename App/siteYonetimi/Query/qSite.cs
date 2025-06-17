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
    //site ile ilgili tüm sorgulamalarımızı bu class içinde tanımlıyoruz Formun code kısmında da burdaki sorgularımızı çalıştırıyourz
    public class qSite
    {
      
        //gridde kullanacağımız başlıklara veri yüklemek için burda bir class olarak bu alanları ve özelliklerini tanımlıyoruz
        public class _Site
        {
            public int Id { get; set; }
            public string siteAdi { get; set; }
            public int IlId { get; set; }
            public string ilAdi { get; set; }
            public string adres { get; set; }
        }

        //form sayfasında yeni site tanımlarken il seçimi yapılabilmesi için iller tablosundaki verileri atayacağımız classı tanımlıyoruz
        public class _iller
        {
            public int Id { get; set; }
            public string ilAdi { get; set; }
        }

        public List<_Site> listSiteler()
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    return (from s in db.Sites
                            join i in db.İllers on s.ilId equals i.Id // sitedeki ilId alanını iller tablosuna bağlıyoruz
                            select new _Site // tanımlamış olduğumuz _Site classına alanları aktarıyoruz
                            {
                                Id = s.Id,
                                siteAdi=s.siteName,
                                adres=s.adres,
                                IlId=s.ilId,
                                ilAdi=i.ilAdi
                            }
                            ).OrderBy(a => a.siteAdi).ToList(); //Liste olarak verileri siteadına göre sıralayarak geri döndürüyoruz
                }
            }
        }
        public List<_iller> listIller()
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    return (from i in db.İllers
                            select new _iller // tanımlamış olduğumuz _iller classına alanları aktarıyoruz
                            {
                                Id = i.Id,
                                ilAdi = i.ilAdi
                            }
                            ).OrderBy(a => a.ilAdi).ToList(); //Liste olarak verileri iladına göre sıralayarak geri döndürüyoruz
                }
            }
        }

        public void InsertOrUpdate(site g, out string outMessage) //formdan bize gelecek olan bilgileri tanımlıyoruz site tipinde g verisi gelecek ve dönüş mesajı tanımlaması yapıyoruz
        {
            try //sistem bir hata verirse kontrol koyuyoruz
            {
                using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (var db = new SQLDBModel(connection, true))
                    {
                        //formdan gelen Id alanı yeni bir kayıt mı yoksa var olan bir kayıt mı? yeni kayıtlar için 0 gönderiyoruz
                        //yeni kayıt 0 geldiğinde veritabanında kontrol edecek 0 olarak bir Id bulamayacağı için yeni kayıt olarak kabul edecek
                        var result = (from s in db.Sites where s.Id == g.Id select s).FirstOrDefault(); 

                        //eğer formdan gelen bilgiler var olan bir kayda aitse kaydı güncelliyoruz
                        if (result != null)
                        {
                            //formdan gelen bilgileri veritabanındaki alanlara atıyoruz
                            result.siteName = g.siteName;
                            result.adres = g.adres;
                            result.ilId = g.ilId;

                            db.SaveChanges(); //bilgileri veritabanına kayıt ediyoruz
                            outMessage = "Bilgiler Güncelleştirildi."; //forma geri göndereceğimiz mesaj
                        }
                        else //gelen bilgi yeni bir kayıt ise
                        {
                            db.Sites.Add(g); 
                            db.SaveChanges(); //bilgileri veritabanına kayıt ediyoruz
                            outMessage = "Yeni Kayıt Eklendi."; //forma geri göndereceğimiz mesaj
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                //hata verirse hasta mesajını forma geri gönderiyoruz
                outMessage = ex.Message;
                return;
            }
        }
        public _Site getSite(int postId) //formdan gelen site Id'sine göre site bilgilerini gönderiyoruz 
        {
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                using (var db = new SQLDBModel(connection, true))
                {
                    return (from s in db.Sites
                            where s.Id== postId  //formdan gelen id veritabanında varmı kontrol ediyoruz
                            select new _Site
                            {
                                Id = s.Id,
                                siteAdi=s.siteName,
                                adres=s.adres,
                                IlId=s.ilId
                            }).FirstOrDefault(); //gelen değere göre veritabanındaki ilk kaydı geri gönderiyoruz
                }
            }
        }
        public void delete(int postId, out string outMessage ) //formdan gelen Id'ye göre veriyi veritabanından siliyoruz
        {
            try
            {
                outMessage = ""; //geri gönderilecek mesajı sıfırlıyoruz
                using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (var db = new SQLDBModel(connection, true))
                    {
                        var result = (from s in db.Sites where s.Id == postId select s).FirstOrDefault(); //gelen değeri veritabanından kontrol ediyoruz
                        if (result != null) //gelen değer veritabanında varsa
                        {
                            db.Sites.Remove(result); //gelen değere göre bulduğumuz kaydı veritabanından siliyoruz
                            db.SaveChanges(); //son durumu kayıt ediyoruz
                            outMessage = "Kayıt Silindi."; //geri döndürdüğümüz mesaj
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //hata verirse hasta mesajını forma geri gönderiyoruz
                outMessage = ex.Message;
                return;
            }
            
        }
    }
}
