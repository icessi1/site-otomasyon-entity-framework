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
    public partial class frmDaire : Form
    {
        qDaire query = new qDaire(); //kod içinde kullanacağımız query sınıfını burada atama yapıyoruz
        public frmDaire()
        {
            InitializeComponent();
        }

        private void frmDaire_Load(object sender, EventArgs e)
        {
            //formun ilk açılışında blok listemizi gride yüklüyoruz
            gridGuncelle(); //gridGuncelle tanımlıyoruz form ilk açıldığında bu fonksiyonu çağırıyoruz
        }
        private void gridGuncelle()
        {
            dataGridView1.DataSource = query.listDaire(Convert.ToInt32(txtBlokId.Text));
            dataGridView1.Refresh();
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            //yeni butonuna basıldığında formdaki alanları boşaltıyoruz
            txtId.Text = "";
            txtDaire.Text = "";
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //kaydet butonuna basıldığında forma girilen bilgileri veritabanına kayıt ediyoruz

            //öncelikle alanların dolu olup olmadığını kontol ediyoruz
            //site adı ve adres alanı boşsa uyarı veriyoruz
            if (txtDaire.Text == "" )
            {
                MessageBox.Show("Tüm alanları dodurmanız gerekiyor.");
            }
            else
            {
                string outMessage;
                try
                {
                    //veri tabanındaki daire tipinde bir değişken tanımlıyoruz
                    daire DbSave = new daire();

                    //txtId alanı boşşa 0 olarak gönderiyoruz boş değilse değeri int dönüştürüp gönderiyoruz
                    //alan 0 olarak giderse yeni kayıt olacak değer giderse güncelleme olacak
                    DbSave.Id = txtId.Text == "" ? 0 : Convert.ToInt32(txtId.Text);

                    //formdaki değerleri atama yapıyoruz
                    DbSave.blokId = Convert.ToInt32(txtBlokId.Text);
                    DbSave.daireNo = txtDaire.Text;

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
                txtDaire.Text = "";
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //griddeki alan çift tıklandığı zaman birinci kolon değerini txtId textbox'a atıyoruz
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            var result = query.getDaire(Convert.ToInt32(txtId.Text)); // seçilen değeri veritabanından sorgulayarak gelen sonucu bir değişkene atıyoruz

            //gelend değerli formumuzdaki alanlara atıyoruz
            txtDaire.Text = result.daireNo;
        }
    }
}
