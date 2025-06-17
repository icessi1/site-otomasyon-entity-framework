
namespace siteYonetimi
{
    partial class frmMenu
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSite = new System.Windows.Forms.Button();
            this.frmParametre = new System.Windows.Forms.Button();
            this.btnKisi = new System.Windows.Forms.Button();
            this.btnDaireKisi = new System.Windows.Forms.Button();
            this.btnRapor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSite
            // 
            this.btnSite.Location = new System.Drawing.Point(22, 12);
            this.btnSite.Name = "btnSite";
            this.btnSite.Size = new System.Drawing.Size(185, 55);
            this.btnSite.TabIndex = 0;
            this.btnSite.Text = "Site İşlemleri";
            this.btnSite.UseVisualStyleBackColor = true;
            this.btnSite.Click += new System.EventHandler(this.btnSite_Click);
            // 
            // frmParametre
            // 
            this.frmParametre.Location = new System.Drawing.Point(22, 195);
            this.frmParametre.Name = "frmParametre";
            this.frmParametre.Size = new System.Drawing.Size(185, 55);
            this.frmParametre.TabIndex = 1;
            this.frmParametre.Text = "Parametre İşlemleri";
            this.frmParametre.UseVisualStyleBackColor = true;
            this.frmParametre.Click += new System.EventHandler(this.frmParametre_Click);
            // 
            // btnKisi
            // 
            this.btnKisi.Location = new System.Drawing.Point(22, 73);
            this.btnKisi.Name = "btnKisi";
            this.btnKisi.Size = new System.Drawing.Size(185, 55);
            this.btnKisi.TabIndex = 2;
            this.btnKisi.Text = "Kişi İşlemleri";
            this.btnKisi.UseVisualStyleBackColor = true;
            this.btnKisi.Click += new System.EventHandler(this.btnKisi_Click);
            // 
            // btnDaireKisi
            // 
            this.btnDaireKisi.Location = new System.Drawing.Point(22, 134);
            this.btnDaireKisi.Name = "btnDaireKisi";
            this.btnDaireKisi.Size = new System.Drawing.Size(185, 55);
            this.btnDaireKisi.TabIndex = 3;
            this.btnDaireKisi.Text = "Daire Kişi Atama";
            this.btnDaireKisi.UseVisualStyleBackColor = true;
            this.btnDaireKisi.Click += new System.EventHandler(this.btnDaireKisi_Click);
            // 
            // btnRapor
            // 
            this.btnRapor.Location = new System.Drawing.Point(22, 256);
            this.btnRapor.Name = "btnRapor";
            this.btnRapor.Size = new System.Drawing.Size(185, 55);
            this.btnRapor.TabIndex = 4;
            this.btnRapor.Text = "Raporlar";
            this.btnRapor.UseVisualStyleBackColor = true;
            this.btnRapor.Click += new System.EventHandler(this.btnRapor_Click);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 332);
            this.Controls.Add(this.btnRapor);
            this.Controls.Add(this.btnDaireKisi);
            this.Controls.Add(this.btnKisi);
            this.Controls.Add(this.frmParametre);
            this.Controls.Add(this.btnSite);
            this.Name = "frmMenu";
            this.Text = "Menü";
            this.Load += new System.EventHandler(this.frmMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSite;
        private System.Windows.Forms.Button frmParametre;
        private System.Windows.Forms.Button btnKisi;
        private System.Windows.Forms.Button btnDaireKisi;
        private System.Windows.Forms.Button btnRapor;
    }
}

