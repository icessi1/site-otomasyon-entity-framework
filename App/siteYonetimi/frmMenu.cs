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
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void btnSite_Click(object sender, EventArgs e)
        {
            //butona tıkladığında site formunu açıyoruz
            frmSite f = new frmSite(); //formSite tipinde bir değişken tanımlıyoruz
            f.Show(); //Form tipinde tanımladığımız değişkeni açıyoruz
        }

        private void frmParametre_Click(object sender, EventArgs e)
        {
            //butona tıkladığında parametre formunu açıyoruz
            frmParametre f = new frmParametre(); //frmParametre tipinde bir değişken tanımlıyoruz
            f.Show(); //Form tipinde tanımladığımız değişkeni açıyoruz
        }

        private void btnKisi_Click(object sender, EventArgs e)
        {
            //butona tıkladığında parametre formunu açıyoruz
            frmKisi f = new frmKisi(); //frmKisi tipinde bir değişken tanımlıyoruz
            f.Show(); //Form tipinde tanımladığımız değişkeni açıyoruz
        }

        private void btnDaireKisi_Click(object sender, EventArgs e)
        {
            //butona tıkladığında parametre formunu açıyoruz
            frmDaireKisi f = new frmDaireKisi(); //frmDaireKisi tipinde bir değişken tanımlıyoruz
            f.Show(); //Form tipinde tanımladığımız değişkeni açıyoruz
        }

        private void btnRapor_Click(object sender, EventArgs e)
        {
            //butona tıkladığında parametre formunu açıyoruz
            frmRapor f = new frmRapor(); //frmRapor tipinde bir değişken tanımlıyoruz
            f.Show(); //Form tipinde tanımladığımız değişkeni açıyoruz
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
