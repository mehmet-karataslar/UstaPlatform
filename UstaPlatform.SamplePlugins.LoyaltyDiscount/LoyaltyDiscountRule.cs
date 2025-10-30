using System;
using UstaPlatform.Domain;
using UstaPlatform.Pricing;

namespace LoyaltyDiscountRulePlugin;


/// Örnek plug-in kuralı: Sadakat indirimi (%10)
/// - Vatandaş kayıt tarihi 90 günden eski ise indirim uygular.

public sealed class LoyaltyDiscountRule : IPricingRule
{
    public string Name => "Sadakat İndirimi (%10)";

    public decimal Apply(WorkOrder order, decimal currentPrice)
    {
        var days = (DateTime.UtcNow - order.Citizen.RegisteredAt).TotalDays;
        return days >= 90 ? currentPrice * 0.90m : currentPrice;
    }
}

