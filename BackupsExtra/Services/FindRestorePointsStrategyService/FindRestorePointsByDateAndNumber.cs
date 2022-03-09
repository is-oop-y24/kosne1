using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Services.FindRestorePointsStrategyService
{
    public class FindRestorePointsByDateAndNumber : IFindRestorePointsStrategy
    {
        public DateTime DateTime { get; set; }
        public int MaxNumberOfRestorePoints { get; set; }
        public List<RestorePoint> ClearRestorePoints(List<RestorePoint> restorePoints)
        {
            return restorePoints.Where(restorePoint => restorePoint.DateTime.CompareTo(DateTime) > 0 || restorePoints.Count <= MaxNumberOfRestorePoints).ToList();
        }
    }
}