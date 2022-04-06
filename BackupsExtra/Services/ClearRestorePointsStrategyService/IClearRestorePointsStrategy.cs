using System.Collections.Generic;
using BackupsExtra.Entities.JobStructure;
using BackupsExtra.Entities.Repository;

namespace BackupsExtra.Services.ClearRestorePointsStrategyService
{
    public interface IClearRestorePointsStrategy
    {
        List<RestorePoint> ClearRestorePoints(List<RestorePoint> restorePoints, List<RestorePoint> newRestorePoints);
    }
}