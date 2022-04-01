namespace Banks.Entities.Banks.Conditions
{
    public interface ICondition<T>
        where T : ICondition<T>
    {
        static string ConditionName { get; }
    }
}