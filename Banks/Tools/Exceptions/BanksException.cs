﻿using System;

namespace Banks.Tools.Exceptions
{
    public class BanksException : Exception
    {
        public BanksException()
        {
        }

        public BanksException(string message)
            : base(message)
        {
        }

        public BanksException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}