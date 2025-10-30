using UstaPlatform.Domain;

namespace UstaPlatform.Pricing;


/// Dinamik fiyatlandırma kuralı arayüzü (Plug-in sözleşmesi).
/// - Açık/Kapalı prensibine uygun olarak yeni kurallar bu arayüzü uygulayarak eklenir.

public interface IPricingRule
{
    
    /// Kurala açıklayıcı bir ad.
    
    string Name { get; }

    
    /// Fiyatı güncelleyen metot. Mevcut fiyat üzerinden artış/indirim yapabilir.
    
    decimal Apply(WorkOrder order, decimal currentPrice);
}

