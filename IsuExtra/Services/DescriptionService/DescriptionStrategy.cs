﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using IsuExtra.Entities.ScheduleStructure;

namespace IsuExtra.Services.DescriptionService
{
    public class DescriptionStrategy : IDescriptionStrategy
    {
        public string GetDescription(Enum value)
        {
            MemberInfo member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            string description = member?.GetCustomAttribute<DescriptionAttribute>()?.Description;

            return description ?? string.Empty;
        }
    }
}