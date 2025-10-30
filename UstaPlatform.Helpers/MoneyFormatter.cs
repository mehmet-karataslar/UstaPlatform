using System.Globalization;

namespace UstaPlatform.Helpers;


/// Para formatlayıcı: TL cinsinden gösterimler için yardımcı (static)

public static class MoneyFormatter
{
    
    /// Ondalık değeri "1.234,56 TL" biçiminde döndürür.
    
    public static string FormatTL(decimal amount)
    {
        var tr = new CultureInfo("tr-TR");
        return string.Format(tr, "{0:C}", amount);
    }
}

