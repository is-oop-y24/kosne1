using System;

namespace Banks.Entities.Banks.Conditions
{
    public class DepositCondition : ICondition<DepositCondition>
    {
        public DepositCondition(double maxInterest, decimal valueForMaxInterest, TimeSpan lifetime)
        {
            MaxInterest = maxInterest;
            ValueForMaxInterest = valueForMaxInterest;
            EndTime = DateTime.Now + lifetime;
        }

        public string ConditionName => "Information about conditions for deposit accounts";
        public DepositCondition DefaultValue => new (0, 0, TimeSpan.Zero);

        public double MaxInterest { get; }
        public decimal ValueForMaxInterest { get; }
        public DateTime EndTime { get; }
        public double GetInterest(decimal value)
        {
            if (value >= ValueForMaxInterest) return MaxInterest;
            if (value <= 0) return 0;

            return (double)(value / ValueForMaxInterest) * MaxInterest;
        }
    }
}