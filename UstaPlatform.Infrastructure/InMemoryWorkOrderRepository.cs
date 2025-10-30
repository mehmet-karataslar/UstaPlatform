using System;
using System.Collections.Generic;
using System.Linq;
using UstaPlatform.Domain;

namespace UstaPlatform.Infrastructure;


/// Basit bellek içi iş emri deposu (Repository): Demo ve test amaçlıdır.

public sealed class InMemoryWorkOrderRepository
{
    private readonly List<WorkOrder> _orders = new();

    
    /// Tüm iş emirleri (okunur koleksiyon)
    
    public IReadOnlyList<WorkOrder> All => _orders;

    
    /// Yeni iş emri ekler.
    
    public void Add(WorkOrder order) => _orders.Add(order);

    
    /// Gün bazında iş emirlerini getirir.
    
    public IEnumerable<WorkOrder> GetByDay(DateOnly day) => _orders.Where(o => o.Day == day);
}

