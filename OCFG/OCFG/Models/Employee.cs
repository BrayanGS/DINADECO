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
        private string email;
        private DateTime dateIn;
        private DateTime dateOut;
        private int status;
        private Officer officer;
        private LinkedList<Canton> cantons;
        private string[] cantons2;


        public Employee()
        {
        }

        public Employee(DateTime dateOut, string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
            this.dateOut = dateOut;
        }

        public Employee(string name, string lastName, string idCard, string phoneNumber, DateTime dateIn, string email)
        {
            this.Name = name;
            this.lastName = lastName;
            this.idCard = idCard;
            this.phoneNumber = phoneNumber;
            this.dateIn = dateIn;
            this.email = email;
        }

        public Employee(int id, string name, string lastName, string idCard, string address, string phoneNumber, DateTime dateIn, string email, int status)
        {
            this.id = id;
            this.name = name;
            this.lastName = lastName;
            this.idCard = IdCard;
            this.address = address;
            this.phoneNumber = PhoneNumber;
            this.dateIn = dateIn;
            this.email = email;
            this.status = Status;
        }

        public Employee(int id, string name, string lastName, string idCard, string address, 
            string phoneNumber, string email, DateTime dateIn, DateTime dateOut,  Officer officer, LinkedList<Canton> cantons)
        {
            this.id = id;
            this.name = name;
            this.lastName = lastName;
            this.idCard = idCard;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.dateIn = dateIn;
            this.dateOut = dateOut;
            this.officer = officer;
            this.cantons = cantons;
        }

        public Employee(int id, string name, string lastName, string idCard, string address,
          string phoneNumber, string email, DateTime dateIn, DateTime dateOut, Officer officer, string[] cantons2)
        {
            this.id = id;
            this.name = name;
            this.lastName = lastName;
            this.idCard = idCard;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.dateIn = dateIn;
            this.dateOut = dateOut;
            this.officer = officer;
            this.cantons2 = cantons2;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string IdCard { get => idCard; set => idCard = value; }
        public string Address { get => address; set => address = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Email { get => email; set => email = value; }
        public DateTime DateIn { get => dateIn; set => dateIn = value; }
        public DateTime DateOut { get => dateOut; set => dateOut = value; }
        public Officer Officer { get => officer; set => officer = value; }
        public LinkedList<Canton> Canton { get => cantons; set => cantons = value; }
        public string[] Canton2 { get => cantons2; set => cantons2 = value; }
        public int Status { get => status; set => status = value; }
    }
}