namespace OCFG.Models
{
    public class WorkPlan
    {

        private int id;
        private string assemblyDate;
        private string status;

        public WorkPlan(int id, string assemblyDate, string status)
        {
            this.id = id;
            this.assemblyDate = assemblyDate;
            this.status = status;
        }

        public int Id { get => id; set => id = value; }
        public string AssemblyDate { get => assemblyDate; set => assemblyDate = value; }
        public string Status { get => status; set => status = value; }
    }
}