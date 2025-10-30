using System;

namespace UstaPlatform.Helpers;

/// Coğrafi yardımcılar (örnek): Rota/mesafe gibi hesaplamalar için ileride genişletilebilir.
/// Şu an basit bir mesafe tahmini sağlar.

public static class GeoHelper
{
   
    /// Izgara üzerinde Manhattan mesafesi (yaklaşık hesap): |x1-x2| + |y1-y2|
    
    public static int ManhattanDistance((int X, int Y) a, (int X, int Y) b)
        => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
}
