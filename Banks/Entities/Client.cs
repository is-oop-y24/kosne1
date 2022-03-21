using System;
using Banks.Entities.ClientInformationStrategies;
using Banks.Tools.ObserverPattern;
using Banks.Tools.SpecificExceptions;

namespace Banks.Entities
{
    public class Client : IEventListenear
    {
        public Client(string firstName, string secondName, Guid bankId)
        {
            if (firstName == string.Empty) throw new ClientException("Error: empty firstName entered");
            if (secondName == string.Empty) throw new ClientException("Error: empty secondName entered");

            Id = Guid.NewGuid();
            FirstName = firstName;
            SecondName = secondName;
            BankId = bankId;
            Address = string.Empty;
            PassportNumber = string.Empty;
        }

        public Guid Id { get; }
        public string FirstName { get; }
        public string SecondName { get; }
        public string Address { get; private set; }
        public string PassportNumber { get; private set; }
        public Guid BankId { get; private set; }

        public IInformationStrategy InformationStrategy { get; set; } = null;

        public bool Confirmation => Address != string.Empty && PassportNumber != string.Empty;
        public string FullName => $"{FirstName} {SecondName}";

        public Client AddAddress(string address)
        {
            if (address == string.Empty && Address == string.Empty) return this;
            if (address == string.Empty) throw new ClientException("Error: empty address entered");
            if (Address != string.Empty) throw new ClientException("Error: client already have address");

            Address = address;
            return this;
        }

        public Client AddPassportNumber(string passportNumber)
        {
            if (passportNumber == string.Empty && PassportNumber == string.Empty) return this;
            if (passportNumber == string.Empty) throw new ClientException("Error: empty passport number entered");
            if (PassportNumber != string.Empty)
                throw new ClientException("Error: client already have passport number");

            PassportNumber = passportNumber;
            return this;
        }

        public void ReactToEvent(string eventName)
        {
            if (InformationStrategy == null) return;
            InformationStrategy.Inform(FullName, eventName + " was changed");
        }
    }
}