using IsuExtra.Services.Description;

namespace IsuExtra.Entities.UniversityFacilities
{
    public class Auditorium
    {
        public Auditorium(int number, CampusAddress campusAddress)
        {
            Number = number;
            DescriptionStrategy = new DescriptionStrategy();
            Address = DescriptionStrategy.GetDescription(campusAddress);
        }

        public int Number { get; }
        public string Address { get; }
        public IDescriptionStrategy DescriptionStrategy { private get; set; }
    }
}