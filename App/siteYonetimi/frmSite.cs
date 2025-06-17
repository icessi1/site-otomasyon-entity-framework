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
    public partial class frmSite : Form
    {
        qSite query = new qSite(); //kod içinde kullanacağımız query sınıfını burada atama yapıyoruz
        public frmSite()
        {
            InitializeComponent();
        }

        private void frmSite_Load(object sender, EventArgs e)
        {
            //formun ilk açılışında site listemizi gride yüklüyoruz
            gridGuncelle(); //gridGuncelle tanımlıyoruz form ilk açıldığında bu fonksiyonu çağırıyoruz

            //formun ilk açılışında
            //iller combobox'a sorgusunu tanımladığımız atamayı yaparak verileri çekiyoruz
            cmbIl.DataSource = query.listIller();
            cmbIl.DisplayMember = "ilAdi"; //combobox ta görenecek olan kısma il adını atıyoruz
            cmbIl.ValueMember = "Id"; //seçilen değerin hangi alanının alınacağını belirtiyoruz
        }
        private void gridGuncelle()
        {
            dataGridView1.DataSource = query.listSiteler();
            dataGridView1.Refresh();
        }
        private void btnYeni_Click(object sender, EventArgs e)
        {
            //yeni butonuna basıldığında formdaki alanları boşaltıyoruz
            txtId.Text = "";
            txtSiteAdi.Text = "";
            txtAdres.Text = "";
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //kaydet butonuna basıldığında forma girilen bilgileri veritabanına kayıt ediyoruz

            //öncelikle alanların dolu olup olmadığını kontol ediyoruz
            //site adı ve adres alanı boşsa uyarı veriyoruz
            if (txtSiteAdi.Text=="" || txtAdres.Text=="")
            {
                MessageBox.Show("Tüm alanları dodurmanız gerekiyor.");
            }
            else
            {
                string outMessage;
                try
                {
                    //veri tabanındaki site tipinde bir değişken tanımlıyoruz
                    site DbSave = new site(); 

                    //txtId alanı boşka 0 olarak gönderiyoruz boş değilse değeri int dönüştürüp gönderiyoruz
                    //alan 0 olarak giderse yeni kayıt olacak değer giderse güncelleme olacak
                    DbSave.Id = txtId.Text == "" ? 0 : Convert.ToInt32(txtId.Text); 

                    //formdaki değerleri atama yapıyoruz
                    DbSave.siteName = txtSiteAdi.Text;
                    DbSave.adres = txtAdres.Text;
                    DbSave.ilId = Convert.ToInt32(cmbIl.SelectedValue);

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
            if (txtId.Text=="")
            {
                MessageBox.Show("Öncelikle silinecek kaydı seçmelisiniz", "Sil", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string outMessage;
                query.delete(Convert.ToInt32(txtId.Text),out outMessage);
                MessageBox.Show(outMessage, "Sil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //kayıt silindiğinde gridimizi güncelliyoruz
                gridGuncelle();

                //kayıt silindiğinde formdaki alanları boşaltıyoruz
                txtId.Text = "";
                txtSiteAdi.Text = "";
                txtAdres.Text = "";
            }
        }
        private void btnParametre_Click(object sender, EventArgs e)
        {
            if (txtId.Text!="")
            {
                frmSiteParametre f = new frmSiteParametre(); //frmSiteParametre tipinde bir değişken tanımlıyoruz
                f.txtSiteId.Text = txtId.Text; //not: frmSiteParametre formundaki txtSiteId textbox'un özelliklerindeki Modifiers özelliğini Public yapıyoruz bu şekilde bu formdan bu alana değer gönderibiyoruz
                f.Show(); //Form tipinde tanımladığımız değişkeni açıyoruz
            }
            else
            {
                MessageBox.Show("Parametre tanımlanacak kaydı seçmelisiniz", "Parametre", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
          
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //griddeki alan çift tıklandığı zaman birinci kolon değerini txtId textbox'a atıyoruz
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            var result = query.getSite(Convert.ToInt32(txtId.Text)); // seçilen değeri veritabanından sorgulayarak gelen sonucu bir değişkene atıyoruz

            //gelend değerli formumuzdaki alanlara atıyoruz
            txtSiteAdi.Text = result.siteAdi;
            txtAdres.Text = result.adres;
            cmbIl.SelectedValue = result.IlId;
        }

        private void btnBlok_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                frmBlok f = new frmBlok(); //frmBlok tipinde bir değişken tanımlıyoruz
                f.txtSiteId.Text = txtId.Text; //not: frmBlok formundaki txtSiteId textbox'un özelliklerindeki Modifiers özelliğini Public yapıyoruz bu şekilde bu formdan bu alana değer gönderibiyoruz
                f.txtSiteAdi.Text = txtSiteAdi.Text; //not: frmBlok formundaki txtSiteAdi textbox'un özelliklerindeki Modifiers özelliğini Public yapıyoruz bu şekilde bu formdan bu alana değer gönderibiyoruz
                f.Show(); //Form tipinde tanımladığımız değişkeni açıyoruz
            }
            else
            {
                MessageBox.Show("Blok tanımlanacak kaydı seçmelisiniz", "Blok", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
