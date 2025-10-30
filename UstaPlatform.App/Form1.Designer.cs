using System;
using System.Drawing;
using System.Windows.Forms;

namespace UstaPlatform
{
    partial class Form1
    {
        
        ///  Required designer variable.
        
        private System.ComponentModel.IContainer components = null;

        
        ///  Clean up any resources being used.
        
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabMasters = new TabPage();
            tabCitizens = new TabPage();
            tabRequests = new TabPage();
            tabRules = new TabPage();

            panelMasterInput = new Panel();
            gridMasters = new DataGridView();
            txtMasterName = new TextBox();
            cmbMasterSpecialty = new ComboBox();
            numMasterRating = new NumericUpDown();
            btnAddMaster = new Button();
            lblMasterTitle = new Label();
            lblMasterNameLabel = new Label();
            lblMasterSpecialtyLabel = new Label();
            lblMasterRatingLabel = new Label();

            panelCitizenInput = new Panel();
            gridCitizens = new DataGridView();
            txtCitizenName = new TextBox();
            txtCitizenAddress = new TextBox();
            btnAddCitizen = new Button();
            lblCitizenTitle = new Label();
            lblCitizenNameLabel = new Label();
            lblCitizenAddressLabel = new Label();

            panelRequestInput = new Panel();
            panelMatchResult = new Panel();
            cmbReqCitizen = new ComboBox();
            cmbReqSpecialty = new ComboBox();
            txtReqDescription = new TextBox();
            dtReqDate = new DateTimePicker();
            chkReqEmergency = new CheckBox();
            btnMatchAndPrice = new Button();
            lblChosenMaster = new Label();
            lblCalculatedPrice = new Label();
            btnAssignWork = new Button();
            gridWorkOrders = new DataGridView();
            lblRequestTitle = new Label();
            lblReqCitizenLabel = new Label();
            lblReqSpecialtyLabel = new Label();
            lblReqDescLabel = new Label();
            lblReqDateLabel = new Label();

            panelRulesTop = new Panel();
            listRules = new ListBox();
            btnReloadRules = new Button();
            lblRulesTitle = new Label();

            SuspendLayout();

            // Form - Modern Tasarım
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 700);
            Text = "UstaPlatform - Arcadia Şehir Uzman Platformu";
            BackColor = Color.FromArgb(240, 242, 245);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            // TabControl - Modern Styling
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI Semibold", 10F);
            tabControl1.Padding = new Point(20, 8);
            tabControl1.Controls.Add(tabMasters);
            tabControl1.Controls.Add(tabCitizens);
            tabControl1.Controls.Add(tabRequests);
            tabControl1.Controls.Add(tabRules);

            // ============= USTALAR TAB =============
            tabMasters.Text = "👨‍🔧 Ustalar";
            tabMasters.BackColor = Color.FromArgb(240, 242, 245);
            tabMasters.Controls.Add(gridMasters);
            tabMasters.Controls.Add(panelMasterInput);

