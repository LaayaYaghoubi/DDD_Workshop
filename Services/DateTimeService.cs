public interface DateTimeService
{
    DateTime Now { get; }
}

public class DateTimeServiceImplementation : DateTimeService
{
    public DateTime Now => DateTime.Now;
}