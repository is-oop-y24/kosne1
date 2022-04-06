using System.Collections.Generic;
using BackupsExtra.Entities.JobStructure;
using BackupsExtra.Services.FindRestorePointsStrategyService.Strategies;

namespace BackupsExtra.Services.FindRestorePointsStrategyService
{
    public abstract class FindRestorePointAbstractStrategy : IFindRestorePointsAbstractStrategy
    {
        protected FindRestorePointAbstractStrategy()
        {
            FindRestorePointsStrategies = new List<IFindRestorePointsStrategy>();
        }

        protected List<IFindRestorePointsStrategy> FindRestorePointsStrategies { get; }
        public abstract List<RestorePoint> ClearRestorePoints(List<RestorePoint> restorePoints);

        public void AddFindRestorePointStrategy(IFindRestorePointsStrategy findRestorePointsStrategy)
        {
            FindRestorePointsStrategies.Add(findRestorePointsStrategy);
        }

        public void RemoveFindRestorePointStrategy(IFindRestorePointsStrategy findRestorePointsStrategy)
        {
            FindRestorePointsStrategies.Remove(findRestorePointsStrategy);
        }
    }
}