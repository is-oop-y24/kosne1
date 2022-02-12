using System;

namespace IsuExtra.Services.DescriptionService
{
    public interface IDescriptionStrategy
    {
        string GetDescription(Enum value);
    }
}