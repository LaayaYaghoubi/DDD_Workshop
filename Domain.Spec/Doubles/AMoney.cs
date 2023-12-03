using Domain.SharedValueObject;

namespace DomainTests.Doubles;

public class AMoney
{
    decimal value = 0;

    public AMoney WithValue(decimal value)
    {
        this.value = value;
        return this;
    }

    public Money Please() => new(value);
}