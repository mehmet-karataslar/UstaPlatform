using System;
using System.Collections.Generic;

namespace UstaPlatform.Domain;


/// Ustanın çizelgesi: Günlere göre iş emirleri listesi tutar.
/// - Dizinleyici (Indexer) ile Schedule[DateOnly gün] -> İşEmri listesi erişimi sağlar.

public sealed class Schedule
{
    // İç veri yapısı: Gün -> İşEmri listesi eşlemesi
    private readonly Dictionary<DateOnly, List<WorkOrder>> _byDay = new();

    
    /// Dizinleyici: Verilen güne ait iş emirlerini döndürür (yoksa boş bir liste oluşturur).
    
    public List<WorkOrder> this[DateOnly day]
    {
        get
        {
            if (!_byDay.TryGetValue(day, out var list))
            {
                list = new List<WorkOrder>();
                _byDay[day] = list;
            }
            return list;
        }
    }

    
    /// Toplam iş emri sayısı (tüm günler toplamı)
    
    public int TotalWorkOrders
    {
        get
        {
            var total = 0;
            foreach (var kv in _byDay)
                total += kv.Value.Count;
            return total;
        }
    }
}

