using System;

namespace Domain.Model
{
    public class Company
    {
        public Guid Id;
        public string CompanyCode;
        public string CompanyName;
        public decimal Currency;
        public Address Address;
        public Contact Contact;
    }
}
