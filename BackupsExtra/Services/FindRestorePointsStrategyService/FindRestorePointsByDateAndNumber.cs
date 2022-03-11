using System;
using System.Collections.Generic;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Services.FindRestorePointsStrategyService
{
    public class FindRestorePointsByDateAndNumber : IFindRestorePointsStrategy
    {
        public DateTime DateTime { get; set; }
        public int MaxNumberOfRestorePoints { get; set; }
        public List<RestorePoint> ClearRestorePoints(List<RestorePoint> restorePoints)
        {
            var foundRestorePoints = new List<RestorePoint>();
            foreach (RestorePoint restorePoint in restorePoints)
            {
                if (restorePoint.DateTime.CompareTo(DateTime) > 0 && foundRestorePoints.Count < MaxNumberOfRestorePoints)
                {
                    foundRestorePoints.Add(restorePoint);
                }
            }

            return foundRestorePoints;
        }
    }
}