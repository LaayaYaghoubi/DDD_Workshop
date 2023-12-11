
public record TransactionCommited(string creditAccountId, string debitAccountId, decimal amount) : IDomainEvent;
