using System;
using System.Collections.Generic;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Services.PointClearingStrategyService
{
    public interface IClearingPointsStrategy
    {
        public DateTime DateTime { get; set; }
        public int MaxNumberOfRestorePoints { get; set; }
        List<RestorePoint> ClearRestorePoints(List<RestorePoint> restorePoints);
    }
}