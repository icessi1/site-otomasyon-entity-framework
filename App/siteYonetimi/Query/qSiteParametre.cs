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
   //site parametreleri ile ilgili tüm sorgulamalarımızı bu class içinde tanımlıyoruz Formun code kısmında da burdaki sorgularımızı çalıştırıyourz
    public class qSiteParametre
    {

        //gridde kullanacağımız başlıklara veri yüklemek için burda bir class olarak bu alanları ve özelliklerini tanımlıyoruz
        public class _SiteParametre
        {
            public int Id { get; set; }
            public string siteAdi { get; set; }
            public string parametreAciklama { get; set; }
            public string kisi { get; set; }
            public string gsmNo { get; set; }
            public int siteId { get; set; }
            public int parametreId { get; set; }
            public int kisiId { get; set; }
        }
        public class _Parametre
        {
            public int Id { get; set; }
            public string aciklama { get; set; }
        }
        public class _Kisi
        {
            public int Id { get; set; }
            public string adiSoyadi { get; set; }
        }
        public List<_SiteParametre> listSitetParametreler(int postId)
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    return (from p in db.SiteParametres
                            where p.siteId==postId
                            select new _SiteParametre // tanımlamış olduğumuz classa alanları aktarıyoruz
                            {
                                Id = p.Id,
                                siteAdi = db.Sites.Where(a => a.Id == p.siteId).Select(a => a.siteName).FirstOrDefault(), //linq farklı bir yazım türü
                                parametreAciklama = db.Parametrelers.Where(a => a.Id == p.parametreId).Select(a => a.aciklama).FirstOrDefault(),
                                kisi = db.Kisilers.Where(a => a.Id == p.kisiId).Select(a => a.adi + " " + a.soyadi).FirstOrDefault(),
                                gsmNo = db.Kisilers.Where(a => a.Id == p.kisiId).Select(a => a.gsmNo).FirstOrDefault(),
                                siteId =p.siteId,
                                parametreId=p.parametreId,
                                kisiId=p.kisiId
                            }
                            ).ToList(); //Liste olarak verileri sıralayarak geri döndürüyoruz
                }
            }
        }
        public List<_Parametre> listParametreler()
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    return (from p in db.Parametrelers
                            where p.parentId == 1 // site parametrelerini çağırıyoruz
                            select new _Parametre // tanımlamış olduğumuz classa alanları aktarıyoruz
                            {
                                Id = p.Id,
                                aciklama = p.aciklama
                            }
                            ).OrderBy(a => a.aciklama).ToList(); //Liste olarak verileri açıklamaya göre sıralayarak geri döndürüyoruz
                }
            }
        }
        public List<_Kisi> listKisiler()
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    return (from k in db.Kisilers
                            select new _Kisi // tanımlamış olduğumuz classa alanları aktarıyoruz
                            {
                                Id = k.Id,
                                adiSoyadi = k.adi + " " + k.soyadi
                            }
                            ).OrderBy(a => a.adiSoyadi).ToList(); //Liste olarak verileri açıklamaya göre sıralayarak geri döndürüyoruz
                }
            }
        }
        public void InsertOrUpdate(siteParametre g, out string outMessage) //formdan bize gelecek olan bilgileri tanımlıyoruz parametre tipinde g verisi gelecek ve dönüş mesajı tanımlaması yapıyoruz
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
                        var result = (from s in db.SiteParametres where s.Id == g.Id select s).FirstOrDefault();

                        //eğer formdan gelen bilgiler var olan bir kayda aitse kaydı güncelliyoruz
                        if (result != null)
                        {
                            //formdan gelen bilgileri veritabanındaki alanlara atıyoruz
                            result.siteId = g.siteId;
                            result.parametreId = g.parametreId;
                            result.kisiId = g.kisiId;

                            db.SaveChanges(); //bilgileri veritabanına kayıt ediyoruz
                            outMessage = "Bilgiler Güncelleştirildi."; //forma geri göndereceğimiz mesaj
                        }
                        else //gelen bilgi yeni bir kayıt ise
                        {
                            db.SiteParametres.Add(g);
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
        public _SiteParametre getParemetre(int postId) //formdan gelen parametre Id'sine göre site bilgilerini gönderiyoruz 
        {
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                using (var db = new SQLDBModel(connection, true))
                {
                    return (from p in db.SiteParametres
                            where p.Id == postId  //formdan gelen id veritabanında varmı kontrol ediyoruz
                            select new _SiteParametre
                            {
                                Id = p.Id,
                                siteId = p.siteId,
                                parametreId = p.parametreId,
                                kisiId=p.kisiId
                            }).FirstOrDefault(); //gelen değere göre veritabanındaki ilk kaydı geri gönderiyoruz
                }
            }
        }
        public void delete(int postId, out string outMessage) //formdan gelen Id'ye göre veriyi veritabanından siliyoruz
        {
            try
            {
                outMessage = ""; //geri gönderilecek mesajı sıfırlıyoruz
                using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (var db = new SQLDBModel(connection, true))
                    {
                        var result = (from p in db.SiteParametres where p.Id == postId select p).FirstOrDefault(); //gelen değeri veritabanından kontrol ediyoruz
                        if (result != null) //gelen değer veritabanında varsa
                        {
                            db.SiteParametres.Remove(result); //gelen değere göre bulduğumuz kaydı veritabanından siliyoruz
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
