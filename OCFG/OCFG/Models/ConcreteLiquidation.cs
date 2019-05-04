using System;

namespace OCFG.Models
{
    public  class ConcreteLiquidation
    {
        private int id;
        private DateTime dateReceived;
        private DateTime year;
        private char status;

        public ConcreteLiquidation(int id, DateTime dateReceived, DateTime year, char status, int id)
        {
            Id = id;
            this.dateReceived = dateReceived;
            this.year = year;
            this.status = status;
            Id = id;
        }

        public int Id { get => id; set => id = value; }
        public DateTime DateReceived { get => dateReceived; set => dateReceived = value; }
        public DateTime Year { get => year; set => year = value; }
        public char Status { get => status; set => status = value; }
    }
}