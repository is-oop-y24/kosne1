using System;

namespace IsuExtra.Services.Description
{
    public interface IDescriptionStrategy
    {
        string GetDescription(Enum value);
    }
}