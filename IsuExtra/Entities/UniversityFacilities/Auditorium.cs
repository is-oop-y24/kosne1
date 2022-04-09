using IsuExtra.Services.DescriptionService;

namespace IsuExtra.Entities.UniversityFacilities
{
    public class Auditorium : DescriptionStrategy
    {
        public Auditorium(int number, CampusAddress campusAddress)
        {
            Number = number;
            Address = GetDescription(campusAddress);
        }

        public int Number { get; }
        public string Address { get; }
    }
}