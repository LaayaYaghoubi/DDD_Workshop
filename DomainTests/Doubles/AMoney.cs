namespace DomainTests.Doubles;

public class AMoney
{
    decimal value = 0;

    public AMoney WithPositiveValue(decimal value)
    {
        this.value = Math.Abs(value);
        return this;
    }

    public AMoney WithNegativeValue(decimal value)
    {
        this.value = -Math.Abs(value);
        return this;
    }

    public Money Please()
        => new Money(value);
}