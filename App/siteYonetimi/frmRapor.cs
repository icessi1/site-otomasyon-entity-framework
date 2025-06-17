using siteYonetimi.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace siteYonetimi
{
    public partial class frmRapor : Form
    {
        qRapor query = new qRapor(); //kod içinde kullanacağımız query sınıfını burada atama yapıyoruz
        public frmRapor()
        {
            InitializeComponent();
        }

        private void frmRapor_Load(object sender, EventArgs e)
        {
            txtSite.Text = query.siteSayisi().ToString() + " Adet";
            txtBlok.Text = query.blokSayisi().ToString() + " Adet";
            txtDaire.Text = query.daireSayisi().ToString() + " Adet";
            txtKisi.Text = query.kisiSayisi().ToString() + " Adet";
            txt18.Text = query.yas18buyuk().ToString() + " Adet";

            dataGridView1.DataSource = query.listKisiTur(); //listemizi gride yüklüyoruz
            dataGridView1.Refresh();

            dataGridView2.DataSource = query.listMedeniDurum(); //listemizi gride yüklüyoruz
            dataGridView2.Refresh();

        }
    }
}
