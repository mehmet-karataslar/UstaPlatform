# 📚 UstaPlatform - Detaylı Proje Dokümantasyonu

> **Amaç:** Bu dokümanda projedeki her dosya, her sınıf ve her kod parçası detaylıca açıklanmıştır.

---

## 📑 İçindekiler

1. [Projeye Genel Bakış](#projeye-genel-bakış)
2. [Domain Klasörü - Model Sınıfları](#domain-klasörü)
3. [Pricing Klasörü - Fiyatlama Sistemi](#pricing-klasörü)
4. [Services Klasörü - İş Mantığı](#services-klasörü)
5. [Infrastructure Klasörü - Veri Yönetimi](#infrastructure-klasörü)
6. [Helpers Klasörü - Yardımcı Araçlar](#helpers-klasörü)
7. [Form1.cs - Kullanıcı Arayüzü](#form1cs---kullanıcı-arayüzü)
8. [SamplePlugins - Örnek Eklentiler](#sampleplugins-klasörü)
9. [Kod Örnekleri ve Açıklamalar](#kod-örnekleri)

---

## Projeye Genel Bakış

### 🎯 Proje Ne İşe Yarar?

**UstaPlatform**, bir şehirde yaşayan ustaları (tesisatçı, elektrikçi vb.) ve vatandaşları eşleştiren bir platformdur.

**Temel İşleyiş:**
1. Vatandaş bir iş talebi oluşturur (örn: "Elektrikçi lazım")
2. Sistem uygun ustayı bulur (müsait, yakın, doğru uzmanlıkta)
3. Dinamik fiyat hesaplanır (hafta sonu, acil durum vb.)
4. İş ustaya atanır

---

## Domain Klasörü

Domain klasörü, projenin **temel veri modellerini** içerir. Bunlar gerçek dünyadan iş kavramlarını kod olarak temsil eder.

### 📄 Master.cs - Usta Sınıfı

**Amaç:** Bir ustanın tüm bilgilerini tutar.

```csharp
public sealed class Master
{
    // GUID: Benzersiz kimlik numarası (otomatik oluşur)
    public Guid Id { get; init; } = Guid.NewGuid();
    
    // Usta adı
    public string Name { get; set; } = string.Empty;
    
    // Uzmanlık alanı (Tesisatçı, Elektrikçi vb.)
    public string Specialty { get; set; } = string.Empty;
    
    // Telefon numarası
    public string Phone { get; set; } = string.Empty;
    
    // Ev adresi (konum)
    public Location Home { get; init; } = new();
    
    // Puan (0-5 arası)
    public decimal Rating { get; set; }
    
    // Kayıt tarihi (otomatik oluşur)
    public DateTime RegisteredAt { get; init; } = DateTime.UtcNow;
    
    // Usta planı (çizelge)
    public Schedule Schedule { get; } = new();
    
    // Bugünkü iş sayısı (anlık hesaplanır)
    public int TodayWorkload => Schedule[DateOnly.FromDateTime(DateTime.Today)].Count;
}
```

**Önemli Noktalar:**

1. **`init` Keyword:** 
   - `Id`, `RegisteredAt`, `Home` sadece nesne oluşturulurken değer alabilir
   - Sonradan değiştirilemez (immutability)
   
2. **`sealed` Keyword:** 
   - Bu sınıftan başka sınıf türetilemez
   
3. **`TodayWorkload` Property:**
   - Getter-only (sadece okunabilir)
   - Bugünkü iş sayısını dinamik hesaplar

**Kullanım Örneği:**

```csharp
var usta = new Master
{
    Name = "Ahmet Yılmaz",
    Specialty = "Tesisatçı",
    Phone = "0555 123 4567",
    Rating = 4.8m,
    Home = new Location { X = 10, Y = 20 }
};

// usta.Id otomatik oluşturuldu (örn: a3b2c1d4-...)
// usta.RegisteredAt otomatik atandı (şu anki zaman)
```

---

### 📄 Citizen.cs - Vatandaş Sınıfı

**Amaç:** Talep oluşturan vatandaşların bilgilerini tutar.

```csharp
public sealed class Citizen
{
    // Benzersiz kimlik
    public Guid Id { get; init; } = Guid.NewGuid();
    
    // Vatandaş adı
    public string Name { get; set; } = string.Empty;
    
    // Ev adresi
    public Location Address { get; init; } = new();
    
    // Kayıt tarihi
    public DateTime RegisteredAt { get; init; } = DateTime.UtcNow;
}
```

**Basit Açıklama:**
- Vatandaş, talep oluşturan kişidir
- Sadece temel bilgileri tutar (ad, adres)
- `Location` (konum) sayesinde en yakın ustayı bulabiliriz

---

### 📄 Request.cs - Talep Sınıfı

**Amaç:** Vatandaşın oluşturduğu iş talebini temsil eder.

```csharp
public sealed class Request
{
    // Benzersiz kimlik
    public Guid Id { get; init; } = Guid.NewGuid();
    
    // Talep sahibi (hangi vatandaş?)
    public Citizen Citizen { get; init; } = null!;
    
    // İstenen uzmanlık (Tesisatçı, Elektrikçi vb.)
    public string RequiredSpecialty { get; set; } = string.Empty;
    
    // Açıklama (sorun nedir?)
    public string Description { get; set; } = string.Empty;
    
    // Acil mi? (true: acil, false: normal)
    public bool IsUrgent { get; set; }
    
    // Talep oluşturulma zamanı
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
```

**Kullanım Örneği:**

```csharp
var talep = new Request
{
    Citizen = mehmetVatandas,
    RequiredSpecialty = "Tesisatçı",
    Description = "Mutfak lavabosunda sızıntı var",
    IsUrgent = true  // Acil!
};
```

**Neden `null!` Kullanılıyor?**
- `Citizen` başta boş olamaz ama constructor'da set edeceğiz
- `null!` derleyiciye "endişelenme, değer gelecek" der

---

### 📄 WorkOrder.cs - İş Emri Sınıfı

**Amaç:** Ustaya atanmış bir işi temsil eder.

```csharp
public sealed class WorkOrder
{
    // Benzersiz kimlik
    public Guid Id { get; init; } = Guid.NewGuid();
    
    // Hangi usta yapacak?
    public Master Master { get; init; } = null!;
    
    // Hangi talep için?
    public Request Request { get; init; } = null!;
    
    // Ücret (hesaplanacak)
    public decimal Price { get; set; }
    
    // İşin tarihi
    public DateOnly ScheduledDate { get; set; }
    
    // Oluşturulma zamanı
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
```

**Basit Açıklama:**
- `Request` (talep) + `Master` (usta) = `WorkOrder` (iş emri)
- Fiyat dinamik hesaplanır (hafta sonu, acil durum vb.)
- `DateOnly`: Sadece tarih tutar (saat yok)

**Örnek:**

```csharp
var isEmri = new WorkOrder
{
    Master = ahmetUsta,
    Request = talep,
    Price = 500m,  // 500 TL
    ScheduledDate = DateOnly.Today
};
```

---

### 📄 Schedule.cs - Çizelge Sınıfı (Indexer Örneği)

**Amaç:** Ustanın günlük iş programını tutar.

```csharp
public sealed class Schedule
{
    // Veri: Gün -> İş listesi
    private readonly Dictionary<DateOnly, List<WorkOrder>> _schedule = new();
    
    // INDEXER: Array gibi erişim sağlar
    public List<WorkOrder> this[DateOnly day]
    {
        get
        {
            // Eğer o gün için iş yoksa, boş liste oluştur
            if (!_schedule.TryGetValue(day, out var orders))
            {
                orders = new List<WorkOrder>();
                _schedule[day] = orders;
            }
            return orders;
        }
    }
    
    // Tüm günleri döndür
    public IEnumerable<DateOnly> Days => _schedule.Keys;
    
    // Tüm işleri döndür
    public IEnumerable<WorkOrder> AllWorkOrders 
        => _schedule.Values.SelectMany(x => x);
}
```

**Indexer Nedir?**

Normal kullanım:
```csharp
schedule.GetWorkOrders(bugün)  // ❌ Uzun
```

Indexer ile:
```csharp
schedule[bugün]  // ✅ Kısa ve anlaşılır (array gibi)
```

**Kullanım Örneği:**

```csharp
var usta = new Master { Name = "Ali" };

// Bugün için iş ekle
usta.Schedule[DateOnly.Today].Add(isEmri1);

// Yarın için iş ekle
usta.Schedule[DateOnly.Today.AddDays(1)].Add(isEmri2);

// Bugün kaç iş var?
int bugunkuIsSayisi = usta.Schedule[DateOnly.Today].Count;
```

---

### 📄 Route.cs - Rota Sınıfı (IEnumerable Örneği)

**Amaç:** Ustanın günlük rotasını (gideceği yerleri) tutar.

```csharp
public sealed class Route : IEnumerable<(int X, int Y)>
{
    // Duraklar (koordinatlar)
    private readonly List<(int X, int Y)> _stops = new();
    
    // Durak ekleme
    public void Add(int x, int y) => _stops.Add((x, y));
    
    // Durak sayısı
    public int Count => _stops.Count;
    
    // Toplam mesafe hesaplama
    public double TotalDistance()
    {
        double total = 0;
        for (int i = 0; i < _stops.Count - 1; i++)
        {
            var from = _stops[i];
            var to = _stops[i + 1];
            total += Math.Sqrt(
                Math.Pow(to.X - from.X, 2) + 
                Math.Pow(to.Y - from.Y, 2)
            );
        }
        return total;
    }
    
    // IEnumerable implementasyonu (foreach için)
    public IEnumerator<(int X, int Y)> GetEnumerator() 
        => _stops.GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() 
        => GetEnumerator();
}
```

**IEnumerable Neden Önemli?**
- `foreach` döngüsü kullanabilmek için gerekli
- Collection initializer (kısa yazım) sağlar

**Kullanım Örneği:**

```csharp
// Collection initializer ile oluşturma
var rota = new Route
{
    { 0, 0 },    // Başlangıç
    { 10, 20 },  // 1. Durak
    { 30, 40 }   // 2. Durak
};

// foreach ile gezme
foreach (var (x, y) in rota)
{
    Console.WriteLine($"Konum: X={x}, Y={y}");
}

// Toplam mesafe
double mesafe = rota.TotalDistance();
```

---

### 📄 Location.cs (Yardımcı Sınıf)

```csharp
public sealed class Location
{
    public int X { get; set; }
    public int Y { get; set; }
}
```

**Basit Açıklama:**
- 2D koordinat sistemi (X, Y)
- Haritadaki konumu temsil eder
- Mesafe hesaplamaları için kullanılır

---

## Pricing Klasörü

Fiyatlama sistemi, **Plug-in Mimarisi** kullanır. Yeni kurallar kod değiştirmeden eklenebilir.

### 📄 IPricingRule.cs - Fiyatlandırma Kuralı Arayüzü

```csharp
public interface IPricingRule
{
    // Kuralın adı (örn: "Hafta Sonu Ek Ücreti")
    string Name { get; }
    
    // Fiyatı hesapla
    // order: İş emri
    // currentPrice: Şu anki fiyat
    // DÖNER: Yeni fiyat
    decimal Apply(WorkOrder order, decimal currentPrice);
}
```

**Neden Interface?**
- Yeni kurallar bu arayüzü uygular
- PricingEngine her kuralı aynı şekilde çağırabilir
- **Open/Closed Prensibi**: Kod değiştirmeden genişletilebilir

---

### 📄 PricingEngine.cs - Fiyatlandırma Motoru

**Amaç:** 
1. Tüm fiyat kurallarını yönetir
2. Plugin'leri (DLL) yükler
3. Fiyat hesaplar

```csharp
public sealed class PricingEngine
{
    // Tüm kuralların listesi
    private readonly List<IPricingRule> _rules = new();
    
    // Kuralları yükle
    public void LoadRules()
    {
        _rules.Clear();
        
        // 1. Dahili kuralları ekle
        _rules.Add(new WeekendSurchargeRule());
        _rules.Add(new EmergencySurchargeRule());
        
        // 2. Plugin'leri yükle
        LoadPlugins();
    }
    
    // Plugin'leri (DLL) yükle
    private void LoadPlugins()
    {
        string pluginsPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, 
            "Plugins"
        );
        
        // Klasör yoksa oluştur
        if (!Directory.Exists(pluginsPath))
        {
            Directory.CreateDirectory(pluginsPath);
            return;
        }
        
        // Tüm DLL dosyalarını bul
        var dllFiles = Directory.GetFiles(pluginsPath, "*.dll");
        
        foreach (var dllPath in dllFiles)
        {
            try
            {
                // DLL'i yükle
                Assembly assembly = Assembly.LoadFrom(dllPath);
                
                // IPricingRule'u uygulayan tipleri bul
                var ruleTypes = assembly.GetTypes()
                    .Where(t => typeof(IPricingRule).IsAssignableFrom(t) 
                             && !t.IsInterface 
                             && !t.IsAbstract);
                
                // Her tip için instance oluştur
                foreach (var type in ruleTypes)
                {
                    var rule = (IPricingRule)Activator.CreateInstance(type)!;
                    _rules.Add(rule);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Plugin yükleme hatası: {ex.Message}");
            }
        }
    }
    
    // Fiyat hesapla
    public decimal CalculatePrice(WorkOrder order, decimal basePrice)
    {
        decimal price = basePrice;
        
        // Her kuralı sırayla uygula
        foreach (var rule in _rules)
        {
            price = rule.Apply(order, price);
        }
        
        return price;
    }
    
    // Tüm kuralları getir
    public IReadOnlyList<IPricingRule> GetRules() => _rules.AsReadOnly();
}
```

**Adım Adım Açıklama:**

1. **LoadRules() Metodu:**
   ```csharp
   public void LoadRules()
   {
       _rules.Clear();  // Eski kuralları temizle
       _rules.Add(new WeekendSurchargeRule());  // Dahili kural 1
       _rules.Add(new EmergencySurchargeRule()); // Dahili kural 2
       LoadPlugins();  // Plugin'leri yükle
   }
   ```

2. **LoadPlugins() Metodu:**
   - `Plugins/` klasörünü tara
   - Tüm `.dll` dosyalarını bul
   - Her DLL'i yükle ve `IPricingRule` uygulayan sınıfları bul
   - Instance oluştur ve listeye ekle

3. **CalculatePrice() Metodu:**
   ```csharp
   decimal price = 300;  // Başlangıç fiyatı
   
   // 1. Kural (Hafta sonu): 300 * 1.15 = 345
   price = rule1.Apply(order, price);
   
   // 2. Kural (Acil): 345 + 200 = 545
   price = rule2.Apply(order, price);
   
   // Final: 545 TL
   return price;
   ```

---

### Dahili Kurallar

#### WeekendSurchargeRule - Hafta Sonu Ek Ücreti

```csharp
internal class WeekendSurchargeRule : IPricingRule
{
    public string Name => "🌅 Hafta Sonu Ek Ücreti (%15)";
    
    public decimal Apply(WorkOrder order, decimal currentPrice)
    {
        var date = order.ScheduledDate;
        var dayOfWeek = date.DayOfWeek;
        
        // Cumartesi veya Pazar ise %15 ekle
        if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
        {
            return currentPrice * 1.15m;  // %15 artış
        }
        
        return currentPrice;  // Değişiklik yok
    }
}
```

**Basit Açıklama:**
- Cumartesi veya Pazar günü işse → %15 ek ücret
- Hafta içi işse → Fiyat değişmez

**Örnek:**
```csharp
// Cumartesi günü iş
// Başlangıç: 300 TL
// %15 artış: 300 * 1.15 = 345 TL
```

---

#### EmergencySurchargeRule - Acil Çağrı Ücreti

```csharp
internal class EmergencySurchargeRule : IPricingRule
{
    public string Name => "🚨 Acil Çağrı Ek Ücreti (+200 TL)";
    
    public decimal Apply(WorkOrder order, decimal currentPrice)
    {
        // Acil işse +200 TL ekle
        if (order.Request.IsUrgent)
        {
            return currentPrice + 200m;
        }
        
        return currentPrice;
    }
}
```

**Basit Açıklama:**
- Acil işse → +200 TL sabit ücret
- Normal işse → Fiyat değişmez

---

## Services Klasörü

### 📄 MatchingService.cs - Eşleştirme Servisi

**Amaç:** Bir talep için en uygun ustayı bulur.

```csharp
public sealed class MatchingService
{
    public Master? FindBestMaster(Request request, List<Master> masters)
    {
        // İstenen uzmanlığa sahip ustaları filtrele
        var suitableMasters = masters
            .Where(m => m.Specialty == request.RequiredSpecialty)
            .ToList();
        
        // Hiç uygun usta yoksa null döner
        if (!suitableMasters.Any())
            return null;
        
        // En az yüklü ustayı bul
        var bestMaster = suitableMasters
            .OrderBy(m => m.TodayWorkload)  // İş yüküne göre sırala
            .ThenBy(m => CalculateDistance(  // Eşitse mesafeye bak
                m.Home, 
                request.Citizen.Address))
            .First();
        
        return bestMaster;
    }
    
    // İki konum arası mesafe hesapla
    private static double CalculateDistance(Location from, Location to)
    {
        int dx = to.X - from.X;
        int dy = to.Y - from.Y;
        return Math.Sqrt(dx * dx + dy * dy);  // Öklid mesafesi
    }
}
```

**Eşleştirme Algoritması:**

1. **Filtreleme:**
   ```csharp
   // Örnek: "Elektrikçi" arayan talep
   var uygunlar = ustalar.Where(u => u.Specialty == "Elektrikçi");
   ```

2. **Sıralama:**
   - Önce: En az iş yükü (bugün kaç işi var?)
   - Sonra: En yakın mesafe

3. **Seçim:**
   - İlk sıradaki usta (en uygun)

**Örnek Senaryo:**

```
Talep: Elektrikçi

Ustalar:
- Ali (Elektrikçi): 3 iş, 10 birim uzakta
- Mehmet (Elektrikçi): 1 iş, 20 birim uzakta
- Ayşe (Tesisatçı): 0 iş, 5 birim uzakta

Sonuç: Mehmet seçilir
Neden? Ayşe tesisatçı (elenmiş), Mehmet'in daha az işi var
```

---

## Infrastructure Klasörü

### 📄 InMemoryWorkOrderRepository.cs - Veri Deposu

**Amaç:** İş emirlerini bellekte tutar (veritabanı yerine).

```csharp
public sealed class InMemoryWorkOrderRepository
{
    // Tüm iş emirleri burada
    private readonly List<WorkOrder> _workOrders = new();
    
    // Yeni iş emri ekle
    public void Add(WorkOrder workOrder)
    {
        _workOrders.Add(workOrder);
    }
    
    // Tüm iş emirlerini getir
    public List<WorkOrder> GetAll() => _workOrders.ToList();
    
    // ID'ye göre iş emri bul
    public WorkOrder? GetById(Guid id) 
        => _workOrders.FirstOrDefault(w => w.Id == id);
    
    // Belirli ustanın iş emirlerini getir
    public List<WorkOrder> GetByMaster(Master master)
        => _workOrders
            .Where(w => w.Master.Id == master.Id)
            .ToList();
    
    // Tümünü sil
    public void Clear() => _workOrders.Clear();
}
```

**Basit Açıklama:**
- `List<WorkOrder>`: Veritabanı yerine RAM'de tutuluyor
- Uygulama kapanınca veriler silinir
- Test ve geliştirme için pratik

**Repository Pattern Nedir?**
- Veri erişim mantığını soyutlar
- İleride veritabanına geçmek kolay olur
- Sadece metodları değiştirirsin

---

## Helpers Klasörü

### 📄 Guard.cs - Doğrulama Yardımcısı

**Amaç:** Giriş kontrollerini tek yerden yapar.

```csharp
public static class Guard
{
    // String boş mu kontrol et
    public static void NotNullOrWhiteSpace(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                $"{paramName} boş olamaz.", 
                paramName
            );
        }
    }
    
    // Nesne null mu kontrol et
    public static void NotNull<T>(T value, string paramName) 
        where T : class
    {
        if (value == null)
        {
            throw new ArgumentNullException(paramName);
        }
    }
}
```

**Kullanım:**

```csharp
public void UstaEkle(string ad, string uzmanlik)
{
    // Önce kontrol et
    Guard.NotNullOrWhiteSpace(ad, nameof(ad));
    Guard.NotNullOrWhiteSpace(uzmanlik, nameof(uzmanlik));
    
    // Sonra işlem yap
    var usta = new Master { Name = ad, Specialty = uzmanlik };
}
```

**Fayda:**
- Kod tekrarını önler
- Her yerde aynı kontrol mantığı
- Hata mesajları tutarlı

---

### 📄 MoneyFormatter.cs - Para Formatlayıcı

**Amaç:** Para miktarlarını güzel gösterir.

```csharp
public static class MoneyFormatter
{
    // Türk Lirası formatı
    public static string Format(decimal amount)
    {
        return amount.ToString("N2") + " TL";
    }
}
```

**Kullanım:**

```csharp
decimal fiyat = 1234.56m;

// Eski yol
string metin1 = fiyat.ToString() + " TL";  // "1234.56 TL"

// Yeni yol
string metin2 = MoneyFormatter.Format(fiyat);  // "1,234.56 TL"
```

**N2 Formatı:**
- Binlik ayırıcı kullanır (virgül)
- 2 ondalık basamak gösterir

---

### 📄 GeoHelper.cs - Coğrafi Hesaplama

**Amaç:** Konum tabanlı hesaplamalar yapar.

```csharp
public static class GeoHelper
{
    // İki konum arası mesafe
    public static double CalculateDistance(Location from, Location to)
    {
        int dx = to.X - from.X;
        int dy = to.Y - from.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
    
    // Bir noktaya yakın konumları bul
    public static List<Location> FindNearby(
        Location center, 
        List<Location> locations, 
        double maxDistance)
    {
        return locations
            .Where(loc => CalculateDistance(center, loc) <= maxDistance)
            .ToList();
    }
}
```

**Mesafe Formülü (Pisagor):**

```
Mesafe = √[(x2-x1)² + (y2-y1)²]

Örnek:
Nokta A: (0, 0)
Nokta B: (3, 4)
Mesafe = √[(3-0)² + (4-0)²] = √[9 + 16] = √25 = 5
```

---

## Form1.cs - Kullanıcı Arayüzü

Form1 dosyası, uygulamanın **görsel arayüzünü** yönetir.

### Ana Bileşenler

```csharp
public partial class Form1 : Form
{
    // Veri listeleri (otomatik güncellenir)
    private readonly BindingList<Master> _masters = new();
    private readonly BindingList<Citizen> _citizens = new();
    private readonly BindingList<WorkOrder> _workOrders = new();
    
    // Servisler
    private readonly MatchingService _matchingService = new();
    private readonly PricingEngine _pricingEngine = new();
    private readonly InMemoryWorkOrderRepository _repository = new();
    
    public Form1()
    {
        InitializeComponent();
        SetupDataGrids();  // Tabloları hazırla
        LoadInitialData(); // Örnek veri yükle
        _pricingEngine.LoadRules(); // Kuralları yükle
    }
}
```

### Önemli Metodlar

#### 1. Usta Ekleme

```csharp
private void btnAddMaster_Click(object sender, EventArgs e)
{
    try
    {
        // Girişleri kontrol et
        Guard.NotNullOrWhiteSpace(txtMasterName.Text, "Ad");
        Guard.NotNullOrWhiteSpace(txtSpecialty.Text, "Uzmanlık");
        
        // Koordinatları parse et
        if (!int.TryParse(txtHomeX.Text, out int x))
            throw new Exception("X koordinatı geçersiz");
        
        if (!int.TryParse(txtHomeY.Text, out int y))
            throw new Exception("Y koordinatı geçersiz");
        
        // Yeni usta oluştur
        var master = new Master
        {
            Name = txtMasterName.Text,
            Specialty = txtSpecialty.Text,
            Phone = txtPhone.Text,
            Rating = numRating.Value,
            Home = new Location { X = x, Y = y }
        };
        
        // Listeye ekle (otomatik grid güncellenir)
        _masters.Add(master);
        
        // Formu temizle
        ClearMasterInputs();
        
        MessageBox.Show("Usta başarıyla eklendi!");
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Hata: {ex.Message}");
    }
}
```

**BindingList Nedir?**
- Normal `List` gibi ama **otomatik bildirim** yapar
- Liste değişince → UI otomatik güncellenir
- Manuel refresh gerekmez

**Örnek:**
```csharp
// BindingList ile
_masters.Add(yeniUsta);  
// Grid otomatik güncellendi! ✅

// Normal List ile
_masters.Add(yeniUsta);
dataGridView.Refresh();  // Manuel gerekli ❌
```

---

#### 2. Eşleştirme İşlemi

```csharp
private void btnMatch_Click(object sender, EventArgs e)
{
    try
    {
        // Seçili vatandaşı al
        var citizen = cmbCitizen.SelectedItem as Citizen;
        Guard.NotNull(citizen, "Vatandaş");
        
        // Talep oluştur
        var request = new Request
        {
            Citizen = citizen,
            RequiredSpecialty = txtRequestSpecialty.Text,
            Description = txtDescription.Text,
            IsUrgent = chkIsUrgent.Checked
        };
        
        // En uygun ustayı bul
        var master = _matchingService.FindBestMaster(
            request, 
            _masters.ToList()
        );
        
        if (master == null)
        {
            MessageBox.Show("Uygun usta bulunamadı!");
            return;
        }
        
        // Fiyat hesapla
        decimal basePrice = 300m;  // Temel fiyat
        
        var workOrder = new WorkOrder
        {
            Master = master,
            Request = request,
            ScheduledDate = DateOnly.FromDateTime(dtpScheduledDate.Value)
        };
        
        decimal finalPrice = _pricingEngine.CalculatePrice(
            workOrder, 
            basePrice
        );
        
        workOrder.Price = finalPrice;
        
        // Geçici olarak sakla (atama bekliyor)
        _pendingWorkOrder = workOrder;
        
        // Sonucu göster
        lblMatchResult.Text = $@"
Usta: {master.Name}
Uzmanlık: {master.Specialty}
Fiyat: {MoneyFormatter.Format(finalPrice)}
Mesafe: {GeoHelper.CalculateDistance(master.Home, citizen.Address):F2}
Bugünkü İş Yükü: {master.TodayWorkload}
";
        
        btnAssign.Enabled = true;
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Hata: {ex.Message}");
    }
}
```

**İşleyiş Adımları:**

1. **Girişleri Topla:** Vatandaş, uzmanlık, açıklama, acil mi?
2. **Talep Oluştur:** `Request` nesnesi
3. **Usta Bul:** `MatchingService.FindBestMaster()`
4. **Fiyat Hesapla:** `PricingEngine.CalculatePrice()`
5. **Sonucu Göster:** Label'da bilgileri yaz
6. **Atamayı Bekle:** Kullanıcı "İşi Ata" butonuna basacak

---

#### 3. İş Atama

```csharp
private void btnAssign_Click(object sender, EventArgs e)
{
    if (_pendingWorkOrder == null)
    {
        MessageBox.Show("Önce eşleştirme yapmalısınız!");
        return;
    }
    
    // Repository'ye kaydet
    _repository.Add(_pendingWorkOrder);
    
    // UI listesine ekle
    _workOrders.Add(_pendingWorkOrder);
    
    // Ustanın çizelgesine ekle
    _pendingWorkOrder.Master
        .Schedule[_pendingWorkOrder.ScheduledDate]
        .Add(_pendingWorkOrder);
    
    MessageBox.Show("İş başarıyla atandı!");
    
    // Temizle
    _pendingWorkOrder = null;
    ClearMatchingInputs();
    btnAssign.Enabled = false;
}
```

**Atama Sonrası:**
- Repository'de saklandı ✅
- UI grid'ine eklendi ✅
- Ustanın çizelgesine eklendi ✅

---

## SamplePlugins Klasörü

### 📄 LoyaltyDiscountRule.cs - Sadakat İndirimi Eklentisi

**Amaç:** Örnek plugin. Değerli müşterilere indirim yapar.

```csharp
// Ayrı DLL projesi!
namespace LoyaltyDiscountRule
{
    public class LoyaltyDiscountRule : IPricingRule
    {
        public string Name => "⭐ Sadakat İndirimi (%10)";
        
        public decimal Apply(WorkOrder order, decimal currentPrice)
        {
            // Usta 4.5 üzeri puana sahipse %10 indirim
            if (order.Master.Rating >= 4.5m)
            {
                return currentPrice * 0.9m;  // %10 indirim
            }
            
            return currentPrice;
        }
    }
}
```

**Plugin Nasıl Çalışır?**

1. **Ayrı Proje:**
   - `LoyaltyDiscountRule` kendi `.csproj` dosyasına sahip
   - Ana projeye referans verir

2. **Derleme:**
   ```bash
   dotnet build
   # LoyaltyDiscountRule.dll oluşur
   ```

3. **Kopyalama:**
   ```
   LoyaltyDiscountRule.dll → UstaPlatform/bin/Debug/net8.0-windows/Plugins/
   ```

4. **Yükleme:**
   - Uygulama başlarken `PricingEngine.LoadRules()` çağrılır
   - `Plugins/` klasörü taranır
   - `LoyaltyDiscountRule.dll` bulunur
   - `IPricingRule` uyguladığı görülür
   - Instance oluşturulup listeye eklenir

5. **Kullanım:**
   - Artık fiyat hesaplamalarında otomatik çalışır!

---

## Kod Örnekleri

### Örnek 1: Baştan Sona İş Akışı

```csharp
// 1. Usta ekle
var usta = new Master
{
    Name = "Ahmet Yılmaz",
    Specialty = "Tesisatçı",
    Phone = "0555 123 4567",
    Rating = 4.8m,
    Home = new Location { X = 10, Y = 20 }
};

// 2. Vatandaş ekle
var vatandas = new Citizen
{
    Name = "Mehmet Demir",
    Address = new Location { X = 15, Y = 25 }
};

// 3. Talep oluştur
var talep = new Request
{
    Citizen = vatandas,
    RequiredSpecialty = "Tesisatçı",
    Description = "Lavabo sızıntısı",
    IsUrgent = true  // Acil!
};

// 4. Usta bul
var matchingService = new MatchingService();
var bulunanUsta = matchingService.FindBestMaster(
    talep, 
    new List<Master> { usta }
);

// 5. İş emri oluştur
var isEmri = new WorkOrder
{
    Master = bulunanUsta,
    Request = talep,
    ScheduledDate = DateOnly.Today
};

// 6. Fiyat hesapla
var pricingEngine = new PricingEngine();
pricingEngine.LoadRules();

decimal baseFiyat = 300m;
decimal finalFiyat = pricingEngine.CalculatePrice(isEmri, baseFiyat);

// Sonuç:
// - Hafta sonu değilse: 300 TL
// - Acil olduğu için: 300 + 200 = 500 TL
// - Hafta sonu + Acil: 300 * 1.15 + 200 = 545 TL
```

---

### Örnek 2: Plugin Oluşturma

**1. Yeni proje oluştur:**
```bash
dotnet new classlib -n MyDiscountRule
cd MyDiscountRule
```

**2. Referans ekle:**
```bash
dotnet add reference ../UstaPlatform/UstaPlatform.csproj
```

**3. Kural yaz:**
```csharp
using UstaPlatform.Domain;
using UstaPlatform.Pricing;

namespace MyDiscountRule
{
    public class StudentDiscountRule : IPricingRule
    {
        public string Name => "🎓 Öğrenci İndirimi (%20)";
        
        public decimal Apply(WorkOrder order, decimal currentPrice)
        {
            // Açıklamada "öğrenci" kelimesi geçiyorsa
            if (order.Request.Description.Contains("öğrenci", 
                StringComparison.OrdinalIgnoreCase))
            {
                return currentPrice * 0.8m;  // %20 indirim
            }
            
            return currentPrice;
        }
    }
}
```

**4. Derle:**
```bash
dotnet build
```

**5. Kopyala:**
```bash
copy bin\Debug\net8.0\MyDiscountRule.dll ^
     ..\UstaPlatform\bin\Debug\net8.0-windows\Plugins\
```

**6. Kullan:**
- Uygulamada "Kuralları Yeniden Yükle" butonuna tıkla
- Artık 3 kural var!

---

### Örnek 3: Indexer Kullanımı

```csharp
var usta = new Master { Name = "Ali" };

// Bugün
var bugun = DateOnly.Today;
usta.Schedule[bugun].Add(isEmri1);
usta.Schedule[bugun].Add(isEmri2);

// Yarın
var yarin = DateOnly.Today.AddDays(1);
usta.Schedule[yarin].Add(isEmri3);

// Bugün kaç iş var?
int bugunkuIsSayisi = usta.Schedule[bugun].Count;  // 2

// Yarın kaç iş var?
int yarinIsSayisi = usta.Schedule[yarin].Count;  // 1

// Tüm günleri listele
foreach (var gun in usta.Schedule.Days)
{
    Console.WriteLine($"{gun}: {usta.Schedule[gun].Count} iş");
}

// Çıktı:
// 2024-10-29: 2 iş
// 2024-10-30: 1 iş
```

---

### Örnek 4: Route (IEnumerable) Kullanımı

```csharp
// Rota oluştur
var rota = new Route
{
    { 0, 0 },     // Ev
    { 10, 20 },   // 1. İş
    { 30, 40 },   // 2. İş
    { 50, 60 }    // 3. İş
};

// Tüm durakları yazdır
foreach (var (x, y) in rota)
{
    Console.WriteLine($"Konum: ({x}, {y})");
}

// Toplam mesafe
double toplamMesafe = rota.TotalDistance();
Console.WriteLine($"Toplam: {toplamMesafe:F2} km");

// Manuel ekleme
rota.Add(70, 80);  // 4. İş eklendi
```

---

## Önemli C# Kavramları

### 1. `init` Keyword

```csharp
public class Person
{
    // Sadece constructor veya object initializer'da set edilebilir
    public string Name { get; init; }
}

var p = new Person { Name = "Ali" };  // ✅ OK
p.Name = "Veli";  // ❌ Derleme hatası!
```

**Fayda:** Immutability (değiştirilemezlik)

---

### 2. `sealed` Keyword

```csharp
public sealed class Master { }

// Hata! Sealed sınıftan türetilemez
public class SuperMaster : Master { }  // ❌
```

**Fayda:** Sınıf hiyerarşisini kontrol et

---

### 3. Property vs Field

```csharp
// Field (kötü)
public string name;  // Doğrudan erişim

// Property (iyi)
public string Name { get; set; }  // Kontrollü erişim
```

**Property Avantajları:**
- Validation ekleyebilirsin
- Lazy loading yapabilirsin
- Interface'lerde kullanılabilir

---

### 4. Expression-bodied Members

```csharp
// Eski yol
public int GetCount()
{
    return _list.Count;
}

// Yeni yol (C# 6+)
public int GetCount() => _list.Count;

// Property için
public int Count => _list.Count;
```

---

### 5. Null-forgiving Operator (`!`)

```csharp
public Citizen Citizen { get; init; } = null!;
```

**Anlamı:** "Evet null ama sonra değer gelecek, endişelenme"

---

### 6. String Interpolation

```csharp
string ad = "Ali";
int yas = 25;

// Eski
string metin = "Adı: " + ad + ", Yaşı: " + yas;

// Yeni
string metin = $"Adı: {ad}, Yaşı: {yas}";
```

---

### 7. LINQ (Language Integrated Query)

```csharp
var ustalar = new List<Master> { /* ... */ };

// Filtreleme
var tesisatcilar = ustalar
    .Where(u => u.Specialty == "Tesisatçı")
    .ToList();

// Sıralama
var sirali = ustalar
    .OrderBy(u => u.Rating)
    .ToList();

// Dönüştürme
var isimler = ustalar
    .Select(u => u.Name)
    .ToList();

// Toplam
int toplam = ustalar.Count;

// Herhangi bir eleman var mı?
bool varMi = ustalar.Any(u => u.Rating > 4.5m);
```

---

## Proje Yapısı Özeti

```
UstaPlatform/
│
├── Domain/                    # Temel modeller
│   ├── Master.cs             # Usta (init, sealed)
│   ├── Citizen.cs            # Vatandaş
│   ├── Request.cs            # Talep
│   ├── WorkOrder.cs          # İş emri
│   ├── Schedule.cs           # Çizelge (Indexer ⭐)
│   └── Route.cs              # Rota (IEnumerable ⭐)
│
├── Pricing/                   # Fiyatlama sistemi
│   ├── IPricingRule.cs       # Kural arayüzü
│   └── PricingEngine.cs      # Plugin yöneticisi ⭐
│
├── Services/                  # İş mantığı
│   └── MatchingService.cs    # Eşleştirme
│
├── Infrastructure/            # Veri
│   └── InMemoryWorkOrderRepository.cs
│
├── Helpers/                   # Yardımcılar
│   ├── Guard.cs              # Validation
│   ├── MoneyFormatter.cs     # Para formatı
│   └── GeoHelper.cs          # Mesafe hesaplama
│
├── Form1.cs                   # UI (WinForms)
│
└── SamplePlugins/             # Eklentiler
    └── LoyaltyDiscountRule/  # Örnek plugin
```

---

## Sonuç

Bu proje aşağıdaki konuları kapsar:

### ✅ SOLID Prensipleri
- **S**ingle Responsibility: Her sınıf tek iş yapar
- **O**pen/Closed: Plugin sistemi ile genişletilebilir
- **L**iskov Substitution: Tüm IPricingRule'lar birbirinin yerine geçebilir
- **I**nterface Segregation: Küçük, odaklanmış arayüzler
- **D**ependency Inversion: Abstraction'lara bağımlılık

### ✅ İleri C# Özellikleri
- `init-only` properties (immutability)
- Indexer (array-like erişim)
- `IEnumerable<T>` (custom collections)
- Object initializers
- Static helper classes

### ✅ Mimari Desenler
- Repository Pattern (veri erişimi)
- Service Pattern (iş mantığı)
- Plugin Architecture (genişletilebilirlik)

### ✅ Modern Kodlama
- LINQ (sorgulama)
- Expression-bodied members
- String interpolation
- Null-safety

---

**Hazırlayan:** AI Assistant  
**Tarih:** 29 Ekim 2024  
**Amaç:** Proje dokümantasyonu ve eğitim

---

## 📚 Ek Kaynaklar

### Öğrenme Sırası

1. **Temel Kavramlar:**
   - C# Class, Property, Method
   - Constructor, Object Initializer
   - List, Dictionary

2. **Domain Sınıfları:**
   - Master.cs'den başla
   - init, sealed, property anla

3. **İleri Özellikler:**
   - Schedule.cs (Indexer)
   - Route.cs (IEnumerable)

4. **Plugin Sistemi:**
   - IPricingRule.cs
   - PricingEngine.cs
   - Kendi plugin'ini yaz

5. **UI:**
   - Form1.cs
   - BindingList nasıl çalışır?

### Pratik Yapma

1. Yeni bir kural ekle (örn: "Gece Vardiyası Ücreti")
2. Yeni bir helper sınıf yaz (örn: "DateHelper")
3. Eşleştirme algoritmasını geliştir (en yakın usta)
4. Veritabanı entegrasyonu ekle (Entity Framework)

**Başarılar!** 🚀

