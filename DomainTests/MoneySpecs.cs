using AutoFixture.Xunit2;
using DomainTests.Doubles;
using FluentAssertions;
using Services.Exceptions;

namespace DomainTests;

public class MoneySpecs
{
    static Money AValidMoney() => FixtureBuilder.A<Money>(with => new Money(Math.Abs(with.Value)));


    [Theory, AutoData]
    public void Money_can_be_positive(decimal amount)
    {
        Build.AMoney.WithValue(amount).Please()
            
            .Value.Should().Be(amount);
    }

    [Theory, AutoData]
    public void Money_can_be_zero(decimal amount)
    {
        Build.AMoney.WithValue(amount - amount).Please()
            
            .Value.Should().Be(amount - amount);
    }

    [Theory, AutoData]
    public void Money_cannot_be_negative(decimal amount)
        => new Action(() => 
                
                Build.AMoney.WithValue(amount.ConvertToNegative()).Please()) 
            
            .Should().Throw<MoneyCanNotBeNegativeException>();

    [Theory, AutoData]
    public void Supports_subtraction_when_left_is_greater(uint five)
    {
        var right = AValidMoney();
        var left = Build.AMoney.WithValue(right.Value + five).Please();
        
        (left - right)
            
            .Value.Should().Be(five);
    }

    [Fact]
    public void Supports_subtraction_when_left_is_equal_to_right()
    {
        var right = AValidMoney();
        var left = Build.AMoney.WithValue(right.Value).Please();
        var expected = left.Value - right.Value;
        
        (left - right)
            
            .Value.Should().Be(expected);
    }

    [Theory, AutoData]
    public void Does_not_support_subtraction_when_left_is_less_than_right(uint five)
    {
        var left = AValidMoney();
        var right = Build.AMoney.WithValue(left.Value + five).Please();

        
        var subtractAction = () => left - right;
        
        subtractAction.Should().Throw<MoneyCanNotBeNegativeException>();
    }

    [Fact]
    public void Supports_addition()
    {
        var left = AValidMoney();
        var right = AValidMoney();
        var expected = right.Value + left.Value;

        (left + right)
            
            .Value.Should().Be(expected);
    }

    [Theory, AutoData]
    public void Supports_greater_than(decimal five)
    {
        var smallerNumber = AValidMoney();
        var biggerNumber = Build.AMoney.WithValue(smallerNumber.Value + five).Please();

        (biggerNumber > smallerNumber)
            
            .Should().BeTrue();
    }

    [Theory, AutoData]
    public void Supports_less_than(decimal five)
    {
        var smallerNumber = AValidMoney();
        var biggerNumber = Build.AMoney.WithValue(smallerNumber.Value + five).Please();

        (smallerNumber < biggerNumber)
            
            .Should().BeTrue();
    }

    [Theory]
    [InlineAutoData(15, 10)]
    [InlineAutoData(30, 20)]
    [InlineAutoData(0, 0)]
    public void Supports_greater_than_or_equal(decimal bigger, decimal smaller)
    {
        var biggerNumber = Build.AMoney.WithValue(bigger).Please();
        var smallerNumber = Build.AMoney.WithValue(smaller).Please();

        (biggerNumber >= smallerNumber)
            
            .Should().BeTrue();
    }

    [Theory]
    [InlineAutoData(5, 10)]
    [InlineAutoData(4, 20)]
    [InlineAutoData(0, 0)]
    public void Supports_less_than_or_equal(decimal bigger, decimal smaller)
    {
        var smallerNumber = Build.AMoney.WithValue(smaller).Please();
        var biggerNumber = Build.AMoney.WithValue(bigger).Please();

        (smallerNumber <= biggerNumber)
            
            .Should().BeTrue();
    }
}