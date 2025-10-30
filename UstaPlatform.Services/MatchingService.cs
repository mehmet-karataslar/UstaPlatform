using System;
using System.Collections.Generic;
using System.Linq;
using UstaPlatform.Domain;

namespace UstaPlatform.Services;


/// Basit eşleştirme servisi: Uzmanlık uyumu olan ustalar arasından en az iş yüküne sahip olanı seçer.
/// Gerekirse ek buluşsal yöntemler eklenebilir (puan, mesafe vb.).

public sealed class MatchingService
{
    
    /// Verilen talep için en uygun ustayı döndürür (null: bulunamadı).
    
    public Master? Match(Request request, IEnumerable<Master> masters)
    {
        var specialty = request.DesiredSpecialty?.Trim().ToLowerInvariant();

        var candidates = masters
            .Where(m => string.IsNullOrWhiteSpace(specialty)
                || m.Specialty.Trim().ToLowerInvariant() == specialty)
            .ToList();

        if (candidates.Count == 0) return null;

        // En az yük + eşitlikte en yüksek puan
        return candidates
            .OrderBy(m => m.Load)
            .ThenByDescending(m => m.Rating)
            .FirstOrDefault();
    }
}