            // Panel for Master Input
            panelMasterInput.BackColor = Color.White;
            panelMasterInput.Location = new Point(15, 15);
            panelMasterInput.Size = new Size(1150, 160);
            panelMasterInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelMasterInput.Paint += (s, e) => {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(220, 223, 230), 1), 
                    0, 0, panelMasterInput.Width - 1, panelMasterInput.Height - 1);
            };
            panelMasterInput.Controls.Add(lblMasterTitle);
            panelMasterInput.Controls.Add(lblMasterNameLabel);
            panelMasterInput.Controls.Add(txtMasterName);
            panelMasterInput.Controls.Add(lblMasterSpecialtyLabel);
            panelMasterInput.Controls.Add(cmbMasterSpecialty);
            panelMasterInput.Controls.Add(lblMasterRatingLabel);
            panelMasterInput.Controls.Add(numMasterRating);
            panelMasterInput.Controls.Add(btnAddMaster);

            lblMasterTitle.Text = "Yeni Usta Kaydı";
            lblMasterTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblMasterTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblMasterTitle.Location = new Point(15, 15);
            lblMasterTitle.AutoSize = true;

            lblMasterNameLabel.Text = "Usta Adı:";
            lblMasterNameLabel.Location = new Point(20, 55);
            lblMasterNameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblMasterNameLabel.ForeColor = Color.FromArgb(52, 73, 94);
            lblMasterNameLabel.AutoSize = true;

            txtMasterName.Location = new Point(20, 80);
            txtMasterName.Width = 280;
            txtMasterName.Height = 35;
            txtMasterName.Font = new Font("Segoe UI", 10F);
            txtMasterName.PlaceholderText = "Örn: Ahmet Yılmaz";
            txtMasterName.BorderStyle = BorderStyle.FixedSingle;

            lblMasterSpecialtyLabel.Text = "Uzmanlık Alanı:";
            lblMasterSpecialtyLabel.Location = new Point(320, 55);
            lblMasterSpecialtyLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblMasterSpecialtyLabel.ForeColor = Color.FromArgb(52, 73, 94);
            lblMasterSpecialtyLabel.AutoSize = true;

            cmbMasterSpecialty.Location = new Point(320, 80);
            cmbMasterSpecialty.Width = 250;
            cmbMasterSpecialty.Height = 35;
            cmbMasterSpecialty.Font = new Font("Segoe UI", 10F);
            cmbMasterSpecialty.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMasterSpecialty.Items.AddRange(new object[] { "Tesisatçı", "Elektrikçi", "Marangoz" });

            lblMasterRatingLabel.Text = "Puan (0-5):";
            lblMasterRatingLabel.Location = new Point(590, 55);
            lblMasterRatingLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblMasterRatingLabel.ForeColor = Color.FromArgb(52, 73, 94);
            lblMasterRatingLabel.AutoSize = true;

            numMasterRating.Location = new Point(590, 80);
            numMasterRating.Width = 120;
            numMasterRating.Height = 35;
            numMasterRating.Font = new Font("Segoe UI", 10F);
            numMasterRating.DecimalPlaces = 1;
            numMasterRating.Minimum = 0;
            numMasterRating.Maximum = 5;
            numMasterRating.Increment = 0.1M;
            numMasterRating.Value = 4.5M;

            btnAddMaster.Location = new Point(730, 75);
            btnAddMaster.Size = new Size(180, 40);
            btnAddMaster.Text = "✓ Usta Ekle";
            btnAddMaster.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddMaster.BackColor = Color.FromArgb(46, 204, 113);
            btnAddMaster.ForeColor = Color.White;
            btnAddMaster.FlatStyle = FlatStyle.Flat;
            btnAddMaster.FlatAppearance.BorderSize = 0;
            btnAddMaster.Cursor = Cursors.Hand;
            btnAddMaster.Click += btnAddMaster_Click;
            btnAddMaster.MouseEnter += (s, e) => btnAddMaster.BackColor = Color.FromArgb(39, 174, 96);
            btnAddMaster.MouseLeave += (s, e) => btnAddMaster.BackColor = Color.FromArgb(46, 204, 113);

            gridMasters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridMasters.Location = new Point(15, 185);
            gridMasters.Size = new Size(1150, 430);
            gridMasters.ReadOnly = true;
            gridMasters.AllowUserToAddRows = false;
            gridMasters.AllowUserToDeleteRows = false;
            gridMasters.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridMasters.BackgroundColor = Color.White;
            gridMasters.BorderStyle = BorderStyle.FixedSingle;
            gridMasters.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            gridMasters.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridMasters.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gridMasters.ColumnHeadersHeight = 40;
            gridMasters.EnableHeadersVisualStyles = false;
            gridMasters.RowTemplate.Height = 35;
            gridMasters.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            gridMasters.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ============= VATANDAŞLAR TAB =============
            tabCitizens.Text = "👥 Vatandaşlar";
            tabCitizens.BackColor = Color.FromArgb(240, 242, 245);
            tabCitizens.Controls.Add(gridCitizens);
            tabCitizens.Controls.Add(panelCitizenInput);

            // Panel for Citizen Input
            panelCitizenInput.BackColor = Color.White;
            panelCitizenInput.Location = new Point(15, 15);
            panelCitizenInput.Size = new Size(1150, 160);
            panelCitizenInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelCitizenInput.Paint += (s, e) => {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(220, 223, 230), 1), 
                    0, 0, panelCitizenInput.Width - 1, panelCitizenInput.Height - 1);
            };
            panelCitizenInput.Controls.Add(lblCitizenTitle);
            panelCitizenInput.Controls.Add(lblCitizenNameLabel);
            panelCitizenInput.Controls.Add(txtCitizenName);
            panelCitizenInput.Controls.Add(lblCitizenAddressLabel);
            panelCitizenInput.Controls.Add(txtCitizenAddress);
            panelCitizenInput.Controls.Add(btnAddCitizen);

            lblCitizenTitle.Text = "Yeni Vatandaş Kaydı";
            lblCitizenTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCitizenTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblCitizenTitle.Location = new Point(15, 15);
            lblCitizenTitle.AutoSize = true;

            lblCitizenNameLabel.Text = "Ad Soyad:";
            lblCitizenNameLabel.Location = new Point(20, 55);
            lblCitizenNameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblCitizenNameLabel.ForeColor = Color.FromArgb(52, 73, 94);
            lblCitizenNameLabel.AutoSize = true;

            txtCitizenName.Location = new Point(20, 80);
            txtCitizenName.Width = 300;
            txtCitizenName.Height = 35;
            txtCitizenName.Font = new Font("Segoe UI", 10F);
            txtCitizenName.PlaceholderText = "Örn: Ayşe Demir";
            txtCitizenName.BorderStyle = BorderStyle.FixedSingle;

            lblCitizenAddressLabel.Text = "Adres:";
            lblCitizenAddressLabel.Location = new Point(340, 55);
            lblCitizenAddressLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblCitizenAddressLabel.ForeColor = Color.FromArgb(52, 73, 94);
            lblCitizenAddressLabel.AutoSize = true;

            txtCitizenAddress.Location = new Point(340, 80);
            txtCitizenAddress.Width = 450;
            txtCitizenAddress.Height = 35;
            txtCitizenAddress.Font = new Font("Segoe UI", 10F);
            txtCitizenAddress.PlaceholderText = "Örn: Atatürk Cad. No:123 Merkez/Arcadia";
            txtCitizenAddress.BorderStyle = BorderStyle.FixedSingle;

            btnAddCitizen.Location = new Point(810, 75);
            btnAddCitizen.Size = new Size(180, 40);
            btnAddCitizen.Text = "✓ Vatandaş Ekle";
            btnAddCitizen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddCitizen.BackColor = Color.FromArgb(52, 152, 219);
            btnAddCitizen.ForeColor = Color.White;
            btnAddCitizen.FlatStyle = FlatStyle.Flat;
            btnAddCitizen.FlatAppearance.BorderSize = 0;
            btnAddCitizen.Cursor = Cursors.Hand;
            btnAddCitizen.Click += btnAddCitizen_Click;
            btnAddCitizen.MouseEnter += (s, e) => btnAddCitizen.BackColor = Color.FromArgb(41, 128, 185);
            btnAddCitizen.MouseLeave += (s, e) => btnAddCitizen.BackColor = Color.FromArgb(52, 152, 219);

            gridCitizens.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridCitizens.Location = new Point(15, 185);
            gridCitizens.Size = new Size(1150, 430);
            gridCitizens.ReadOnly = true;
            gridCitizens.AllowUserToAddRows = false;
            gridCitizens.AllowUserToDeleteRows = false;
            gridCitizens.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridCitizens.BackgroundColor = Color.White;
            gridCitizens.BorderStyle = BorderStyle.FixedSingle;
            gridCitizens.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            gridCitizens.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridCitizens.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gridCitizens.ColumnHeadersHeight = 40;
            gridCitizens.EnableHeadersVisualStyles = false;
            gridCitizens.RowTemplate.Height = 35;
            gridCitizens.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            gridCitizens.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ============= TALEPLİR / İŞ PLANLAMA TAB =============
            tabRequests.Text = "📋 Talepler & İş Planlama";
            tabRequests.BackColor = Color.FromArgb(240, 242, 245);
            tabRequests.Controls.Add(gridWorkOrders);
            tabRequests.Controls.Add(panelRequestInput);
            tabRequests.Controls.Add(panelMatchResult);

            // Panel for Request Input
            panelRequestInput.BackColor = Color.White;
            panelRequestInput.Location = new Point(15, 15);
            panelRequestInput.Size = new Size(1150, 180);
            panelRequestInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelRequestInput.Paint += (s, e) => {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(220, 223, 230), 1), 
                    0, 0, panelRequestInput.Width - 1, panelRequestInput.Height - 1);
            };
            panelRequestInput.Controls.Add(lblRequestTitle);
            panelRequestInput.Controls.Add(lblReqCitizenLabel);
            panelRequestInput.Controls.Add(cmbReqCitizen);
            panelRequestInput.Controls.Add(lblReqSpecialtyLabel);
            panelRequestInput.Controls.Add(cmbReqSpecialty);
            panelRequestInput.Controls.Add(lblReqDescLabel);
            panelRequestInput.Controls.Add(txtReqDescription);
            panelRequestInput.Controls.Add(lblReqDateLabel);
            panelRequestInput.Controls.Add(dtReqDate);
            panelRequestInput.Controls.Add(chkReqEmergency);

            lblRequestTitle.Text = "Yeni Talep Oluştur";
            lblRequestTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRequestTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblRequestTitle.Location = new Point(15, 15);
            lblRequestTitle.AutoSize = true;

            lblReqCitizenLabel.Text = "Vatandaş:";
            lblReqCitizenLabel.Location = new Point(20, 55);
            lblReqCitizenLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblReqCitizenLabel.ForeColor = Color.FromArgb(52, 73, 94);
            lblReqCitizenLabel.AutoSize = true;

            cmbReqCitizen.Location = new Point(20, 80);
            cmbReqCitizen.Width = 250;
            cmbReqCitizen.Height = 35;
            cmbReqCitizen.Font = new Font("Segoe UI", 10F);
            cmbReqCitizen.DropDownStyle = ComboBoxStyle.DropDownList;

            lblReqSpecialtyLabel.Text = "Uzmanlık:";
            lblReqSpecialtyLabel.Location = new Point(290, 55);
            lblReqSpecialtyLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblReqSpecialtyLabel.ForeColor = Color.FromArgb(52, 73, 94);
            lblReqSpecialtyLabel.AutoSize = true;

            cmbReqSpecialty.Location = new Point(290, 80);
            cmbReqSpecialty.Width = 200;
            cmbReqSpecialty.Height = 35;
            cmbReqSpecialty.Font = new Font("Segoe UI", 10F);
            cmbReqSpecialty.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReqSpecialty.Items.AddRange(new object[] { "", "Tesisatçı", "Elektrikçi", "Marangoz" });

            lblReqDescLabel.Text = "Açıklama:";
            lblReqDescLabel.Location = new Point(20, 125);
            lblReqDescLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblReqDescLabel.ForeColor = Color.FromArgb(52, 73, 94);
            lblReqDescLabel.AutoSize = true;

            txtReqDescription.Location = new Point(120, 122);
            txtReqDescription.Width = 370;
            txtReqDescription.Height = 35;
            txtReqDescription.Font = new Font("Segoe UI", 10F);
            txtReqDescription.PlaceholderText = "Örn: Lavabo sızıntısı var, acilen bakılmalı";
            txtReqDescription.BorderStyle = BorderStyle.FixedSingle;

            lblReqDateLabel.Text = "Tarih:";
            lblReqDateLabel.Location = new Point(510, 55);
            lblReqDateLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblReqDateLabel.ForeColor = Color.FromArgb(52, 73, 94);
            lblReqDateLabel.AutoSize = true;

            dtReqDate.Location = new Point(510, 80);
            dtReqDate.Width = 220;
            dtReqDate.Height = 35;
            dtReqDate.Font = new Font("Segoe UI", 10F);

            chkReqEmergency.Location = new Point(750, 82);
            chkReqEmergency.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            chkReqEmergency.ForeColor = Color.FromArgb(231, 76, 60);
            chkReqEmergency.Text = "🚨 Acil";
            chkReqEmergency.AutoSize = true;

            // Panel for Match Result
            panelMatchResult.BackColor = Color.White;
            panelMatchResult.Location = new Point(15, 205);
            panelMatchResult.Size = new Size(1150, 100);
            panelMatchResult.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelMatchResult.Paint += (s, e) => {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(220, 223, 230), 1), 
                    0, 0, panelMatchResult.Width - 1, panelMatchResult.Height - 1);
            };
            panelMatchResult.Controls.Add(btnMatchAndPrice);
            panelMatchResult.Controls.Add(lblChosenMaster);
            panelMatchResult.Controls.Add(lblCalculatedPrice);
            panelMatchResult.Controls.Add(btnAssignWork);

            btnMatchAndPrice.Location = new Point(20, 30);
            btnMatchAndPrice.Size = new Size(200, 45);
            btnMatchAndPrice.Text = "🔍 Eşleştir ve Fiyatla";
            btnMatchAndPrice.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnMatchAndPrice.BackColor = Color.FromArgb(155, 89, 182);
            btnMatchAndPrice.ForeColor = Color.White;
            btnMatchAndPrice.FlatStyle = FlatStyle.Flat;
            btnMatchAndPrice.FlatAppearance.BorderSize = 0;
            btnMatchAndPrice.Cursor = Cursors.Hand;
            btnMatchAndPrice.Click += btnMatchAndPrice_Click;
            btnMatchAndPrice.MouseEnter += (s, e) => btnMatchAndPrice.BackColor = Color.FromArgb(142, 68, 173);
            btnMatchAndPrice.MouseLeave += (s, e) => btnMatchAndPrice.BackColor = Color.FromArgb(155, 89, 182);

            lblChosenMaster.Location = new Point(240, 30);
            lblChosenMaster.AutoSize = true;
            lblChosenMaster.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblChosenMaster.ForeColor = Color.FromArgb(39, 174, 96);
            lblChosenMaster.Text = "Usta: -";

            lblCalculatedPrice.Location = new Point(240, 55);
            lblCalculatedPrice.AutoSize = true;
            lblCalculatedPrice.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCalculatedPrice.ForeColor = Color.FromArgb(230, 126, 34);
            lblCalculatedPrice.Text = "Fiyat: -";

            btnAssignWork.Location = new Point(680, 25);
            btnAssignWork.Size = new Size(200, 50);
            btnAssignWork.Text = "✓ İşi Ata";
            btnAssignWork.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnAssignWork.BackColor = Color.FromArgb(231, 76, 60);
            btnAssignWork.ForeColor = Color.White;
            btnAssignWork.FlatStyle = FlatStyle.Flat;
            btnAssignWork.FlatAppearance.BorderSize = 0;
            btnAssignWork.Cursor = Cursors.Hand;
            btnAssignWork.Click += btnAssignWork_Click;
            btnAssignWork.MouseEnter += (s, e) => btnAssignWork.BackColor = Color.FromArgb(192, 57, 43);
            btnAssignWork.MouseLeave += (s, e) => btnAssignWork.BackColor = Color.FromArgb(231, 76, 60);

            gridWorkOrders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridWorkOrders.Location = new Point(15, 315);
            gridWorkOrders.Size = new Size(1150, 300);
            gridWorkOrders.ReadOnly = true;
            gridWorkOrders.AllowUserToAddRows = false;
            gridWorkOrders.AllowUserToDeleteRows = false;
            gridWorkOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridWorkOrders.BackgroundColor = Color.White;
            gridWorkOrders.BorderStyle = BorderStyle.FixedSingle;
            gridWorkOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            gridWorkOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridWorkOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gridWorkOrders.ColumnHeadersHeight = 40;
            gridWorkOrders.EnableHeadersVisualStyles = false;
            gridWorkOrders.RowTemplate.Height = 35;
            gridWorkOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            gridWorkOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ============= FİYAT KURALLARI TAB =============
            tabRules.Text = "⚙️ Fiyat Kuralları";
            tabRules.BackColor = Color.FromArgb(240, 242, 245);
            tabRules.Controls.Add(listRules);
            tabRules.Controls.Add(panelRulesTop);

            // Panel for Rules Top
            panelRulesTop.BackColor = Color.White;
            panelRulesTop.Location = new Point(15, 15);
            panelRulesTop.Size = new Size(1150, 80);
            panelRulesTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelRulesTop.Paint += (s, e) => {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(220, 223, 230), 1), 
                    0, 0, panelRulesTop.Width - 1, panelRulesTop.Height - 1);
            };
            panelRulesTop.Controls.Add(lblRulesTitle);
            panelRulesTop.Controls.Add(btnReloadRules);

            lblRulesTitle.Text = "Yüklü Fiyatlandırma Kuralları (Plug-in Sistemi)";
            lblRulesTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRulesTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblRulesTitle.Location = new Point(15, 15);
            lblRulesTitle.AutoSize = true;

            btnReloadRules.Location = new Point(15, 45);
            btnReloadRules.Size = new Size(220, 25);
            btnReloadRules.Text = "🔄 Kuralları Yeniden Yükle";
            btnReloadRules.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReloadRules.BackColor = Color.FromArgb(52, 152, 219);
            btnReloadRules.ForeColor = Color.White;
            btnReloadRules.FlatStyle = FlatStyle.Flat;
            btnReloadRules.FlatAppearance.BorderSize = 0;
            btnReloadRules.Cursor = Cursors.Hand;
            btnReloadRules.Click += btnReloadRules_Click;
            btnReloadRules.MouseEnter += (s, e) => btnReloadRules.BackColor = Color.FromArgb(41, 128, 185);
            btnReloadRules.MouseLeave += (s, e) => btnReloadRules.BackColor = Color.FromArgb(52, 152, 219);

            listRules.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listRules.Location = new Point(15, 105);
            listRules.Size = new Size(1150, 510);
            listRules.Font = new Font("Segoe UI", 10F);
            listRules.BackColor = Color.White;
            listRules.BorderStyle = BorderStyle.FixedSingle;
            listRules.ItemHeight = 30;

            // Add TabControl to Form
            Controls.Add(tabControl1);

            Name = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabMasters;
        private TabPage tabCitizens;
        private TabPage tabRequests;
        private TabPage tabRules;

        // Masters Tab Controls
        private Panel panelMasterInput;
        private DataGridView gridMasters;
        private TextBox txtMasterName;
        private ComboBox cmbMasterSpecialty;
        private NumericUpDown numMasterRating;
        private Button btnAddMaster;
        private Label lblMasterTitle;
        private Label lblMasterNameLabel;
        private Label lblMasterSpecialtyLabel;
        private Label lblMasterRatingLabel;

        // Citizens Tab Controls
        private Panel panelCitizenInput;
        private DataGridView gridCitizens;
        private TextBox txtCitizenName;
        private TextBox txtCitizenAddress;
        private Button btnAddCitizen;
        private Label lblCitizenTitle;
        private Label lblCitizenNameLabel;
        private Label lblCitizenAddressLabel;

        // Requests Tab Controls
        private Panel panelRequestInput;
        private Panel panelMatchResult;
        private ComboBox cmbReqCitizen;
        private ComboBox cmbReqSpecialty;
        private TextBox txtReqDescription;
        private DateTimePicker dtReqDate;
        private CheckBox chkReqEmergency;
        private Button btnMatchAndPrice;
        private Label lblChosenMaster;
        private Label lblCalculatedPrice;
        private Button btnAssignWork;
        private DataGridView gridWorkOrders;
        private Label lblRequestTitle;
        private Label lblReqCitizenLabel;
        private Label lblReqSpecialtyLabel;
        private Label lblReqDescLabel;
        private Label lblReqDateLabel;

        // Rules Tab Controls
        private Panel panelRulesTop;
        private ListBox listRules;
        private Button btnReloadRules;
        private Label lblRulesTitle;
    }
}
