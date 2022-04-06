using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Entities.JobStructure;
using BackupsExtra.Services.FindRestorePointsStrategyService.Strategies;

namespace BackupsExtra.Services.FindRestorePointsStrategyService
{
    public class FindRestorePointsByUnion : FindRestorePointAbstractStrategy
    {
        public override List<RestorePoint> ClearRestorePoints(List<RestorePoint> restorePoints)
        {
            var foundRestorePoints = new List<RestorePoint>();
            foreach (IFindRestorePointsStrategy findRestorePointsStrategy in FindRestorePointsStrategies)
            {
                foundRestorePoints.AddRange(findRestorePointsStrategy.ClearRestorePoints(new List<RestorePoint>(restorePoints)));
            }

            return restorePoints.Except(foundRestorePoints).ToList();
        }
    }
}