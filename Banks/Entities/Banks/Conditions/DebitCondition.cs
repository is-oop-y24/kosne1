namespace Banks.Entities.Banks.Conditions
{
    public class DebitCondition : ICondition<DebitCondition>
    {
        private const string _conditionName = "Information about debit accounts";

        public DebitCondition(double cashbackProportion)
        {
            CashbackInterest = cashbackProportion;
        }

        public double CashbackInterest { get; }
        public string ConditionName => _conditionName;
        public DebitCondition DefaultValue => new DebitCondition(0);
    }
}