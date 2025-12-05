// Topic: Operators — T2.2 VersionLite (relational operators)
// Задача: реализовать сравнение версий по лексикографическому порядку: (Major, Minor, Patch).
// Требования:
// - Публичные только для чтения свойства: int Major, Minor, Patch; задать через конструктор.
// - Реализовать IComparable<VersionLite> и операторы <, >, <=, >=.
// - Сравнение: сначала Major, затем Minor, затем Patch.
// - Конструктор должен проверять, что значения не отрицательные; иначе ArgumentOutOfRangeException.

namespace App.Topics.Operators.T2_2_VersionLite;

public class VersionLite : IComparable<VersionLite>, IComparable
{
    public int Major { get; }
    public int Minor { get; }
    public int Patch { get; }

    public VersionLite(int major, int minor, int patch)
    {
        if (major < 0)
            throw new ArgumentOutOfRangeException(nameof(major), "Major must be non-negative.");

        if (minor < 0)
            throw new ArgumentOutOfRangeException(nameof(minor), "Minor must be non-negative.");

        if (patch < 0)
            throw new ArgumentOutOfRangeException(nameof(patch), "Patch must be non-negative.");

        Major = major;
        Minor = minor;
        Patch = patch;
    }

    // Реализация IComparable<VersionLite>
    public int CompareTo(VersionLite other)
    {
        if (other is null)
            return 1; // Любая версия больше null

        // Сравниваем Major
        int majorComparison = Major.CompareTo(other.Major);
        if (majorComparison != 0)
            return majorComparison;

        // Если Major равны, сравниваем Minor
        int minorComparison = Minor.CompareTo(other.Minor);
        if (minorComparison != 0)
            return minorComparison;

        // Если Minor равны, сравниваем Patch
        return Patch.CompareTo(other.Patch);
    }

    // Реализация IComparable (для совместимости)
    public int CompareTo(object obj)
    {
        if (obj is null)
            return 1;

        if (obj is VersionLite other)
            return CompareTo(other);

        throw new ArgumentException($"Object must be of type {nameof(VersionLite)}");
    }

    // Операторы сравнения
    public static bool operator ==(VersionLite left, VersionLite right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (left is null || right is null)
            return false;

        return left.CompareTo(right) == 0;
    }

    public static bool operator !=(VersionLite left, VersionLite right)
    {
        return !(left == right);
    }

    public static bool operator <(VersionLite left, VersionLite right)
    {
        if (left is null)
            return right is not null; // null меньше любого не-null

        return left.CompareTo(right) < 0;
    }

    public static bool operator >(VersionLite left, VersionLite right)
    {
        if (left is null)
            return false; // null не больше ничего

        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(VersionLite left, VersionLite right)
    {
        if (left is null)
            return true; // null меньше или равен всему

        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(VersionLite left, VersionLite right)
    {
        if (left is null)
            return right is null; // null >= null, но null не >= не-null

        return left.CompareTo(right) >= 0;
    }

    // Переопределение Equals и GetHashCode
    public override bool Equals(object obj)
    {
        return obj is VersionLite other && CompareTo(other) == 0;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Major, Minor, Patch);
    }

    // Метод для удобного представления
    public override string ToString()
    {
        return $"{Major}.{Minor}.{Patch}";
    }
}

