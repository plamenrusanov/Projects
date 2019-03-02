using System;

namespace Taxi.Data.Models
{
    public abstract class Person
    {
        protected Person(string firstname, string lastname)
        {
            Id = Guid.NewGuid().ToString();
            Firstname = firstname;
            Lastname = lastname;
        }

        public string Id { get; set; }

       

        public string TelefonNumber { get; set; }

    }
}
