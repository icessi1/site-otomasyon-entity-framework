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
    //daire kişi parametreleri ile ilgili tüm sorgulamalarımızı bu class içinde tanımlıyoruz Formun code kısmında da burdaki sorgularımızı çalıştırıyourz
    public class qDaireKisi
    {

        //gridde kullanacağımız başlıklara veri yüklemek için burda bir class olarak bu alanları ve özelliklerini tanımlıyoruz
        public class _DaireKisi
        {
            public int Id { get; set; }
            public string siteAdi { get; set; }
            public string blokAdi { get; set; }
            public string daireNo { get; set; }
            public string adiSoyadi { get; set; }
            public int kisiId { get; set; }
            public int daireId { get; set; }
            public int blokId { get; set; }
            public int siteId { get; set; }
        }

        public List<_DaireKisi> listDaireKisi(int siteId,int blokId,int daireNo)
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    var result = (from dk in db.DaireKisis
                                  join d in db.Daires on dk.daireId equals d.Id
                                  join b in db.Bloks on d.blokId equals b.Id
                                  join s in db.Sites on b.siteId equals s.Id
                                  join k in db.Kisilers on dk.kisiId equals k.Id
                                  select new _DaireKisi // tanımlamış olduğumuz classa alanları aktarıyoruz
                                  {
                                      Id = dk.Id,
                                      siteAdi = s.siteName,
                                      blokAdi = b.blokAdi,
                                      daireNo = d.daireNo,
                                      adiSoyadi = k.adi + " " + k.soyadi,
                                      kisiId = k.Id,
                                      daireId = d.Id,
                                      blokId = b.Id,
                                      siteId = s.Id
                                  }
                            );


                    if (siteId != 0) result = result.Where(a => a.siteId == siteId); //siteId seçilmişse filtreleme yapıyoruz
                    if (blokId != 0) result = result.Where(a => a.blokId == blokId); //blokId seçilmişse filtreleme yapıyoruz
                    if (daireNo != 0) result = result.Where(a => a.daireId == daireNo); //daireNo seçilmişse filtreleme yapıyoruz

                    return result.OrderBy(a => a.siteAdi).ThenBy(a => a.blokAdi).ThenBy(a => a.daireNo).ThenBy(a => a.adiSoyadi).ToList(); //veriyi sıralı sekilde geri gönderiyoruz
                    
                }
            }
        }
        public void InsertOrUpdate(daireKisi g, out string outMessage) //formdan bize gelecek olan bilgileri tanımlıyoruz daireKisi tipinde g verisi gelecek ve dönüş mesajı tanımlaması yapıyoruz
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
                        var result = (from d in db.DaireKisis where d.Id == g.Id select d).FirstOrDefault();

                        //eğer formdan gelen bilgiler var olan bir kayda aitse kaydı güncelliyoruz
                        if (result != null)
                        {
                            //formdan gelen bilgileri veritabanındaki alanlara atıyoruz
                            result.daireId = g.daireId;
                            result.kisiId = g.kisiId;

                            db.SaveChanges(); //bilgileri veritabanına kayıt ediyoruz
                            outMessage = "Bilgiler Güncelleştirildi."; //forma geri göndereceğimiz mesaj
                        }
                        else //gelen bilgi yeni bir kayıt ise
                        {
                            db.DaireKisis.Add(g);
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
        public _DaireKisi getDaireKisi(int postId) //formdan gelen parametre Id'sine göre site bilgilerini gönderiyoruz 
        {
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                using (var db = new SQLDBModel(connection, true))
                {
                    return (from dk in db.DaireKisis
                            join d in db.Daires on dk.daireId equals d.Id
                            join b in db.Bloks on d.blokId equals b.Id
                            join s in db.Sites on b.siteId equals s.Id
                            join k in db.Kisilers on dk.kisiId equals k.Id
                            where dk.Id == postId  //formdan gelen id veritabanında varmı kontrol ediyoruz
                            select new _DaireKisi
                            {
                                Id = dk.Id,
                                siteAdi = s.siteName,
                                blokAdi = b.blokAdi,
                                daireNo = d.daireNo,
                                adiSoyadi = k.adi + " " + k.soyadi,
                                siteId=s.Id,
                                blokId=b.Id,
                                daireId=d.Id,
                                kisiId=k.Id
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
                        var result = (from dk in db.DaireKisis where dk.Id == postId select dk).FirstOrDefault(); //gelen değeri veritabanından kontrol ediyoruz
                        if (result != null) //gelen değer veritabanında varsa
                        {
                            db.DaireKisis.Remove(result); //gelen değere göre bulduğumuz kaydı veritabanından siliyoruz
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
        public class _Blok
        {
            public int Id { get; set; }
            public string blokAdi { get; set; }
            public int siteId { get; set; }
        }
        public List<_Blok> listBlok(int postId)
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    var result = (from b in db.Bloks
                                  select new _Blok // tanımlamış olduğumuz classa alanları aktarıyoruz
                                  {
                                      Id = b.Id,
                                      blokAdi = b.blokAdi,
                                      siteId=b.siteId
                                  }
                            );
                    if (postId != 0) result = result.Where(a => a.siteId == postId);
                    return result.OrderBy(a => a.blokAdi).ToList();
                }
            }
        }
        public class _Daire
        {
            public int Id { get; set; }
            public string daireNo { get; set; }
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
                                Id=d.Id,
                                daireNo = d.daireNo
                            }
                            ).OrderBy(a => a.daireNo).ToList(); //Liste olarak verileri sıralayarak geri döndürüyoruz
                }
            }
        }
        public class _Kisi
        {
            public int Id { get; set; }
            public string adiSoyadi { get; set; }
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
                            select new _Kisi // tanımlamış olduğumuz _Kisi classına alanları aktarıyoruz
                            {
                                Id = k.Id,
                                adiSoyadi =k.adi + " " + k.soyadi
                            }
                            ).OrderBy(a => a.adiSoyadi).ToList(); //Liste olarak verileri adı soyadına göre sıralayarak geri döndürüyoruz
                }
            }
        }
    }
}
