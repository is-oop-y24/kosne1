using System;
using Isu.Entities;
using Isu.Services;

namespace Isu
{
    internal class Program
    {
        private static void Main()
        {
            var isuService = new IsuService();
            var myGroupName = new GroupName("M3207");
            Group myGroup = isuService.AddGroup(myGroupName);
            isuService.AddStudent(myGroup, "pidoras");
            Console.WriteLine(isuService.HasStudent("pidoras"));
        }
    }
}