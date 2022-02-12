using System.ComponentModel;

namespace IsuExtra.Entities.UniversityFacilities
{
    public enum CampusAddress
    {
        [Description("St. Petersburg, Kronverksky prospect, 49, lit. A.")]
        Address1,
        [Description("St. Petersburg, Birzhevaya line, 14, lit. A")]
        Address2,
        [Description("St. Petersburg, Grivtsova lane, 14-16, lit.A")]
        Address3,
        [Description("St. Petersburg, Lomonosova street, 9")]
        Address4,
        [Description("St. Petersburg, Tchaikovsky street, 11/2, lit.A")]
        Address5,
    }
}