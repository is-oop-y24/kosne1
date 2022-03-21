using System;

namespace Banks.Entities.Accounts
{
    public interface IAccount
    {
        Guid Id { get; }
        Client Client { get; }
        decimal Cash { get; }
        decimal VirtualCash { get; }
        DateTime CreationDate { get; }

        void SetCash(decimal money, Guid bankId, DateTime dateTime);
        void SetVirtualCash(decimal money, Guid bankId, DateTime dateTime);
    }
}