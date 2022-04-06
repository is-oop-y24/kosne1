using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Services.FindRestorePointsStrategyService.Strategies
{
    public class FindRestorePointsByDate : IFindRestorePointsStrategy
    {
        public FindRestorePointsByDate(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        public DateTime DateTime { get; }

        public List<RestorePoint> ClearRestorePoints(List<RestorePoint> restorePoints)
        {
            return restorePoints.Where(restorePoint => restorePoint.DateTime.CompareTo(DateTime) > 0).ToList();
        }

        public override int GetHashCode()
        {
            return DateTime.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            var other = (FindRestorePointsByDate)obj;
            return other.DateTime == DateTime;
        }

        protected bool Equals(FindRestorePointsByDate other)
        {
            return DateTime.Equals(other.DateTime);
        }
    }
}