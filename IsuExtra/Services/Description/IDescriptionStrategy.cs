using IsuExtra.Entities.UniversityFacilities;

namespace IsuExtra.Services.Description
{
    public interface IDescriptionStrategy
    {
        string GetDescription(CampusAddress value);
    }
}