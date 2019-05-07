using System;

namespace OCFG.Models
{
    public  class ConcreteLiquidation
    {
        private int id;
        private DateTime dateReceived;
        private string year;
        private char status;

        public ConcreteLiquidation(int id, DateTime dateReceived, string year, char status)
        {
            Id = id;
            this.dateReceived = dateReceived;
            this.year = year;
            this.status = status;
        }

        public int Id { get => id; set => id = value; }
        public DateTime DateReceived { get => dateReceived; set => dateReceived = value; }
        public string Year { get => year; set => year = value; }
        public char Status { get => status; set => status = value; }
    }
}