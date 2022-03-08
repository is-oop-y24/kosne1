using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Services.PointClearingStrategyService
{
    public class ClearingRestorePointsByDateOrNumber : IClearingPointsStrategy
    {
        public DateTime DateTime { get; set; }
        public int MaxNumberOfRestorePoints { get; set; }
        public List<RestorePoint> ClearRestorePoints(List<RestorePoint> restorePoints)
        {
            if (restorePoints.Count > MaxNumberOfRestorePoints)
            {
                restorePoints.RemoveRange(0, restorePoints.Count - MaxNumberOfRestorePoints);
            }

            return restorePoints.Where(restorePoint => restorePoint.DateTime.CompareTo(DateTime) > 0).ToList();
        }
    }
}