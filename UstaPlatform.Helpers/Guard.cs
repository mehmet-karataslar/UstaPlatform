using System;

namespace UstaPlatform.Helpers;

/// Basit doğrulama yardımcıları (static): SRP kapsamında tek sorumluluk doğrulamadır.

public static class Guard
{
    
    /// String boş/whitespace ise istisna fırlatır.
    
    public static void NotNullOrWhiteSpace(string? value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{paramName} boş olamaz.", paramName);
    }
}
