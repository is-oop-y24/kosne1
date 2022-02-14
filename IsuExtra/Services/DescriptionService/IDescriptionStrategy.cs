using System;
using IsuExtra.Entities.ScheduleStructure;

namespace IsuExtra.Services.DescriptionService
{
    public interface IDescriptionStrategy
    {
        string GetDescription(Enum value);
    }
}