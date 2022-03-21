namespace Banks.Entities.Banks.Conditions
{
    public class BankConditions
    {
        public BankConditions(
            UnconfirmedCondition unconfirmedCondition,
            DebitCondition debitCondition,
            DepositCondition depositCondition,
            CreditCondition creditCondition)
        {
            UnconfirmedCondition = unconfirmedCondition;
            DebitCondition = debitCondition;
            DepositCondition = depositCondition;
            CreditCondition = creditCondition;
        }

        public UnconfirmedCondition UnconfirmedCondition { get; }
        public DebitCondition DebitCondition { get; }
        public DepositCondition DepositCondition { get; }
        public CreditCondition CreditCondition { get; }
    }
}