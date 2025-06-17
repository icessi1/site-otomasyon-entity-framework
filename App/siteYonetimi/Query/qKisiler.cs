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
    //kişiler ile ilgili tüm sorgulamalarımızı bu class içinde tanımlıyoruz Formun code kısmında da burdaki sorgularımızı çalıştırıyourz
    public class qKisiler
    {

        //gridde kullanacağımız başlıklara veri yüklemek için burda bir class olarak bu alanları ve özelliklerini tanımlıyoruz
        public class _Kisi
        {
            public int Id { get; set; }
            public string tcKimlikNo { get; set; }
            public string adi { get; set; }
            public string soyadi { get; set; }
            public DateTime dogumTarihi { get; set; }
            public string dogumYeri { get; set; }
            public string medeniDurumu { get; set; }
            public Boolean kiracimi { get; set; }
            public string gsmNo { get; set; }
            public int dogumYeriId { get; set; }
            public int medeniDurumId { get; set; }
        }

        //form sayfasında yeni kişi tanımlarken il seçimi yapılabilmesi için iller tablosundaki verileri atayacağımız classı tanımlıyoruz
        public class _iller
        {
            public int Id { get; set; }
            public string ilAdi { get; set; }
        }
        //form sayfasında yeni kişi tanımlarken medeni durumu seçimi yapılabilmesi için parametreler tablosundaki verileri atayacağımız classı tanımlıyoruz
        public class _medeniDurum
        {
            public int Id { get; set; }
            public string aciklama { get; set; }
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
                            join i in db.İllers on k.dogumYeriId equals i.Id // kişilerdeki dogumyeriId alanını iller tablosuna bağlıyoruz
                            join p in db.Parametrelers on k.medeniDurumId equals p.Id
                            select new _Kisi // tanımlamış olduğumuz _Kisi classına alanları aktarıyoruz
                            {
                                Id = k.Id,
                                tcKimlikNo = k.tcKimlikNo,
                                adi=k.adi,
                                soyadi=k.soyadi,
                                dogumTarihi=k.dogumTarihi,
                                dogumYeri=i.ilAdi,
                                medeniDurumu=p.aciklama,
                                kiracimi=k.kiracimi,
                                gsmNo=k.gsmNo,
                                dogumYeriId=k.dogumYeriId,
                                medeniDurumId=k.medeniDurumId
                            }
                            ).OrderBy(a => a.adi).ThenBy(a=>a.soyadi).ToList(); //Liste olarak verileri adı soyadına göre sıralayarak geri döndürüyoruz
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
        public List<_medeniDurum> listMedeniDurum()
        {
            //connectionString kullanarak bağlanmak istediğimiz SQL veritabanına bağlnıyoruz
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open(); //veritabanı açık değilse açıyoruz
                using (var db = new SQLDBModel(connection, true)) //tanımlamış olduğumuz model bağlanıyoruz
                {
                    return (from p in db.Parametrelers
                            where p.parentId==8 //medeni durumların id si
                            select new _medeniDurum // tanımlamış olduğumuz _medeniDurum classına alanları aktarıyoruz
                            {
                                Id = p.Id,
                                aciklama = p.aciklama
                            }
                            ).OrderBy(a => a.aciklama).ToList(); //Liste olarak verileri açıklamaya göre sıralayarak geri döndürüyoruz
                }
            }
        }
        public void InsertOrUpdate(kisiler g, out string outMessage) //formdan bize gelecek olan bilgileri tanımlıyoruz kisi tipinde g verisi gelecek ve dönüş mesajı tanımlaması yapıyoruz
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
                        var result = (from k in db.Kisilers where k.Id == g.Id select k).FirstOrDefault();

                        //eğer formdan gelen bilgiler var olan bir kayda aitse kaydı güncelliyoruz
                        if (result != null)
                        {
                            //formdan gelen bilgileri veritabanındaki alanlara atıyoruz
                            result.tcKimlikNo = g.tcKimlikNo;
                            result.adi = g.adi;
                            result.soyadi = g.soyadi;
                            result.dogumTarihi = g.dogumTarihi;
                            result.dogumYeriId = g.dogumYeriId;
                            result.medeniDurumId = g.medeniDurumId;
                            result.kiracimi = g.kiracimi;
                            result.gsmNo = g.gsmNo;

                            db.SaveChanges(); //bilgileri veritabanına kayıt ediyoruz
                            outMessage = "Bilgiler Güncelleştirildi."; //forma geri göndereceğimiz mesaj
                        }
                        else //gelen bilgi yeni bir kayıt ise
                        {
                            db.Kisilers.Add(g);
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
        public _Kisi getKisi(int postId) //formdan gelen kişi Id'sine göre site bilgilerini gönderiyoruz 
        {
            using (var connection = new SqlConnection() { ConnectionString = connectionString.sqlConnect() })
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                using (var db = new SQLDBModel(connection, true))
                {
                    return (from k in db.Kisilers
                            where k.Id == postId  //formdan gelen id veritabanında varmı kontrol ediyoruz
                            select new _Kisi
                            {
                                Id = k.Id,
                                tcKimlikNo = k.tcKimlikNo,
                                adi = k.adi,
                                soyadi = k.soyadi,
                                dogumTarihi = k.dogumTarihi,
                                dogumYeriId = k.dogumYeriId,
                                medeniDurumId = k.medeniDurumId,
                                kiracimi=k.kiracimi,
                                gsmNo=k.gsmNo
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
                        var result = (from k in db.Kisilers where k.Id == postId select k).FirstOrDefault(); //gelen değeri veritabanından kontrol ediyoruz
                        if (result != null) //gelen değer veritabanında varsa
                        {
                            db.Kisilers.Remove(result); //gelen değere göre bulduğumuz kaydı veritabanından siliyoruz
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

