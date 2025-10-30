using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using UstaPlatform.Domain;
using UstaPlatform.Helpers;
using UstaPlatform.Infrastructure;
using UstaPlatform.Pricing;
using UstaPlatform.Services;

namespace UstaPlatform
{

    /// Ana Form: Modern WinForms arayüzü ile Usta, Vatandaş, Talep ve Fiyatlama akışını yönetir.
    /// - BindingList kullanarak verilerin anlık yansıması sağlanır.
    /// - PricingEngine, eklenti (plug-in) kurallarını yükleyebilir.

    public partial class Form1 : Form
    {
        // Bellek içi veri kaynakları - BindingList ile anlık güncelleme
        private readonly BindingList<Master> _masters = new();
        private readonly BindingList<Citizen> _citizens = new();
        private readonly BindingList<WorkOrder> _orders = new();

        // Servisler
        private readonly MatchingService _matching = new();
        private readonly PricingEngine _pricing = new();
        private readonly InMemoryWorkOrderRepository _repo = new();

        // Seçilen usta ve hesaplanan fiyatın UI üzerinde tutulması
        private Master? _selectedMaster;
        private decimal _lastPrice;

        public Form1()
        {
            InitializeComponent();
            InitializeBindings();
            InitializePricing();
            LoadSampleData();
        }

    
        /// Başlangıçta grid ve combobox veri bağlama ayarları.
        /// BindingList kullanarak anlık veri yansıması sağlanır.
    
        private void InitializeBindings()
        {
            // DataGridView'lere BindingList bağlama
            gridMasters.DataSource = _masters;
            gridCitizens.DataSource = _citizens;
            gridWorkOrders.DataSource = _orders;

            // ComboBox için BindingList
            cmbReqCitizen.DisplayMember = nameof(Citizen.Name);
            cmbReqCitizen.ValueMember = nameof(Citizen.Id);
            cmbReqCitizen.DataSource = _citizens;

            // Grid sütun ayarları
            ConfigureGridColumns();
        }

    
        /// DataGridView sütunlarını Türkçeleştirir ve özelleştirir
    
