namespace Banks.Entities.Banks.Conditions
{
    public class DebitCondition : ICondition<DebitCondition>
    {
        public DebitCondition(double cashbackProportion)
        {
            CashbackInterest = cashbackProportion;
        }

        public static string ConditionName { get; } = "Information about debit accounts";
        public double CashbackInterest { get; }
        public static DebitCondition DefaultValue() => new DebitCondition(0);
    }
}