using BackupsExtra.Services.FindRestorePointsStrategyService.Strategies;

namespace BackupsExtra.Services.FindRestorePointsStrategyService
{
    public interface IFindRestorePointsAbstractStrategy : IFindRestorePointsStrategy
    {
        void AddFindRestorePointStrategy(IFindRestorePointsStrategy findRestorePointsStrategy);
        void RemoveFindRestorePointStrategy(IFindRestorePointsStrategy findRestorePointsStrategy);
    }
}