// Topic: Operators — T2.1 Money (==, !=, Equals/GetHashCode)
// Задача: реализовать value object для денег.
// Требования:
// - Поля: string Currency (например, "RUB", "USD"), long Amount (в минимальных единицах: копейки/центы).
// - Конструктор Money(string currency, long amount) — должен проверять currency на null/пустую строку.
// - Реализовать IEquatable<Money>, переопределить Equals(object), GetHashCode, операторы == и !=.
// - Деньги равны только если совпадают и валюта, и сумма.
// Подсказка: подумайте о нормализации регистра Currency (например, ToUpperInvariant) — оговорено в тестах.

namespace App.Topics.Operators.T2_1_Money;

public struct Money : IEquatable<Money>
{
    public string Currency { get; }
    public long Amount { get; }

    public Money(string currency, long amount)
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be null or empty.", nameof(currency));

        Currency = currency.Trim();
        Amount = amount;
    }

    // Операторы равенства
    public static bool operator ==(Money left, Money right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Money left, Money right)
    {
        return !(left == right);
    }

    // Реализация IEquatable<Money>
    public bool Equals(Money other)
    {
        // Нормализация регистра валюты
        return string.Equals(Currency, other.Currency, StringComparison.OrdinalIgnoreCase)
               && Amount == other.Amount;
    }

    // Переопределение Object.Equals
    public override bool Equals(object obj)
    {
        return obj is Money other && Equals(other);
    }

    // Переопределение GetHashCode
    public override int GetHashCode()
    {
        return HashCode.Combine(Currency?.ToUpperInvariant(), Amount);
    }

    // Арифметические операторы
    public static Money operator +(Money left, Money right)
    {
        ValidateSameCurrency(left, right);
        return new Money(left.Currency, left.Amount + right.Amount);
    }

    public static Money operator -(Money left, Money right)
    {
        ValidateSameCurrency(left, right);
        return new Money(left.Currency, left.Amount - right.Amount);
    }

    public static Money operator ++(Money money)
    {
        return new Money(money.Currency, money.Amount + 1);
    }

    public static Money operator --(Money money)
    {
        return new Money(money.Currency, money.Amount - 1);
    }

    // Операторы деления и остатка
    public static Money operator /(Money money, int divisor)
    {
        if (divisor == 0)
            throw new DivideByZeroException("Cannot divide Money by zero.");

        // Деление с усечением к нулю
        long result = divisor > 0
            ? money.Amount / divisor
            : -(-money.Amount / divisor);

        return new Money(money.Currency, result);
    }

    public static long operator %(Money money, int divisor)
    {
        if (divisor == 0)
            throw new DivideByZeroException("Cannot compute remainder when divisor is zero.");

        return money.Amount % divisor;
    }

    // Вспомогательные методы
    private static void ValidateSameCurrency(Money left, Money right)
    {
        if (!string.Equals(left.Currency, right.Currency, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                $"Cannot perform operation on different currencies: {left.Currency} and {right.Currency}");
        }
    }

    // Метод для удобного представления
    public override string ToString()
    {
        return $"{Amount} {Currency.ToUpper()}";
    }
}
