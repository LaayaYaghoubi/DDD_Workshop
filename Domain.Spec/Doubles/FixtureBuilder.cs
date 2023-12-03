using AutoFixture;

public static class FixtureBuilder
{
    public static T A<T>(Func<T, T>? customization = null)
    {
        var t = new Fixture().Create<T>();
        if (null != customization)
            t = customization(t);
        return t;
    }
}