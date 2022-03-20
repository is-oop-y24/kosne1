using System.Collections.Generic;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Services.FindRestorePointsStrategyService.Strategies
{
    public interface IFindRestorePointsStrategy
    {
        List<RestorePoint> ClearRestorePoints(List<RestorePoint> restorePoints);
    }
}