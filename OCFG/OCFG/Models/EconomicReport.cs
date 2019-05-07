using System;

namespace OCFG.Models
{
    public class EconomicReport
    {
        private int id;
        private DateTime dateReceived;
        private string year;
        private string balance;
        private char status;

        public EconomicReport(int id, DateTime dateReceived, string year, string balance, char status)
        {
            this.id = id;
            this.dateReceived = dateReceived;
            this.year = year;
            this.balance = balance;
            this.status = status;
        }

        public int Id { get => id; set => id = value; }
        public DateTime DateReceived { get => dateReceived; set => dateReceived = value; }
        public string Year { get => year; set => year = value; }
        public string Balance { get => balance; set => balance = value; }
        public char Status { get => status; set => status = value; }
    }
}