using System;
using System.Collections.Generic;

namespace OCFG.Models
{
    public class Employee
    {
        private int id;
        private string name;
        private string lastName;
        private string idCard;
        private string address;
        private string phoneNumber;
        private DateTime dateIn;
        private DateTime dateOut;
        private List<Association> assotiations;
        private Officer officer;

        public Employee()
        {
        }
            public Employee(int id, string name, string lastName, string idCard, string address, 
            string phoneNumber, DateTime dateIn, DateTime dateOut, List<Association> assotiations, Officer officer)
        {
            this.id = id;
            this.name = name;
            this.lastName = lastName;
            this.idCard = idCard;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.dateIn = dateIn;
            this.dateOut = dateOut;
            this.assotiations = assotiations;
            this.officer = officer;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string IdCard { get => idCard; set => idCard = value; }
        public string Address { get => address; set => address = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public DateTime DateIn { get => dateIn; set => dateIn = value; }
        public DateTime DateOut { get => dateOut; set => dateOut = value; }
        public List<Association> Assotiations { get => assotiations; set => assotiations = value; }
        public Officer Officer { get => officer; set => officer = value; }
    }
}