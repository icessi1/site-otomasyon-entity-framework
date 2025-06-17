namespace siteYonetimi.SQLTables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("kisiler")] //sql veri tabanındaki tablomuzun adı
    //sql veritabanında tanımladığımız tablonun alanlarını ve özelliklerini birebir burda tanımlıyoruz, alan adları özellikleri birebir veritabanında tanımlandığı gibi olmalı
    //alnalardan kullanmayacağımız alanları tanımlamamıza gerek yok fakat biz anlaşılması için tüm alanları birebir ekliyoruz
    public partial class kisiler
    {
        public int Id { get; set; }

        [StringLength(11)]
        public string tcKimlikNo { get; set; }
        [StringLength(100)]
        public string adi { get; set; }
        [StringLength(100)]
        public string soyadi { get; set; }
        public DateTime dogumTarihi { get; set; }
        public int dogumYeriId { get; set; }
        public int medeniDurumId { get; set; }
        public Boolean kiracimi { get; set; }
         [StringLength(11)]
        public string gsmNo { get; set; }
    }
}