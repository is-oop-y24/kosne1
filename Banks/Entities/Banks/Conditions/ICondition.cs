namespace Banks.Entities.Banks.Conditions
{
    public interface ICondition<T>
        where T : ICondition<T>
    {
        string ConditionName { get; }
        T DefaultValue { get; }
    }
}