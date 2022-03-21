using System;
using Banks.Tools.SpecificExceptions;

namespace Banks.Entities.Transactions
{
    public abstract class AbstractTransaction : ITransaction
    {
        private bool wasRealized;
        private bool wasCanceled;
        protected AbstractTransaction(decimal size)
        {
            Id = Guid.NewGuid();
            Size = size;
            wasRealized = false;
            wasCanceled = false;
        }

        public Guid Id { get; }
        public decimal Size { get; protected set; }

        public bool WasRealized
        {
            get => wasRealized;
            set
            {
                if (wasRealized && value == false)
                    throw new TransactionException("Try unrealize transaction");
                wasRealized = value;
            }
        }

        public bool WasCanceled
        {
            get => wasCanceled;
            set
            {
                if (wasCanceled && value == false)
                    throw new TransactionException("Try uncancel transaction");
                wasCanceled = value;
            }
        }

        public Guid BankFromId { get; protected set; }
        public Guid BankToId { get; protected set; }
        public Guid AccountFromId { get; protected set; }
        public Guid AccountToId { get; protected set; }

        public abstract bool HasAccess(Client client);
    }
}