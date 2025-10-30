using System;
using System.Collections;
using System.Collections.Generic;

namespace UstaPlatform.Domain;


/// Rota: Ustanın gün içindeki ziyaret sırasını temsil eden özel koleksiyon.
/// - IEnumerable<(int X, int Y)> uygular.
/// - Koleksiyon başlatıcıları için Add(int x, int y) metodu içerir.

public sealed class Route : IEnumerable<(int X, int Y)>
{
    private readonly List<(int X, int Y)> _points = new();

    
    /// Koleksiyon başlatıcılarını destekleyen ekleme metodu.
    /// Örn: new Route { { 1, 2 }, { 5, 8 } };
    
    public void Add(int x, int y) => _points.Add((x, y));

    
    /// Standart IEnumerable uygulaması.
    
    public IEnumerator<(int X, int Y)> GetEnumerator() => _points.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

