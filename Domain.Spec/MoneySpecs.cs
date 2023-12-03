using AutoFixture;
using AutoFixture.Xunit2;
using Domain.SharedValueObject;
using FluentAssertions;
namespace DomainTests;


public class MoneySpecs
{
    [Theory, AutoData]
    public void Money_cannot_be_negative(decimal amount)
    => new Action(() =>               //Arrange
       new Money(-Math.Abs(amount))   //Act
       ).Should().Throw<Exception>(); //Assert

    [Theory, AutoData]
    public void Supports_subtraction(uint five)
    {
        //Arrange
        var smallerNumber = A.ValidMoney();
        var biggerNumber = new Money(smallerNumber.Value + five);

        //Act
        (biggerNumber - smallerNumber)

        //Assert
        .Value.Should().Be(five);
    }

    

}