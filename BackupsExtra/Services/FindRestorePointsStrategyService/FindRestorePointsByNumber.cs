using System;
using System.Collections.Generic;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Services.FindRestorePointsStrategyService
{
    public class FindRestorePointsByNumber : IFindRestorePointsStrategy
    {
        public DateTime DateTime { get; set; }
        public int MaxNumberOfRestorePoints { get; set; }
        public List<RestorePoint> ClearRestorePoints(List<RestorePoint> restorePoints)
        {
            if (restorePoints.Count > MaxNumberOfRestorePoints)
            {
                restorePoints.RemoveRange(0, restorePoints.Count - MaxNumberOfRestorePoints);
            }

            return restorePoints;
        }
    }
}