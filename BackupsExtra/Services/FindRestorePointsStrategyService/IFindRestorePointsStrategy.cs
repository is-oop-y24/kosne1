using System;
using System.Collections.Generic;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Services.FindRestorePointsStrategyService
{
    public interface IFindRestorePointsStrategy
    {
        public DateTime DateTime { get; set; }
        public int MaxNumberOfRestorePoints { get; set; }
        List<RestorePoint> ClearRestorePoints(List<RestorePoint> restorePoints);

        public class Snapshot
        {
            public Snapshot()
            {
            }

            public Snapshot(IFindRestorePointsStrategy findRestorePointsStrategy)
            {
                DateTime = findRestorePointsStrategy.DateTime;
                MaxNumberOfRestorePoints = findRestorePointsStrategy.MaxNumberOfRestorePoints;
            }

            public DateTime DateTime { get; set; }
            public int MaxNumberOfRestorePoints { get; set; }

            public DateTime RestoreDateTime()
            {
                return DateTime;
            }

            public int RestoreMaxNumberOfRestorePoints()
            {
                return MaxNumberOfRestorePoints;
            }
        }
    }
}