using System;

namespace OCFG.Models
{
    public  class ConcreteLiquidation
    {
        private int id;
        private DateTime dateReceived;
        private String year;
        private char status;

        public ConcreteLiquidation()
        {

        }

        public ConcreteLiquidation(DateTime dateReceived, String year)
        {
            this.dateReceived = dateReceived;
            this.year = year;
        }

        public ConcreteLiquidation(int id, DateTime dateReceived, string year)
        {
            this.id = id;
            this.dateReceived = dateReceived;
            this.year = year;
        }

        public ConcreteLiquidation(int id, DateTime dateReceived, String year, char status)
        {
            Id = id;
            this.dateReceived = dateReceived;
            this.year = year;
            this.status = status;
        }

        public int Id { get => id; set => id = value; }
        public DateTime DateReceived { get => dateReceived; set => dateReceived = value; }
        public String Year { get => year; set => year = value; }
        public char Status { get => status; set => status = value; }
    }
}