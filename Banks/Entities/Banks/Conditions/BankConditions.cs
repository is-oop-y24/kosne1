using Banks.Tools.ObserverPattern;

namespace Banks.Entities.Banks.Conditions
{
    public class BankConditions : EventCreator<Client>
    {
        private UnconfirmedCondition unconfirmedCondition;
        private DebitCondition debitCondition;
        private DepositCondition depositCondition;
        private CreditCondition creditCondition;
        public BankConditions(
            UnconfirmedCondition unconfirmedCondition,
            DebitCondition debitCondition,
            DepositCondition depositCondition,
            CreditCondition creditCondition)
        {
            this.unconfirmedCondition = unconfirmedCondition;
            this.debitCondition = debitCondition;
            this.depositCondition = depositCondition;
            this.creditCondition = creditCondition;
        }

        public UnconfirmedCondition UnconfirmedCondition
        {
            get => unconfirmedCondition;
            set
            {
                unconfirmedCondition = value;
                Inform(UnconfirmedCondition.ConditionName);
            }
        }

        public DebitCondition DebitCondition
        {
            get => debitCondition;
            set
            {
                debitCondition = value;
                Inform(DebitCondition.ConditionName);
            }
        }

        public DepositCondition DepositCondition
        {
            get => depositCondition;
            set
            {
                depositCondition = value;
                Inform(DepositCondition.ConditionName);
            }
        }

        public CreditCondition CreditCondition
        {
            get => creditCondition;
            set
            {
                creditCondition = value;
                Inform(CreditCondition.ConditionName);
            }
        }
    }
}