﻿namespace IsuExtra.Tools.SpecificExceptions
{
    public class GroupScheduleException : IsuExtraException
    {
        public GroupScheduleException()
        {
        }

        public GroupScheduleException(string message)
            : base(message)
        {
        }

        public GroupScheduleException(string message, IsuExtraException innerException)
            : base(message, innerException)
        {
        }
    }
}