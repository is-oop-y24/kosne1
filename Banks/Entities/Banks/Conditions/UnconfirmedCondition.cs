namespace Banks.Entities.Banks.Conditions
{
    public class UnconfirmedCondition : ICondition<UnconfirmedCondition>
    {
        public UnconfirmedCondition(decimal maxTransactionValue)
        {
            MaxTransactionValue = maxTransactionValue;
        }

        public decimal MaxTransactionValue { get; }

        public string ConditionName { get; } = "Information about condition for not confirmed rules";
        public UnconfirmedCondition DefaultValue { get; } = new (0);
    }
}