using System;

namespace OCFG.Models
{
    public class Settlement
    {
        private int id;
        private DateTime dateReceived;
        private DateTime year;
        private char status;

        public Settlement(int id, DateTime dateReceived, DateTime year, char status)
        {
            this.id = id;
            this.dateReceived = dateReceived;
            this.year = year;
            this.status = status;
        }

        public int Id { get => id; set => id = value; }
        public DateTime DateReceived { get => dateReceived; set => dateReceived = value; }
        public DateTime Year { get => year; set => year = value; }
        public char Status { get => status; set => status = value; }
    }
}