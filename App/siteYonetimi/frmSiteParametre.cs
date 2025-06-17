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
    public partial class frmSiteParametre : Form
    {
        qSiteParametre query = new qSiteParametre(); //kod içinde kullanacağımız query sınıfını burada atama yapıyoruz
        public frmSiteParametre()
        {
            InitializeComponent();
        }

        private void frmSiteParametre_Load(object sender, EventArgs e)
        {
            //formun ilk açılışında site listemizi gride yüklüyoruz
            gridGuncelle(); //gridGuncelle tanımlıyoruz form ilk açıldığında bu fonksiyonu çağırıyoruz

            //formun ilk açılışında
            //parametre combobox'a sorgusunu tanımladığımız atamayı yaparak verileri çekiyoruz
            cmbParametre.DataSource = query.listParametreler();
            cmbParametre.DisplayMember = "aciklama"; //combobox ta görenecek olan kısma il adını atıyoruz
            cmbParametre.ValueMember = "Id"; //seçilen değerin hangi alanının alınacağını belirtiyoruz

            //formun ilk açılışında
            //kişi combobox'a sorgusunu tanımladığımız atamayı yaparak verileri çekiyoruz
            cmbKisi.DataSource = query.listKisiler();
            cmbKisi.DisplayMember = "adiSoyadi"; //combobox ta görenecek olan kısma il adını atıyoruz
            cmbKisi.ValueMember = "Id"; //seçilen değerin hangi alanının alınacağını belirtiyoruz
        }
        private void gridGuncelle()
        {
            dataGridView1.DataSource = query.listSitetParametreler(Convert.ToInt32(txtSiteId.Text));
            dataGridView1.Refresh();
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            //yeni butonuna basıldığında formdaki alanları boşaltıyoruz
            txtId.Text = "";
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //kaydet butonuna basıldığında forma girilen bilgileri veritabanına kayıt ediyoruz

            string outMessage;
            try
            {
                //veri tabanındaki site tipinde bir değişken tanımlıyoruz
                siteParametre DbSave = new siteParametre();

                //txtId alanı boşka 0 olarak gönderiyoruz boş değilse değeri int dönüştürüp gönderiyoruz
                //alan 0 olarak giderse yeni kayıt olacak değer giderse güncelleme olacak
                DbSave.Id = txtId.Text == "" ? 0 : Convert.ToInt32(txtId.Text);

                //formdaki değerleri atama yapıyoruz
                DbSave.siteId = Convert.ToInt32(txtSiteId.Text);
                DbSave.parametreId = Convert.ToInt32(cmbParametre.SelectedValue);
                DbSave.kisiId = Convert.ToInt32(cmbKisi.SelectedValue);

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
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //griddeki alan çift tıklandığı zaman birinci kolon değerini txtId textbox'a atıyoruz
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            var result = query.getParemetre(Convert.ToInt32(txtId.Text)); // seçilen değeri veritabanından sorgulayarak gelen sonucu bir değişkene atıyoruz

            //gelend değerli formumuzdaki alanlara atıyoruz
            cmbParametre.SelectedValue = result.parametreId;
            cmbKisi.SelectedValue = result.kisiId;
        }
    }
}
