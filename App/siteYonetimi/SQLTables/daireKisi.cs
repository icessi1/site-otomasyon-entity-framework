namespace siteYonetimi.SQLTables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("daireKisi")] //sql veri tabanındaki tablomuzun adı
    //sql veritabanında tanımladığımız tablonun alanlarını ve özelliklerini birebir burda tanımlıyoruz, alan adları özellikleri birebir veritabanında tanımlandığı gibi olmalı
    //alnalardan kullanmayacağımız alanları tanımlamamıza gerek yok fakat biz anlaşılması için tüm alanları birebir ekliyoruz
    public partial class daireKisi
    {
        public int Id { get; set; }

        public int daireId { get; set; }
        public int kisiId { get; set; }
    }
}