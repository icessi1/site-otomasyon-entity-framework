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
    //daire parametreleri ile ilgili tüm sorgulamalarımızı bu class içinde tanımlıyoruz Formun code kısmında da burdaki sorgularımızı çalıştırıyourz
    public class qDaire
    {

        //gridde kullanacağımız başlıklara veri yüklemek için burda bir class olarak bu alanları ve özelliklerini tanımlıyoruz
        public class _Daire
        {
            public int Id { get; set; }
            public string siteAdi { get; set; }
            public string blokAdi { get; set; }
            public string daireNo { get; set; }
            public int blokId { get; set; }
        }

        public List<_Daire> listDaire(int postId)
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    return (from d in db.Daires
                            where d.blokId == postId
                            select new _Daire // tanımlamış olduğumuz classa alanları aktarıyoruz
                            {
                                Id = d.Id,
                                siteAdi = db.Sites.Join(db.Bloks,s=>s.Id,b=>b.siteId, (s,b)=>new {blokId=b.Id,siteAdi=s.siteName}).Where(a=>a.blokId==d.blokId).Select(a=>a.siteAdi).FirstOrDefault(),  //linq farklı bir yazım türü
                                blokAdi = db.Bloks.Where(a => a.Id == d.blokId).Select(a => a.blokAdi).FirstOrDefault(),
                                daireNo=d.daireNo,
                                blokId = d.blokId
                            }
                            ).OrderBy(a => a.siteAdi).ThenBy(a => a.blokAdi).ThenBy(a=>a.daireNo).ToList(); //Liste olarak verileri sıralayarak geri döndürüyoruz
                }
            }
        }
        public void InsertOrUpdate(daire g, out string outMessage) //formdan bize gelecek olan bilgileri tanımlıyoruz daire tipinde g verisi gelecek ve dönüş mesajı tanımlaması yapıyoruz
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
                        var result = (from d in db.Daires where d.Id == g.Id select d).FirstOrDefault();

                        //eğer formdan gelen bilgiler var olan bir kayda aitse kaydı güncelliyoruz
                        if (result != null)
                        {
                            //formdan gelen bilgileri veritabanındaki alanlara atıyoruz
                            result.blokId = g.blokId;
                            result.daireNo = g.daireNo;

                            db.SaveChanges(); //bilgileri veritabanına kayıt ediyoruz
                            outMessage = "Bilgiler Güncelleştirildi."; //forma geri göndereceğimiz mesaj
                        }
                        else //gelen bilgi yeni bir kayıt ise
                        {
                            db.Daires.Add(g);
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
        public _Daire getDaire(int postId) //formdan gelen parametre Id'sine göre site bilgilerini gönderiyoruz 
        {
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                using (var db = new SQLDBModel(connection, true))
                {
                    return (from d in db.Daires
                            where d.Id == postId  //formdan gelen id veritabanında varmı kontrol ediyoruz
                            select new _Daire
                            {
                                siteAdi = db.Sites.Join(db.Bloks, s => s.Id, b => b.siteId, (s, b) => new { blokId = b.Id, siteAdi = s.siteName }).Where(a => a.blokId == d.blokId).Select(a => a.siteAdi).FirstOrDefault(),  //linq farklı bir yazım türü
                                blokAdi = db.Bloks.Where(a => a.Id == d.blokId).Select(a => a.blokAdi).FirstOrDefault(),
                                daireNo = d.daireNo,
                                blokId = d.blokId
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
                        var result = (from d in db.Daires where d.Id == postId select d).FirstOrDefault(); //gelen değeri veritabanından kontrol ediyoruz
                        if (result != null) //gelen değer veritabanında varsa
                        {
                            db.Daires.Remove(result); //gelen değere göre bulduğumuz kaydı veritabanından siliyoruz
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
