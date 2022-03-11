using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Services.ClearRestorePointsStrategyService
{
    public class MergeRestorePoints : IClearRestorePointsStrategy
    {
        public List<RestorePoint> ClearRestorePoints(List<RestorePoint> restorePoints, List<RestorePoint> newRestorePoints)
        {
            newRestorePoints.ForEach(restorePoint => restorePoints.Remove(restorePoint));

            restorePoints.Sort((restorePoint1, restorePoint2) =>
                restorePoint1.DateTime.CompareTo(restorePoint2.DateTime));
            restorePoints.Reverse();
            RestorePoint restorePointForMerge = newRestorePoints.First();
            restorePoints.ForEach(restorePoint => restorePointForMerge.Merge(restorePoint));

            return newRestorePoints;
        }
    }
}