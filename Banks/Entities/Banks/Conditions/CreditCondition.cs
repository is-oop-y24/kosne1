namespace Banks.Entities.Banks.Conditions
{
    public class CreditCondition : ICondition<CreditCondition>
    {
        public CreditCondition(decimal limit, decimal fixCommission)
        {
            Limit = limit;
            FixCommission = fixCommission;
        }

        public decimal Limit { get; }
        public decimal FixCommission { get; }
        public string ConditionName { get; } = "Information about credit accounts";
        public CreditCondition DefaultValue { get; } = new (0, 0);
    }
}