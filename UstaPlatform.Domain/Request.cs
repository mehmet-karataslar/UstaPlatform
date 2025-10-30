using System;

namespace UstaPlatform.Domain;


/// Talep varlığı: Vatandaş tarafından açılan iş talebi.
/// - Açıklama, adres, kategori ve tercihler içerir.

public sealed class Request
{
    
    /// Talebi oluşturan vatandaş
    
    public Citizen Citizen { get; set; } = new();

    
    /// Talebin kısa açıklaması (örn: "Mutfak lavabosu sızıntısı")
    
    public string Description { get; set; } = string.Empty;

    
    /// İstenilen uzmanlık (örn: Tesisatçı)
    
    public string DesiredSpecialty { get; set; } = string.Empty;

    
    /// Talebin adresi (varsayılan: vatandaş adresi)
    
    public string Address { get; set; } = string.Empty;

    
    /// Randevu tarihi (saat bilgisiz)
    
    public DateOnly RequestedDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    
    /// Acil çağrı mı? (Fiyat kuralları için tetikleyici)
    
    public bool IsEmergency { get; set; }
}

