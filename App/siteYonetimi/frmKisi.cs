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
    public partial class frmKisi : Form
    {
        qKisiler query = new qKisiler(); //kod içinde kullanacağımız query sınıfını burada atama yapıyoruz
        public frmKisi()
        {
            InitializeComponent();
        }

        private void frmKisi_Load(object sender, EventArgs e)
        {
            //formun ilk açılışında kişi listemizi gride yüklüyoruz
            gridGuncelle(); //gridGuncelle tanımlıyoruz form ilk açıldığında bu fonksiyonu çağırıyoruz

            //formun ilk açılışında
            //iller combobox'a sorgusunu tanımladığımız atamayı yaparak verileri çekiyoruz
            cmbIl.DataSource = query.listIller();
            cmbIl.DisplayMember = "ilAdi"; //combobox ta görenecek olan kısma il adını atıyoruz
            cmbIl.ValueMember = "Id"; //seçilen değerin hangi alanının alınacağını belirtiyoruz

            //medeni durumunu combobox'a sorgusunu tanımladığımız atamayı yaparak verileri çekiyoruz
            cmbParametre.DataSource = query.listMedeniDurum();
            cmbParametre.DisplayMember = "aciklama"; //combobox ta görenecek olan kısma parametre açıklamasını atıyoruz
            cmbParametre.ValueMember = "Id"; //seçilen değerin hangi alanının alınacağını belirtiyoruz
        }
        private void gridGuncelle()
        {
            dataGridView1.DataSource = query.listKisiler();
            dataGridView1.Refresh();
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            //yeni butonuna basıldığında formdaki alanları boşaltıyoruz
            txtId.Text = "";
            txtTC.Text = "";
            txtAdı.Text = "";
            txtSoyadi.Text = "";
            chcKiraci.Checked = false;
            txtGSM.Text = "";
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //kadet butonuna basıldığında forma girilen bilgileri veritabanına kayıt ediyoruz

            //öncelikle alanların dolu olup olmadığını kontol ediyoruz
            //alanlar boşsa uyarı veriyoruz
            if (txtTC.Text == "" || txtAdı.Text == "" || txtSoyadi.Text=="" || txtGSM.Text=="" || txtTC.Text.Length<11 || txtGSM.Text.Length<11)
            {
                MessageBox.Show("Tüm alanları eksiksiz dodurmanız gerekiyor.");
            }
            else
            {
                string outMessage;
                try
                {
                    //veri tabanındaki kisiler tipinde bir değişken tanımlıyoruz
                    kisiler DbSave = new kisiler();

                    //txtId alanı boşka 0 olarak gönderiyoruz boş değilse değeri int dönüştürüp gönderiyoruz
                    //alan 0 olarak giderse yeni kayıt olacak değer giderse güncelleme olacak
                    DbSave.Id = txtId.Text == "" ? 0 : Convert.ToInt32(txtId.Text);

                    //formdaki değerleri atama yapıyoruz
                    DbSave.tcKimlikNo = txtTC.Text;
                    DbSave.adi = txtAdı.Text;
                    DbSave.soyadi = txtSoyadi.Text;
                    DbSave.dogumTarihi = Convert.ToDateTime(dateTimePicker1.Text);
                    DbSave.dogumYeriId = Convert.ToInt32(cmbIl.SelectedValue);
                    DbSave.medeniDurumId = Convert.ToInt32(cmbParametre.SelectedValue);
                    DbSave.kiracimi = Convert.ToBoolean(chcKiraci.Checked);
                    DbSave.gsmNo = txtGSM.Text;

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
                txtTC.Text = "";
                txtAdı.Text = "";
                txtSoyadi.Text = "";
                chcKiraci.Checked = false;
                txtGSM.Text = "";
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //griddeki alan çift tıklandığı zaman birinci kolon değerini txtId textbox'a atıyoruz
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            var result = query.getKisi(Convert.ToInt32(txtId.Text)); // seçilen değeri veritabanından sorgulayarak gelen sonucu bir değişkene atıyoruz

            //gelend değerli formumuzdaki alanlara atıyoruz
            txtTC.Text = result.tcKimlikNo;
            txtAdı.Text = result.adi;
            txtSoyadi.Text = result.soyadi;
            dateTimePicker1.Text = result.dogumTarihi.ToShortDateString();
            cmbIl.SelectedValue = result.dogumYeriId;
            cmbParametre.SelectedValue = result.medeniDurumId;
            chcKiraci.Checked = result.kiracimi;
            txtGSM.Text = result.gsmNo;
        }

        private void txtTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            //textbox alanına sadece sayı girilebilmesini yapıyoruz
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtGSM_KeyPress(object sender, KeyPressEventArgs e)
        {
            //textbox alanına sadece sayı girilebilmesini yapıyoruz
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
