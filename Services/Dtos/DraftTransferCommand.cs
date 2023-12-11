public record DraftTransferCommand(
    string TransactionId,
    string CreditAccountId,
    string DebitAccountId,
    decimal Amount);