﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OCFG.Models
{
    public class Association
    {
        private int id;
        private int registryCode;
        private int type;
        private string name;
        private string canton;
        private string region;
        private string province;
        private string status;
        private string active;
        private string adequacy;
        private string affidavit;
        private EconomicReport economicReport;
        private Settlement settlement;
        private WorkPlan workPlan;
        private ConcreteLiquidation concreteLiquidation;
        private string legalDocument;
        private string superavit;

        public Association()
        {

        }

        public Association(int id, int registryCode, int type, string name, string canton, string region, string province, string status,
            string active, EconomicReport economicReport, Settlement settlement, WorkPlan workPlan, ConcreteLiquidation concreteLiquidation,
            string legalDocument, string superavit)
        {
            this.id = id;
            this.registryCode = registryCode;
            this.type = type;
            this.name = name;
            this.canton = canton;
            this.region = region;
            this.province = province;
            this.status = status;
            this.active = active;
            this.workPlan = workPlan;
            this.settlement = settlement;
            this.economicReport = economicReport;
            this.concreteLiquidation = concreteLiquidation;
            this.legalDocument = legalDocument;
            this.superavit = superavit;
        }


        public Association(int id, int registryCode, string name, string region, string canton, string status,
            string active, string province, WorkPlan workPlan, Settlement settlement, EconomicReport economicReport,
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
            this.workPlan = workPlan;
            this.settlement = settlement;
            this.economicReport = economicReport;
            this.concreteLiquidation = concreteLiquidation;
        }

        public Association(int registryCode, string name, string region, string canton, string status,
            string active, string province, WorkPlan workPlan, EconomicReport economicReport, Settlement settlement,
            ConcreteLiquidation concreteLiquidation)
        {
            this.registryCode = registryCode;
            this.name = name;
            this.region = region;
            this.canton = canton;
            this.status = status;
            this.active = active;
            this.province = province;
            this.workPlan = workPlan;
            this.economicReport = economicReport;
            this.settlement = settlement;
            this.concreteLiquidation = concreteLiquidation;
        }

        public Association(int id, int registryCode, string name, string region, string canton, string status, 
            string active, string province, Employee employee, WorkPlan workPlan, Settlement settlement, EconomicReport economicReport, 
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
        public string Adequacy { get => adequacy; set => adequacy = value; }
        public string Affidavit { get => affidavit; set => affidavit = value; }
        public int Type { get => type; set => type = value; }
        public string LegalDocument { get => legalDocument; set => legalDocument = value; }
        public string Superavit { get => superavit; set => superavit = value; }
    }

}