using Domain.SharedValueObject;

public static class A
{
    public static Money ValidMoney() => A<Money>.But(with => new Money(Math.Abs(with.Value)));
}