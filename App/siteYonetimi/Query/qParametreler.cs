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
    public class qParametreler
    {

        //gridde kullanacağımız başlıklara veri yüklemek için burda bir class olarak bu alanları ve özelliklerini tanımlıyoruz
        public class _Parametre
        {
            public int Id { get; set; }
            public string aciklama { get; set; }
            public string parametreTuru { get; set; }
            public string anaParametre { get; set; }
            public int parentId { get; set; }
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
                            select new _Parametre // tanımlamış olduğumuz classa alanları aktarıyoruz
                            {
                                Id = p.Id,
                                aciklama=p.aciklama,
                                parametreTuru= p.parentId==0 ? "Ana Parametre" : "Alt Parametre", //if değişik bir yazım şekli parantId varsa ana parametre yoksa alt parametre
                                anaParametre= p.parentId==0 ? "" : (from pr in db.Parametrelers where pr.Id == p.parentId select pr.aciklama).FirstOrDefault(),
                                parentId = p.parentId
                            }
                            ).OrderBy(a => a.parentId).ThenBy(a=>a.anaParametre).ThenBy(a=>a.aciklama).ToList(); //Liste olarak verileri sıralayarak geri döndürüyoruz
                }
            }
        }
        private string getAnaParametre(int postId) //alt parametrenin ana parametre adını bulmak için
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    return (from p in db.Parametrelers where p.Id == postId select p.aciklama).FirstOrDefault(); //ilk değerin açıklamasını döndürüyoruz
                }
            }
        }
        public List<_Parametre> listAnaParametreler()
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    return (from p in db.Parametrelers
                            where p.parentId==0
                            select new _Parametre // tanımlamış olduğumuz classa alanları aktarıyoruz
                            {
                                Id = p.Id,
                                aciklama = p.aciklama
                            }
                            ).OrderBy(a => a.aciklama).ToList(); //Liste olarak verileri açıklamaya göre sıralayarak geri döndürüyoruz
                }
            }
        }

        public void InsertOrUpdate(parametreler g, out string outMessage) //formdan bize gelecek olan bilgileri tanımlıyoruz parametre tipinde g verisi gelecek ve dönüş mesajı tanımlaması yapıyoruz
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
                        var result = (from s in db.Parametrelers where s.Id == g.Id select s).FirstOrDefault();

                        //eğer formdan gelen bilgiler var olan bir kayda aitse kaydı güncelliyoruz
                        if (result != null)
                        {
                            //formdan gelen bilgileri veritabanındaki alanlara atıyoruz
                            result.aciklama = g.aciklama;
                            result.parentId = g.parentId;

                            db.SaveChanges(); //bilgileri veritabanına kayıt ediyoruz
                            outMessage = "Bilgiler Güncelleştirildi."; //forma geri göndereceğimiz mesaj
                        }
                        else //gelen bilgi yeni bir kayıt ise
                        {
                            db.Parametrelers.Add(g);
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
        public _Parametre getParemetre(int postId) //formdan gelen parametre Id'sine göre site bilgilerini gönderiyoruz 
        {
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                using (var db = new SQLDBModel(connection, true))
                {
                    return (from p in db.Parametrelers
                            where p.Id == postId  //formdan gelen id veritabanında varmı kontrol ediyoruz
                            select new _Parametre
                            {
                                Id = p.Id,
                                aciklama = p.aciklama,
                                parentId=p.parentId
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
                        var result = (from p in db.Parametrelers where p.Id == postId select p).FirstOrDefault(); //gelen değeri veritabanından kontrol ediyoruz
                        if (result != null) //gelen değer veritabanında varsa
                        {
                            if (result.parentId == 0) //eğer ana parametre ise ana parametreyle birlikte alt parametreleride siliyoruz
                            {
                                var r = (from p in db.Parametrelers where p.parentId == postId select p).FirstOrDefault();
                                if (r!=null)
                                {
                                    db.Parametrelers.Remove(r);
                                    db.SaveChanges();
                                }
                            }
                            db.Parametrelers.Remove(result); //gelen değere göre bulduğumuz kaydı veritabanından siliyoruz
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
