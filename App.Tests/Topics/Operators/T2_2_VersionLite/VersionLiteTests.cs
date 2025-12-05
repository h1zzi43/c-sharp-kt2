using App.Topics.Operators.T2_2_VersionLite;
using NUnit.Framework;

namespace App.Tests.Topics.Operators.T2_2_VersionLite;

public class VersionLiteTests
{
    [Test]
    public void Ctor_Negative_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new VersionLite(-1, 0, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new VersionLite(0, -1, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new VersionLite(0, 0, -1));
    }

    [Test]
    public void Compare_And_Operators_Work_Lexicographic()
    {
        var a = new VersionLite(1, 2, 3);
        var b = new VersionLite(1, 3, 0);
        var c = new VersionLite(2, 0, 0);
        var d = new VersionLite(1, 2, 3);

        Assert.Multiple(() =>
        {
            Assert.That(a.CompareTo(b) < 0);
            Assert.That(b.CompareTo(c) < 0);
            Assert.That(c.CompareTo(a) > 0);
            Assert.That(a == d, Is.True);
            Assert.That(a < b, Is.True);
            Assert.That(b > a, Is.True);
            Assert.That(a <= d, Is.True);
            Assert.That(a >= d, Is.True);
        });
    }

    [Test]
    public void CompareTo_Null_ReturnsPositive()
    {
        var a = new VersionLite(0, 0, 1);
        Assert.That(a.CompareTo(null), Is.GreaterThan(0));
    }
}
