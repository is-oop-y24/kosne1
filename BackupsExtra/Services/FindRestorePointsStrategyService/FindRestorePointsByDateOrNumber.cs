using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Services.FindRestorePointsStrategyService
{
    public class FindRestorePointsByDateOrNumber : IFindRestorePointsStrategy
    {
        public DateTime DateTime { get; set; }
        public int MaxNumberOfRestorePoints { get; set; }
        public List<RestorePoint> ClearRestorePoints(List<RestorePoint> restorePoints)
        {
            restorePoints = restorePoints.Where(restorePoint => restorePoint.DateTime.CompareTo(DateTime) > 0).ToList();
            if (restorePoints.Count > MaxNumberOfRestorePoints)
            {
                restorePoints.RemoveRange(0, restorePoints.Count - MaxNumberOfRestorePoints);
            }

            return restorePoints;
        }
    }
}