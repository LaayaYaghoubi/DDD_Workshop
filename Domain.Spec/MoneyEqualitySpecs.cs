using Domain.SharedValueObject;
using FluentAssertions;

public class MoneyEqualitySpecs
{
    [Fact]
    public void Equals_another_object_with_the_same_value()
    {
        var left = A.ValidMoney();
        var right = new Money(left.Value);
        left.Equals(right).Should().BeTrue();
        (left == right).Should().BeTrue();
    }

    [Fact]
    public void Equals_the_same_reference_object()
    {
        var left = A.ValidMoney();
        var right = left;
        left.Equals(right).Should().BeTrue();
        (left == right).Should().BeTrue();
    }

    [Fact]
    public void Doesnt_equal_another_object_with_a_different_type()
    {
        var left = A.ValidMoney();
        var right = new object();
        left.Equals(right).Should().BeFalse();
    }
}