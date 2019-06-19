using System;

namespace OCFG.Models
{
    public class EconomicReport
    {
        private int id;
        private DateTime dateReceived;
        private string year;
        private float balance;
        private char status;

        public EconomicReport()
        {

        }

        public EconomicReport(DateTime dateReceived, string year)
        {
            this.dateReceived = dateReceived;
            this.year = year;
        }

        public EconomicReport(int id, DateTime dateReceived, string year, float balance)
        {
            this.id = id;
            this.dateReceived = dateReceived;
            this.year = year;
            this.balance = balance;
        }

        public EconomicReport(int id, DateTime dateReceived, string year, float balance, char status)
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
        public float Balance { get => balance; set => balance = value; }
        public char Status { get => status; set => status = value; }
    }
}