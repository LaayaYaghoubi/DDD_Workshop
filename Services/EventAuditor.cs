
public class EventAuditor : IHandleMessage<TransactionCommited>
{
    public void Handle(TransactionCommited @event)
    => Console.WriteLine(@event);
}