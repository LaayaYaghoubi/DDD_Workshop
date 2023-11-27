public class Money
{
    public decimal Value { get; }
    public Money(decimal amount)
    {
        if (amount < 0) throw new InvalidOperationException("Money cannot be negative.");
        Value = amount;
    }

    public static Money operator -(Money left, Money right)
    => new Money(left.Value - right.Value);

    public static Money operator +(Money left, Money right)
    => new Money(left.Value + right.Value);

    public static bool operator <(Money left, Money right)
    => left.Value < right.Value;

    public static bool operator >(Money left, Money right)
    => left.Value > right.Value;

    public static bool operator <=(Money left, Money right)
    => left.Value <= right.Value;

    public static bool operator >=(Money left, Money right)
    => left.Value >= right.Value;

    public static implicit operator Money(decimal amount)
    => new Money(amount);

}