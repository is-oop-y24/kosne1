using System.Collections.Generic;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Services.ClearRestorePointsStrategyService
{
    public class DeleteRestorePoints : IClearRestorePointsStrategy
    {
        public List<RestorePoint> ClearRestorePoints(List<RestorePoint> restorePoints, List<RestorePoint> newRestorePoints)
        {
            return newRestorePoints;
        }
    }
}