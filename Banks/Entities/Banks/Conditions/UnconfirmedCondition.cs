namespace Banks.Entities.Banks.Conditions
{
    public class UnconfirmedCondition
            : ICondition<UnconfirmedCondition>
    {
        public UnconfirmedCondition(decimal maxTransactionValue)
        {
            MaxTransactionValue = maxTransactionValue;
        }

        public static string ConditionName { get; } = "Information about condition for not confirmed rules";
        public decimal MaxTransactionValue { get; private set; }
        public static UnconfirmedCondition DefaultValue() => new UnconfirmedCondition(0);
    }
}