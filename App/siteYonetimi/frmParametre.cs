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
    public partial class frmParametre : Form
    {
        qParametreler query = new qParametreler(); //kod içinde kullanacağımız query sınıfını burada atama yapıyoruz
        public frmParametre()
        {
            InitializeComponent();
        }
        private void frmParametre_Load(object sender, EventArgs e)
        {
            //formun ilk açılışında parametre listemizi gride yüklüyoruz
            gridGuncelle(); //gridGuncelle tanımlıyoruz form ilk açıldığında bu fonksiyonu çağırıyoruz
        }
        private void gridGuncelle()
        {
            dataGridView1.DataSource = query.listParametreler();
            dataGridView1.Refresh();
        }
        private void parametreGuncelle()
        {
            //parametre verilerini çekiyoruz
            cmbParametre.DataSource = query.listAnaParametreler();
            cmbParametre.DisplayMember = "Aciklama"; //combobox ta görenecek olan kısma il adını atıyoruz
            cmbParametre.ValueMember = "Id"; //seçilen değerin hangi alanının alınacağını belirtiyoruz
        }
        private void chcParametre_CheckedChanged(object sender, EventArgs e)
        {
            if (chcParametre.Checked==true) //ana parametre ise parametre alanının görünürlüğünü kapatıyoruz
            {
                label4.Visible = false;
                cmbParametre.Visible = false;
            }
            else //ana parametre değilse parametre alanının görünürlüğünü açıyoruz
            {
                parametreGuncelle();   //ana parametre değilse parametreleri combobox'a yüklüyoruz
                label4.Visible = true;
                cmbParametre.Visible = true;
            }
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            //yeni butonuna basıldığında formdaki alanları boşaltıyoruz
            txtId.Text = "";
            txtAciklama.Text = "";
            chcParametre.Checked = true;
            label4.Visible = false;
            cmbParametre.Visible = false;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //kadet butonuna basıldığında forma girilen bilgileri veritabanına kayıt ediyoruz

            //öncelikle alanların dolu olup olmadığını kontol ediyoruz
            //site adı ve adres alanı boşsa uyarı veriyoruz
            if (txtAciklama.Text == "")
            {
                MessageBox.Show("Tüm alanları dodurmanız gerekiyor.");
            }
            else
            {
                string outMessage;
                try
                {
                    //veri tabanındaki site tipinde bir değişken tanımlıyoruz
                    parametreler DbSave = new parametreler();

                    //txtId alanı boşka 0 olarak gönderiyoruz boş değilse değeri int dönüştürüp gönderiyoruz
                    //alan 0 olarak giderse yeni kayıt olacak değer giderse güncelleme olacak
                    DbSave.Id = txtId.Text == "" ? 0 : Convert.ToInt32(txtId.Text);

                    //formdaki değerleri atama yapıyoruz
                    DbSave.aciklama = txtAciklama.Text;
                    DbSave.parentId = !chcParametre.Checked ? Convert.ToInt32(cmbParametre.SelectedValue) : 0;

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
            else if (txtId.Text=="1" || txtId.Text=="2" || txtId.Text=="8") //sistem parametrelerini silmesini engelliyoruz
            {
                MessageBox.Show("Sistem parametrelerini silemezsiniz", "Sil", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtAciklama.Text = "";
                chcParametre.Checked = true;
                label4.Visible = false;
                cmbParametre.Visible = false;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //griddeki alan çift tıklandığı zaman birinci kolon değerini txtId textbox'a atıyoruz
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            var result = query.getParemetre(Convert.ToInt32(txtId.Text)); // seçilen değeri veritabanından sorgulayarak gelen sonucu bir değişkene atıyoruz

            //gelend değerli formumuzdaki alanlara atıyoruz
            txtAciklama.Text = result.aciklama;
            if (result.parentId!=0)
            {
                chcParametre.Checked = false;
                label4.Visible = true;
                cmbParametre.Visible = true;
            }
            else
            {
                chcParametre.Checked = true;
                label4.Visible = false;
                cmbParametre.Visible = false;
            }
        }
    }
}
