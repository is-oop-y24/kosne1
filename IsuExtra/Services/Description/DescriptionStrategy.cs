using System.ComponentModel;
using System.Linq;
using System.Reflection;
using IsuExtra.Entities.UniversityFacilities;

namespace IsuExtra.Services.Description
{
    public class DescriptionStrategy : IDescriptionStrategy
    {
        public string GetDescription(CampusAddress value)
        {
            MemberInfo member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            string description = member?.GetCustomAttribute<DescriptionAttribute>()?.Description;

            return description ?? string.Empty;
        }
    }
}