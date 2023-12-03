using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;

public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute()
        : base(() =>
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            //fixture.Customizations.Add(new PositiveDecimalBuilder());
            return fixture;
        })
    {
    }
}

public class PositiveDecimalBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(decimal))
        {
            return context.Resolve(typeof(decimal)) switch
            {
                decimal value when value > 0 => value,
                _ => context.Resolve(typeof(decimal)),
            };
        }

        return new NoSpecimen();
    }
}