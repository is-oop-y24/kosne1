using System;

namespace Banks.Entities.Transactions
{
    public interface ITransaction
    {
        Guid Id { get; }
        decimal Size { get; }
        bool WasRealized { get; set; }
        bool WasCanceled { get; set; }

        Guid BankFromId { get; }
        Guid BankToId { get; }
        Guid AccountFromId { get; }
        Guid AccountToId { get; }

        bool HasAccess(Client client);
    }
}