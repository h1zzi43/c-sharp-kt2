using App.Topics.Indexers.T1_3_Matrix2D;
using NUnit.Framework;

namespace App.Tests.Topics.Indexers.T1_3_Matrix2D;

public class Matrix2DTests
{
    [Test]
    public void Constructor_InvalidSizes_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix2D(0, 1));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix2D(1, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix2D(-1, 3));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix2D(3, -1));
    }

    [Test]
    public void SetGet_WithinBounds_Works()
    {
        var m = new Matrix2D(2, 3);
        m[0, 0] = 1;
        m[0, 1] = 2;
        m[1, 2] = 7;
        Assert.Multiple(() =>
        {
            Assert.That(m[0, 0], Is.EqualTo(1));
            Assert.That(m[0, 1], Is.EqualTo(2));
            Assert.That(m[1, 2], Is.EqualTo(7));
        });
    }

    [Test]
    public void Get_OutOfBounds_Throws()
    {
        var m = new Matrix2D(2, 3);
        Assert.Throws<ArgumentOutOfRangeException>(() => { var _ = m[-1, 0]; });
        Assert.Throws<ArgumentOutOfRangeException>(() => { var _ = m[0, -1]; });
        Assert.Throws<ArgumentOutOfRangeException>(() => { var _ = m[2, 0]; });
        Assert.Throws<ArgumentOutOfRangeException>(() => { var _ = m[0, 3]; });
    }

    [Test]
    public void Set_OutOfBounds_Throws()
    {
        var m = new Matrix2D(2, 3);
        Assert.Throws<ArgumentOutOfRangeException>(() => m[-1, 0] = 5);
        Assert.Throws<ArgumentOutOfRangeException>(() => m[0, -1] = 5);
        Assert.Throws<ArgumentOutOfRangeException>(() => m[2, 0] = 5);
        Assert.Throws<ArgumentOutOfRangeException>(() => m[0, 3] = 5);
    }
}
