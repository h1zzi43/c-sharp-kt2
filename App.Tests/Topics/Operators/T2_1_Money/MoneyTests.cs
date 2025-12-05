using App.Topics.Operators.T2_1_Money;
using NUnit.Framework;

namespace App.Tests.Topics.Operators.T2_1_Money;

public class MoneyTests
{
    [Test]
    public void Ctor_NullOrEmptyCurrency_Throws()
    {
        Assert.Throws<ArgumentException>(() => new Money(null!, 0));
        Assert.Throws<ArgumentException>(() => new Money("", 0));
        Assert.Throws<ArgumentException>(() => new Money("   ", 0));
    }

    [Test]
    public void Equality_SameCurrency_CaseInsensitive_AndSameAmount()
    {
        var a = new Money("rub", 150);
        var b = new Money("RUB", 150);
        Assert.Multiple(() =>
        {
            Assert.That(a == b, Is.True);
        });
    }

    [Test]
    public void Inequality_DifferentCurrency_OrAmount()
    {
        var a = new Money("RUB", 100);
        var b = new Money("USD", 100);
        var c = new Money("RUB", 200);
        Assert.Multiple(() =>
        {
            Assert.That(a != b, Is.True);
            Assert.That(a != c, Is.True);
        });
    }
}
