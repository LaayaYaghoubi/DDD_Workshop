using Domain.Spec.Doubles;

namespace DomainTests.Doubles;

public static class Build
{
    public static ATransferRequest ATransferRequest => new();
    public static AMoney AMoney => new();
    public static AnAccountId AnAccountId => new();
    public static ATransactionId ATransactionId => new();
    
    
}