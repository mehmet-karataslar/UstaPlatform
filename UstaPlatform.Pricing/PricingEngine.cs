using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UstaPlatform.Domain;

namespace UstaPlatform.Pricing;


/// Fiyatlama motoru: Kuralları yükler (dahili ve eklenti DLL'lerinden) ve sıralı uygular.
/// - Plug-in klasörü: Uygulama dizinindeki "Plugins" klasörü (yoksa otomatik oluşturulur).
/// - DIP: Motor, somut kural sınıflarına değil IPricingRule arayüzüne bağımlıdır.

public sealed class PricingEngine
{
    private readonly List<IPricingRule> _rules = new();

  
    /// Yüklenen kuralların yalnızca okunur listesi (UI için)
    
    public IReadOnlyList<IPricingRule> Rules => _rules;

    
    /// Kural listesini yeniler: Dahili kuralları ekler ve plug-in DLL'lerini tarar.
    
    public void ReloadRules()
    {
        _rules.Clear();

        // 1) Dahili (yerleşik) kurallar
        _rules.Add(new WeekendSurchargeRule());
        _rules.Add(new EmergencyFeeRule());

        // 2) Plug-in kural DLL'leri (Plugins klasöründen)
        var baseDir = AppContext.BaseDirectory;
        var pluginsDir = Path.Combine(baseDir, "Plugins");
        Directory.CreateDirectory(pluginsDir);

        foreach (var dll in Directory.EnumerateFiles(pluginsDir, "*.dll"))
        {
            try
            {
                var asm = Assembly.LoadFrom(dll);
                var ruleTypes = asm
                    .GetTypes()
                    .Where(t => !t.IsAbstract && !t.IsInterface && typeof(IPricingRule).IsAssignableFrom(t));
                foreach (var t in ruleTypes)
                {
                    if (Activator.CreateInstance(t) is IPricingRule rule)
                    {
                        _rules.Add(rule);
                    }
                }
            }
            catch
            {
                // Basit tut: Yüklenemeyen DLL'leri sessizce atla.
            }
        }
    }

    
    /// Temel ücreti ve kuralları sıralı uygulayarak toplam fiyatı hesaplar.
    
    public decimal Calculate(WorkOrder order)
    {
        // Temel ücret: basit örnek (uzmanlığa göre değişebilir)
        decimal price = order.Master.Specialty?.ToLowerInvariant() switch
        {
            "tesisatçı" => 500m,
            "elektrikçi" => 450m,
            "marangoz" => 400m,
            _ => 350m
        };

        foreach (var rule in _rules)
        {
            price = rule.Apply(order, price);
        }

        return price;
    }
}


/// Dahili kural: Hafta sonu ek ücret (Cumartesi/Pazar +%15)

public sealed class WeekendSurchargeRule : IPricingRule
{
    public string Name => "Hafta Sonu Ek Ücreti (%15)";

    public decimal Apply(WorkOrder order, decimal currentPrice)
    {
        var dow = order.Day.DayOfWeek;
        return (dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday)
            ? currentPrice * 1.15m
            : currentPrice;
    }
}


/// Dahili kural: Acil çağrı sabit ek ücret (+200 TL)

public sealed class EmergencyFeeRule : IPricingRule
{
    public string Name => "Acil Çağrı Ek Ücreti (+200 TL)";

    public decimal Apply(WorkOrder order, decimal currentPrice)
    {
        // İş emrinde acil bilgisi yoksa talep açıklamasından anlamak yerine, 
        // pratik çözüm: Açıklama içinde [ACIL] etiketi varsa uygula.
        var emergency = order.Description?.Contains("[ACIL]", StringComparison.OrdinalIgnoreCase) == true;
        return emergency ? currentPrice + 200m : currentPrice;
    }
}

