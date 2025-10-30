using System;

namespace UstaPlatform.Domain;


/// Vatandaş varlığı: Hizmet talebi açan kişi.
/// - init-only Id ve RegisteredAt alanları, nesne oluşturulduktan sonra değiştirilemez.
/// - Ad ve Adres alanları düzenlenebilirdir.

public sealed class Citizen
{
    
    /// Benzersiz kimlik (sadece başlatmada atanır) - init-only
    
    public Guid Id { get; init; } = Guid.NewGuid();

    
    /// Kayıt zamanı (UTC) - init-only
    
    public DateTime RegisteredAt { get; init; } = DateTime.UtcNow;

    
    /// Vatandaşın adı ve soyadı
    
    public string Name { get; set; } = string.Empty;

    
    /// Adres bilgisi (metinsel)
    
    public string Address { get; set; } = string.Empty;
}

