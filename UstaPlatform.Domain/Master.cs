using System;

namespace UstaPlatform.Domain;


/// Usta (uzman) varlığı: Hizmet veren kişi.
/// - Uzmanlık alanı, puanı ve çizelgesi bulunur.
/// - init-only Id ve RegisteredAt alanları ile OOP güvenliği sağlanır.

public sealed class Master
{
    
    /// Benzersiz kimlik (init-only)
    
    public Guid Id { get; init; } = Guid.NewGuid();

    
    /// Kayıt zamanı (UTC, init-only)
    
    public DateTime RegisteredAt { get; init; } = DateTime.UtcNow;

    
    /// Ustanın adı
    
    public string Name { get; set; } = string.Empty;

    
    /// Uzmanlık alanı (örn: Tesisatçı, Elektrikçi)
    
    public string Specialty { get; set; } = string.Empty;

    
    /// Puan (0..5 arası)
    
    public decimal Rating { get; set; }

    
    /// Ustanın çizelgesi (günlük iş emirleri)
    
    public Schedule Schedule { get; } = new();

    
    /// Ustanın mevcut iş yükü (çizelgedeki toplam iş sayısı)
    
    public int Load => Schedule.TotalWorkOrders;
}

