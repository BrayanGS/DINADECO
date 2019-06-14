using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OCFG.Models
{
    public class Association
    {
        private int id;
        private int registryCode;
        private string name;
        private string region;
        private string canton;
        private string status;
        private string active;
        private string province;
        private string legalDocumet;
        private string superavit;
        private string adequacy;
        private string affiavit;
        private int type;
        private WorkPlan workPlan;
        private Settlement settlement;
        private EconomicReport economicReport;
        private ConcreteLiquidation concreteLiquidation;
        private string legalDocument;

        public Association()
        {

        }

        public Association(int id, int registryCode, string name, string region, string canton, string status,
            string active, string province, string legalDocumet, string superavit, string adequacy, string affiavit, int type, 
            Employee employee, WorkPlan workPlan, Settlement settlement, EconomicReport economicReport,
            ConcreteLiquidation concreteLiquidation)
        {
            this.id = id;
            this.registryCode = registryCode;
            this.name = name;
            this.region = region;
            this.canton = canton;
            this.status = status;
            this.active = active;
            this.province = province;
            this.legalDocumet = legalDocumet;
            this.superavit = superavit;
            this.adequacy = adequacy;
            this.affiavit = affiavit;
            this.type = type;
            this.workPlan = workPlan;
            this.settlement = settlement;
            this.economicReport = economicReport;
            this.concreteLiquidation = concreteLiquidation;
        }

        public Association(int registryCode, string name, string canton, string region, string status, string active, string province, WorkPlan workPlan, EconomicReport economicReport, Settlement settlement, ConcreteLiquidation concreteLiquidation)
        {
            this.registryCode = registryCode;
            this.name = name;
            this.canton = canton;
            this.region = region;
            this.status = status;
            this.active = active;
            this.province = province;
            this.workPlan = workPlan;
            this.economicReport = economicReport;
            this.settlement = settlement;
            this.concreteLiquidation = concreteLiquidation;
        }

        public Association(int id, int registryCode, string name, string region, string canton, string status, string active, string province, string legalDocument, string superavit, string adequacy, string affiavit, int type, WorkPlan workPlan, Settlement settlement, EconomicReport economicReport, ConcreteLiquidation concreteLiquidation)
        {
            this.id = id;
            this.registryCode = registryCode;
            this.name = name;
            this.region = region;
            this.canton = canton;
            this.status = status;
            this.active = active;
            this.province = province;
            this.legalDocument = legalDocument;
            this.superavit = superavit;
            this.adequacy = adequacy;
            this.affiavit = affiavit;
            this.type = type;
            this.workPlan = workPlan;
            this.settlement = settlement;
            this.economicReport = economicReport;
            this.concreteLiquidation = concreteLiquidation;
        }

        public int Id { get => id; set => id = value; }
        public int RegistryCode { get => registryCode; set => registryCode = value; }
        public string Name { get => name; set => name = value; }
        public string Region { get => region; set => region = value; }
        public string Canton { get => canton; set => canton = value; }
        public string Status { get => status; set => status = value; }
        public string Active { get => active; set => active = value; }
        public string Province { get => province; set => province = value; }
        public WorkPlan WorkPlan { get => workPlan; set => workPlan = value; }
        public Settlement Settlement { get => settlement; set => settlement = value; }
        public EconomicReport EconomicReport { get => economicReport; set => economicReport = value; }
        public ConcreteLiquidation ConcreteLiquidation { get => concreteLiquidation; set => concreteLiquidation = value; }
        public string LegalDocumet { get => legalDocumet; set => legalDocumet = value; }
        public string Superavit { get => superavit; set => superavit = value; }
        public string Adequacy { get => adequacy; set => adequacy = value; }
        public string Affiavit { get => affiavit; set => affiavit = value; }
        public int Type { get => type; set => type = value; }
    }

}