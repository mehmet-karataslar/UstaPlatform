using System;

namespace UstaPlatform.Domain;


/// İş emri: Onaylanmış ve ustaya atanmış iş.
/// - Fiyat, tarih, atanmış usta ve rota bilgisi içerir.

public sealed class WorkOrder
{
    
    /// Benzersiz iş emri numarası (init-only)
    
    public Guid Id { get; init; } = Guid.NewGuid();

    
    /// İşi talep eden vatandaş
    
    public Citizen Citizen { get; set; } = new();

    
    /// Atanan usta
    
    public Master Master { get; set; } = new();

    
    /// Talebin açıklaması (kopya)
    
    public string Description { get; set; } = string.Empty;

    
    /// İşin yapılacağı adres
    
    public string Address { get; set; } = string.Empty;

    
    /// Planlanan gün (DateOnly)
    
    public DateOnly Day { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    
    /// Hesaplanan toplam fiyat (TL)
    
    public decimal TotalPrice { get; set; }

    
    /// Ustanın günlük rotasında durak bilgisi (opsiyonel)
    
    public Route? Route { get; set; }
}

