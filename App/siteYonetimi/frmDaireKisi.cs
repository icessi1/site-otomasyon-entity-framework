using siteYonetimi.Query;
using siteYonetimi.SQLTables;
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
    public partial class frmDaireKisi : Form
    {
        qDaireKisi query = new qDaireKisi(); //kod içinde kullanacağımız query sınıfını burada atama yapıyoruz
        public frmDaireKisi()
        {
            InitializeComponent();
        }

        private void frmDaireKisi_Load(object sender, EventArgs e)
        {
            //formun ilk açılışında kişi listemizi gride yüklüyoruz
            gridGuncelle(); //gridGuncelle tanımlıyoruz form ilk açıldığında bu fonksiyonu çağırıyoruz

            //formun ilk açılışında
            //site combobox'a sorgusunu tanımladığımız atamayı yaparak verileri çekiyoruz
            qSite querySite = new qSite(); //qSite class'ındaki fonksiyonları çağırabilmemiz için tanımlama yapıyoruz

            //site combobox'a sorgusunu tanımladığımız atamayı yaparak verileri çekiyoruz
            var dataSite = querySite.listSiteler();

            cmbSite.Items.Clear(); // combobox içindeki verileri temiziyoruz
            Dictionary<int, string> veriSite = new Dictionary<int, string> //veritabanından çektiğimiz verileri yüklemek için Dictionary tanımlıyoruz
                { { 0, "Seçiniz"} }; //ilk satıra seçiniz ekliyoruz

            //data array ımızı döndürerek verileri comboboxa ekliyoruz
            foreach (var item in dataSite)
            {
                veriSite.Add(item.Id, item.siteAdi);
            }
            cmbSite.DataSource = new BindingSource(veriSite, null);
            cmbSite.DisplayMember = "Value";
            cmbSite.ValueMember = "Key";


            //formun ilk açılışında
            //kisi combobox'a sorgusunu tanımladığımız atamayı yaparak verileri çekiyoruz
            var dataKisi = query.listKisiler();
            cmbKisi.Items.Clear(); // combobox içindeki verileri temiziyoruz
            Dictionary<int, string> veri = new Dictionary<int, string> //veritabanından çektiğimiz verileri yüklemek için Dictionary tanımlıyoruz
                { { 0, "Seçiniz"} }; //ilk satıra seçiniz ekliyoruz

            //data array ımızı döndürerek verileri comboboxa ekliyoruz
            foreach (var item in dataKisi)
            {
                veri.Add(item.Id, item.adiSoyadi);
            }
            cmbKisi.DataSource = new BindingSource(veri, null);
            cmbKisi.DisplayMember = "Value";
            cmbKisi.ValueMember = "Key";
        }
        private void gridGuncelle()
        {
            dataGridView1.DataSource = query.listDaireKisi(Convert.ToInt32(cmbSite.SelectedValue == null ? 0 : cmbSite.SelectedValue)
                , Convert.ToInt32(cmbBlok.SelectedValue ==null ? 0 : cmbBlok.SelectedValue)
                , Convert.ToInt32(cmbDaire.SelectedValue ==null ? 0 : cmbDaire.SelectedValue)); //filtreleme yaparak listeyi çağırıyoruz
            dataGridView1.Refresh();
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            //yeni butonuna basıldığında formdaki alanları boşaltıyoruz
            txtId.Text = "";
            cmbSite.SelectedIndex = -1;
            cmbKisi.SelectedIndex = -1;
            cmbBlok.DataSource = null;
            cmbDaire.DataSource = null;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //kaydet butonuna basıldığında forma girilen bilgileri veritabanına kayıt ediyoruz
            if (cmbSite.SelectedIndex==-1 || cmbBlok.SelectedIndex == -1 || cmbDaire.SelectedIndex==-1 || cmbKisi.SelectedIndex==-1)
            {
                MessageBox.Show("Tüm alanları eksiksiz dodurmanız gerekiyor.");
            }
            else
            {
                string outMessage;
                try
                {
                    //veri tabanındaki daireKisi tipinde bir değişken tanımlıyoruz
                    daireKisi DbSave = new daireKisi();

                    //txtId alanı boşka 0 olarak gönderiyoruz boş değilse değeri int dönüştürüp gönderiyoruz
                    //alan 0 olarak giderse yeni kayıt olacak değer giderse güncelleme olacak
                    DbSave.Id = txtId.Text == "" ? 0 : Convert.ToInt32(txtId.Text);

                    //formdaki değerleri atama yapıyoruz
                    ;

                    DbSave.daireId = Convert.ToInt32(((KeyValuePair<int, string>)cmbDaire.SelectedItem).Key);
                    DbSave.kisiId = Convert.ToInt32(((KeyValuePair<int, string>)cmbKisi.SelectedItem).Key);


                    //query olarak tanımladığımız class a değerleri gönderiyoruz
                    query.InsertOrUpdate(DbSave, out outMessage);

                    //dönen mesajı ekranda gösteriyoruz
                    MessageBox.Show(outMessage, "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //yeni kayıtsa kayıt numarası text alanına atıyoruz
                    txtId.Text = DbSave.Id.ToString();

                    //kayıt işleminden sonra gridimizin güncellenmesi için tanımlamış olduğumuz fonksiyonu çağırıyoruz
                    gridGuncelle();
                }
                catch (Exception ex)
                {
                    //sistem hata veriyorsa hata mesajını gösteriyoruz
                    MessageBox.Show(ex.Message, "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //kayıt sil butonuna basıldığında
            //öncelikle silenecek kayıt seçilmişmi kontrol ediyoruz seçilmemişse uyarı variyoruz
            if (txtId.Text == "")
            {
                MessageBox.Show("Öncelikle silinecek kaydı seçmelisiniz", "Sil", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string outMessage;
                query.delete(Convert.ToInt32(txtId.Text), out outMessage);
                MessageBox.Show(outMessage, "Sil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //kayıt silindiğinde gridimizi güncelliyoruz
                gridGuncelle();

                //kayıt silindiğinde formdaki alanları boşaltıyoruz
                txtId.Text = "";
                cmbSite.SelectedIndex = -1;
                cmbKisi.SelectedIndex = -1;
                cmbBlok.DataSource = null;
                cmbDaire.DataSource = null;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //griddeki alan çift tıklandığı zaman birinci kolon değerini txtId textbox'a atıyoruz
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            var result = query.getDaireKisi(Convert.ToInt32(txtId.Text)); // seçilen değeri veritabanından sorgulayarak gelen sonucu bir değişkene atıyoruz

            //gelend değerli formumuzdaki alanlara atıyoruz
            cmbSite.SelectedValue = result.siteId;
            cmbBlok.SelectedValue = result.blokId;
            cmbDaire.SelectedValue = result.daireId;
            cmbKisi.SelectedValue = result.kisiId;
        }

        private void cmbSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            //site değiştiğinde sitenin bloklarını çağırıyoruz

            if (cmbSite.SelectedIndex>0)
            {
                //blok combobox'a sorgusunu tanımladığımız atamayı yaparak verileri çekiyoruz
                var data = query.listBlok(Convert.ToInt32(cmbSite.SelectedIndex == 0 ? 0 : cmbSite.SelectedValue));
                cmbBlok.Items.Clear(); // combobox içindeki verileri temiziyoruz
                Dictionary<int, string> veri = new Dictionary<int, string> //veritabanından çektiğimiz verileri yüklemek için Dictionary tanımlıyoruz
                { { 0, "Seçiniz"} }; //ilk satıra seçiniz ekliyoruz

                //data array ımızı döndürerek verileri comboboxa ekliyoruz
                foreach (var item in data)
                {
                    veri.Add(item.Id, item.blokAdi);
                }
                cmbBlok.DataSource = new BindingSource(veri, null);
                cmbBlok.DisplayMember = "Value";
                cmbBlok.ValueMember = "Key";
            }
        }

        private void cmbBlok_SelectedIndexChanged(object sender, EventArgs e)
        {
            //blok değiştiğinde sitenin daire ve katlarını çağırıyoruz
            int blokId = 0;
            if (cmbBlok.SelectedIndex>0) blokId = ((KeyValuePair<int, string>)cmbBlok.SelectedItem).Key; //blok ta seçilen key değerini okuyoruz
             

            if (blokId != 0) //gelen değer 0 dan farklıysa bunu yap
            {
                //daire combobox'a sorgusunu tanımladığımız atamayı yaparak verileri çekiyoruz
                var data = query.listDaire(blokId);

                cmbDaire.Items.Clear(); // combobox içindeki verileri temiziyoruz
                Dictionary<int, string> veri = new Dictionary<int, string> //veritabanından çektiğimiz verileri yüklemek için Dictionary tanımlıyoruz
                    { { 0, "Seçiniz"} }; //ilk satıra seçiniz ekliyoruz

                //data array ımızı döndürerek verileri comboboxa ekliyoruz
                foreach (var item in data)
                {
                    veri.Add(item.Id, item.daireNo);
                }
                cmbDaire.DataSource = new BindingSource(veri, null);
                cmbDaire.DisplayMember = "Value";
                cmbDaire.ValueMember = "Key";
            }
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = query.listDaireKisi(0,0,0); //filtreleme yapmadan tüm listeyi çağırıyoruz
            dataGridView1.Refresh();
        }
    }
}
