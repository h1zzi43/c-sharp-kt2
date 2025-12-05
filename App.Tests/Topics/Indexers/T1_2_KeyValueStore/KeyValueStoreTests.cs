using App.Topics.Indexers.T1_2_KeyValueStore;
using NUnit.Framework;

namespace App.Tests.Topics.Indexers.T1_2_KeyValueStore;

public class KeyValueStoreTests
{
    [Test]
    public void Get_UnknownId_ThrowsKeyNotFound()
    {
        var s = new KeyValueStore();
        Assert.Throws<KeyNotFoundException>(() => { var _ = s[123]; });
    }

    [Test]
    public void Get_UnknownKey_ThrowsKeyNotFound()
    {
        var s = new KeyValueStore();
        Assert.Throws<KeyNotFoundException>(() => { var _ = s["abc"]; });
    }

    [Test]
    public void Get_NullKey_ThrowsArgumentNull()
    {
        var s = new KeyValueStore();
        Assert.Throws<ArgumentNullException>(() => { var _ = s[(string)null!]; });
    }

    [Test]
    public void Set_And_Get_ById_Works()
    {
        var s = new KeyValueStore();
        s[1] = "one";
        s[2] = "two";
        Assert.Multiple(() =>
        {
            Assert.That(s[1], Is.EqualTo("one"));
            Assert.That(s[2], Is.EqualTo("two"));
        });
    }

    [Test]
    public void Set_And_Get_ByKey_Works()
    {
        var s = new KeyValueStore();
        s["a"] = "alpha";
        s["b"] = "beta";
        Assert.Multiple(() =>
        {
            Assert.That(s["a"], Is.EqualTo("alpha"));
            Assert.That(s["b"], Is.EqualTo("beta"));
        });
    }

    [Test]
    public void Replace_Existing_Value_Works()
    {
        var s = new KeyValueStore();
        s[1] = "one";
        s[1] = "uno";
        s["a"] = "alpha";
        s["a"] = "ALPHA";
        Assert.Multiple(() =>
        {
            Assert.That(s[1], Is.EqualTo("uno"));
            Assert.That(s["a"], Is.EqualTo("ALPHA"));
        });
    }
}