        private void ConfigureGridColumns()
        {
            // Ustalar Grid
            gridMasters.AutoGenerateColumns = false;
            gridMasters.Columns.Clear();
            gridMasters.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Master.Name),
                HeaderText = "Usta Adı",
                Width = 150
            });
            gridMasters.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Master.Specialty),
                HeaderText = "Uzmanlık",
                Width = 120
            });
            gridMasters.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Master.Rating),
                HeaderText = "Puan",
                Width = 60
            });
            gridMasters.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Master.Load),
                HeaderText = "İş Yükü",
                Width = 80
            });

            // Vatandaşlar Grid
            gridCitizens.AutoGenerateColumns = false;
            gridCitizens.Columns.Clear();
            gridCitizens.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Citizen.Name),
                HeaderText = "Vatandaş Adı",
                Width = 150
            });
            gridCitizens.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Citizen.Address),
                HeaderText = "Adres",
                Width = 300
            });

            // İş Emirleri Grid - Özel gösterim için
            gridWorkOrders.AutoGenerateColumns = false;
            gridWorkOrders.Columns.Clear();
            gridWorkOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Usta",
                Width = 120,
                Name = "colMaster"
            });
            gridWorkOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Vatandaş",
                Width = 120,
                Name = "colCitizen"
            });
            gridWorkOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(WorkOrder.Description),
                HeaderText = "Açıklama",
                Width = 200
            });
            gridWorkOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(WorkOrder.Address),
                HeaderText = "Adres",
                Width = 250
            });
            gridWorkOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(WorkOrder.TotalPrice),
                HeaderText = "Fiyat (TL)",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });
            gridWorkOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(WorkOrder.Day),
                HeaderText = "Tarih",
                Width = 100
            });

            // WorkOrder için özel gösterim - CellFormatting event'i
            gridWorkOrders.CellFormatting += GridWorkOrders_CellFormatting;
        }

    
        /// WorkOrder grid'inde Master ve Citizen isimlerini göstermek için
    
        private void GridWorkOrders_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (gridWorkOrders.Rows[e.RowIndex].DataBoundItem is WorkOrder order)
            {
                switch (gridWorkOrders.Columns[e.ColumnIndex].Name)
                {
                    case "colMaster":
                        e.Value = order.Master?.Name ?? "-";
                        e.FormattingApplied = true;
                        break;
                    case "colCitizen":
                        e.Value = order.Citizen?.Name ?? "-";
                        e.FormattingApplied = true;
                        break;
                }
            }
        }

    
        /// Demo amaçlı başlangıç verileri yüklenir.
    
        private void LoadSampleData()
        {
            // Örnek Ustalar
            _masters.Add(new Master { Name = "Ahmet Yılmaz", Specialty = "Tesisatçı", Rating = 4.8m });
            _masters.Add(new Master { Name = "Mehmet Kaya", Specialty = "Elektrikçi", Rating = 4.5m });
            _masters.Add(new Master { Name = "Ali Demir", Specialty = "Marangoz", Rating = 4.9m });
            _masters.Add(new Master { Name = "Ayşe Şahin", Specialty = "Tesisatçı", Rating = 4.7m });
            _masters.Add(new Master { Name = "Fatma Özkan", Specialty = "Elektrikçi", Rating = 4.6m });

            // Örnek Vatandaşlar
            _citizens.Add(new Citizen { Name = "Can Öztürk", Address = "Atatürk Cad. No:45 Merkez/Arcadia" });
            _citizens.Add(new Citizen { Name = "Zeynep Aydın", Address = "İnönü Sok. No:12 Çamlıca/Arcadia" });
            _citizens.Add(new Citizen { Name = "Burak Yıldız", Address = "Cumhuriyet Bulvarı No:78 Yenimahalle/Arcadia" });
            _citizens.Add(new Citizen { Name = "Elif Arslan", Address = "Gazi Cad. No:23 Bahçelievler/Arcadia" });
        }

    
        /// Fiyatlama kurallarının yüklenmesi ve UI güncellenmesi.
    
        private void InitializePricing()
        {
            _pricing.ReloadRules();
            RefreshRulesList();
        }

        private void RefreshRulesList()
        {
            listRules.Items.Clear();
            foreach (var r in _pricing.Rules)
            {
                listRules.Items.Add(r.Name);
            }
        }

    
        /// Usta ekleme buton olayı.
        /// BindingList kullanıldığı için otomatik olarak grid güncellenir.
    
        private void btnAddMaster_Click(object? sender, EventArgs e)
        {
            var name = txtMasterName.Text?.Trim() ?? string.Empty;
            var sp = cmbMasterSpecialty.SelectedItem?.ToString() ?? string.Empty;
            var rating = (decimal)numMasterRating.Value;

            try
            {
                Guard.NotNullOrWhiteSpace(name, nameof(name));
                Guard.NotNullOrWhiteSpace(sp, nameof(sp));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Doğrulama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var m = new Master
            {
                Name = name,
                Specialty = sp,
                Rating = rating
            };
            _masters.Add(m);  // BindingList otomatik günceller

            // Form temizliği
            txtMasterName.Clear();
            cmbMasterSpecialty.SelectedIndex = -1;
            numMasterRating.Value = 4.5m;

            // Talep ekranındaki uzmanlık için de veri sağlayabilir
            if (!cmbReqSpecialty.Items.Contains(sp))
                cmbReqSpecialty.Items.Add(sp);

            MessageBox.Show($"✓ {name} başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    
        /// Vatandaş ekleme buton olayı.
        /// BindingList kullanıldığı için otomatik olarak grid ve combobox güncellenir.
    
        private void btnAddCitizen_Click(object? sender, EventArgs e)
        {
            var name = txtCitizenName.Text?.Trim() ?? string.Empty;
            var addr = txtCitizenAddress.Text?.Trim() ?? string.Empty;

            try
            {
                Guard.NotNullOrWhiteSpace(name, nameof(name));
                Guard.NotNullOrWhiteSpace(addr, nameof(addr));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Doğrulama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var c = new Citizen { Name = name, Address = addr };
            _citizens.Add(c);  // BindingList otomatik günceller

            // Form temizliği
            txtCitizenName.Clear();
            txtCitizenAddress.Clear();

            MessageBox.Show($"✓ {name} başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    
        /// Eşleştir ve fiyatla buton olayı: Vatandaş ve uzmanlık seçimine göre usta bulup fiyat hesaplar.
    
        private void btnMatchAndPrice_Click(object? sender, EventArgs e)
        {
            if (cmbReqCitizen.SelectedItem is not Citizen citizen)
            {
                MessageBox.Show("⚠️ Lütfen bir vatandaş seçin.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var specialty = cmbReqSpecialty.SelectedItem?.ToString() ?? string.Empty;
            var desc = txtReqDescription.Text?.Trim() ?? string.Empty;
            var day = DateOnly.FromDateTime(dtReqDate.Value.Date);
            var isEmergency = chkReqEmergency.Checked;

            if (string.IsNullOrWhiteSpace(desc))
            {
                MessageBox.Show("⚠️ Lütfen bir açıklama girin.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var req = new Request
            {
                Citizen = citizen,
                Description = desc,
                DesiredSpecialty = specialty,
                Address = citizen.Address,
                RequestedDate = day,
                IsEmergency = isEmergency
            };

            var match = _matching.Match(req, _masters.ToList());
            if (match is null)
            {
                MessageBox.Show("❌ Uygun usta bulunamadı. Lütfen başka bir tarih veya uzmanlık deneyin.", 
                    "Eşleştirme Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Geçici iş emri oluşturup fiyat hesapla (atanmadan önce)
            var tempOrder = new WorkOrder
            {
                Citizen = citizen,
                Master = match,
                Description = isEmergency ? $"[ACIL] {desc}" : desc,
                Address = citizen.Address,
                Day = day
            };

            _selectedMaster = match;
            _lastPrice = _pricing.Calculate(tempOrder);

            lblChosenMaster.Text = $"👨‍🔧 Usta: {match.Name} ({match.Specialty}) - Puan: {match.Rating:F1}⭐";
            lblCalculatedPrice.Text = $"💰 Fiyat: {MoneyFormatter.FormatTL(_lastPrice)}";
        }

    
        /// İşi Ata butonu: Seçilen usta ile iş emri oluşturur ve çizelgeye ekler.
        /// BindingList kullanıldığı için otomatik olarak grid güncellenir.
    
        private void btnAssignWork_Click(object? sender, EventArgs e)
        {
            if (_selectedMaster is null)
            {
                MessageBox.Show("⚠️ Önce 'Eşleştir ve Fiyatla' işlemini yapın.", "İşlem Sırası Hatası", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbReqCitizen.SelectedItem is not Citizen citizen)
            {
                MessageBox.Show("⚠️ Lütfen bir vatandaş seçin.", "Eksik Bilgi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var desc = txtReqDescription.Text?.Trim() ?? string.Empty;
            var isEmergency = chkReqEmergency.Checked;
            var day = DateOnly.FromDateTime(dtReqDate.Value.Date);

            var order = new WorkOrder
            {
                Citizen = citizen,
                Master = _selectedMaster,
                Description = isEmergency ? $"[ACIL] {desc}" : desc,
                Address = citizen.Address,
                Day = day,
                TotalPrice = _lastPrice
            };

            // Çizelgeye ekle
            _selectedMaster.Schedule[day].Add(order);
            _orders.Add(order);  // BindingList otomatik günceller
            _repo.Add(order);

            // UI temizliği
            txtReqDescription.Clear();
            chkReqEmergency.Checked = false;
            _selectedMaster = null;
            _lastPrice = 0m;
            lblChosenMaster.Text = "👨‍🔧 Usta: -";
            lblCalculatedPrice.Text = "💰 Fiyat: -";

            MessageBox.Show($"✓ İş emri başarıyla oluşturuldu ve atandı!\n\n" +
                          $"Vatandaş: {citizen.Name}\n" +
                          $"İş: {desc}\n" +
                          $"Tarih: {day:dd.MM.yyyy}\n" +
                          $"Toplam Ücret: {MoneyFormatter.FormatTL(_lastPrice)}", 
                          "İş Atandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    
        /// Kuralları Yeniden Yükle butonu: Plugins klasörünü tarar ve kuralları tazeler.
    
        private void btnReloadRules_Click(object? sender, EventArgs e)
        {
            _pricing.ReloadRules();
            RefreshRulesList();
            MessageBox.Show($"✓ {_pricing.Rules.Count} adet fiyatlama kuralı yüklendi.\n\n" +
                          "Plugins klasörüne yeni DLL ekleyerek dinamik kurallar ekleyebilirsiniz!", 
                          "Kurallar Güncellendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
