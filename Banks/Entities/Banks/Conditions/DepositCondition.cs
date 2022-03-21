using System;

namespace Banks.Entities.Banks.Conditions
{
    public class DepositCondition : ICondition<DepositCondition>
    {
        public DepositCondition(double maxInterest, decimal valueForMaxInterest, TimeSpan lifetime)
        {
            MaxInterest = maxInterest;
            ValueForMaxInterest = valueForMaxInterest;
            Lifetime = lifetime;
        }

        public static string ConditionName { get; } = "Information about conditions for deposit accounts";
        public double MaxInterest { get; }
        public decimal ValueForMaxInterest { get; }
        public TimeSpan Lifetime { get; }
        public static DepositCondition DefaultValue() => new (0, 0, TimeSpan.Zero);

        public double GetInterest(decimal value)
        {
            if (value >= ValueForMaxInterest) return MaxInterest;
            if (value <= 0) return 0;

            return (double)(value / ValueForMaxInterest) * MaxInterest;
        }
    }
}