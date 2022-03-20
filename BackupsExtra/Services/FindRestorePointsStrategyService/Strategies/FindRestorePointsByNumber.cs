using System.Collections.Generic;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Services.FindRestorePointsStrategyService.Strategies
{
    public class FindRestorePointsByNumber : IFindRestorePointsStrategy
    {
        public FindRestorePointsByNumber(int maxNumberOfRestorePoints)
        {
            MaxNumberOfRestorePoints = maxNumberOfRestorePoints;
        }

        public int MaxNumberOfRestorePoints { get; }
        public List<RestorePoint> ClearRestorePoints(List<RestorePoint> restorePoints)
        {
            if (restorePoints.Count > MaxNumberOfRestorePoints)
            {
                restorePoints.RemoveRange(0, restorePoints.Count - MaxNumberOfRestorePoints);
            }

            return restorePoints;
        }

        public override int GetHashCode()
        {
            return MaxNumberOfRestorePoints.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            var other = (FindRestorePointsByNumber)obj;
            return other.MaxNumberOfRestorePoints == MaxNumberOfRestorePoints;
        }

        protected bool Equals(FindRestorePointsByNumber other)
        {
            return MaxNumberOfRestorePoints.Equals(other.MaxNumberOfRestorePoints);
        }
    }
}