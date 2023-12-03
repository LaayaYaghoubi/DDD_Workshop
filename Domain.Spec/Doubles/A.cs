using AutoFixture;

public static class A<T>
{
    public static T But(Func<T, T>? customization = null)
    {
        var t = new Fixture().Create<T>();
        if (null != customization)
            t = customization(t);
        return t;
    }
}