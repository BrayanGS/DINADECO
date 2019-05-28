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
        private Officer officer;
        private string email;
        private string name_employee;
        private string last_name;
        private string id_card;
        private string phone_number;
        private DateTime date_in;

        public Employee()
        {
        }

        public Employee(DateTime dateOut, string phoneNumber)
        {
            this.dateOut = dateOut;
            this.phoneNumber = phoneNumber;
        }

        public Employee(string name_employee, string last_name, string id_card, string phone_number, DateTime date_in, string email)
        {
            this.name_employee = name_employee;
            this.last_name = last_name;
            this.id_card = id_card;
            this.phone_number = phone_number;
            this.date_in = date_in;
            this.email = email;
        }

        public Employee(int id, string name, string lastName, string idCard, string address, 
            string phoneNumber, DateTime dateIn, DateTime dateOut,  Officer officer, string email)
        {
            this.id = id;
            this.name = name;
            this.lastName = lastName;
            this.idCard = idCard;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.dateIn = dateIn;
            this.dateOut = dateOut;
            this.officer = officer;
            this.email = email;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string IdCard { get => idCard; set => idCard = value; }
        public string Address { get => address; set => address = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public DateTime DateIn { get => dateIn; set => dateIn = value; }
        public DateTime DateOut { get => dateOut; set => dateOut = value; }
        public Officer Officer { get => officer; set => officer = value; }
        public string Email { get => email; set => email = value; }
    }
}