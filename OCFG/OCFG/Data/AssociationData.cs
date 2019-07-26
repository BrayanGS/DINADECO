using OCFG.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace OCFG.Data
{
    public class AssociationData
    {
        String connectionString = "Server=163.178.173.148; Database=OCFG_DataBase; Uid=lenguajesap; Pwd=lenguajesap;";
        WorkPlan workPlan;
        EconomicReport economicReport;
        Settlement settlement;
        ConcreteLiquidation concreteLiquidation;
        EmployeeData employeeData = new EmployeeData();

        public AssociationData()
        {

        }

        private SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Association> getAssociations()
        {
            List<Association> listAssociations = new List<Association>();
            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();
                String query = "SELECT as.registry_code, as.name_association, as.canton, as.region, " +
                               "wr.assembly_date,as.status FROM Association as, WorkPlan wp" +
                               "WHERE as.id_work = wp.id_work";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Association association = null;
                    WorkPlan workPlan = null;
                    while (reader.Read())
                    {
                        association = new Association();
                        workPlan = new WorkPlan();
                        association.RegistryCode = reader.GetInt32(1);
                        association.Name = reader.GetString(2);
                        association.Canton = reader.GetString(3);
                        association.Region = reader.GetString(4);
                        workPlan.AssemblyDate = reader.GetString(5);
                        association.Status = reader.GetString(6);

                        //association.RegistryCode = getAssociationById(registryCode);

                        listAssociations.Add(association);
                    }
                    sqlConnection.Close();
                }
            }
            return listAssociations;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="association"></param>
        public void updateAssociation(Association association)
        {
            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                string queryWork = "UPDATE WorkPlan SET assembly_date = '" + association.WorkPlan.AssemblyDate + "'" +
                                   "WHERE EconomicReport.id_economic = " + association.EconomicReport.Id;

                string queryEconomic = "UPDATE EconomicReport SET date_received = '" + association.EconomicReport.DateReceived + "', " +
                                       " year = '" + association.EconomicReport.Year + "', " +
                                       "balance = '" + association.EconomicReport.Balance + "'" +
                                       "WHERE EconomicReport.id_economic = "+association.EconomicReport.Id;

                string querySettlement = "UPDATE Settlement SET date_received = '" + association.Settlement.DateReceived + "', " +
                                        "year = '" + association.Settlement.Year + "'" +
                                        "WHERE EconomicReport.id_economic = " + association.EconomicReport.Id;

                string queryConcrete = "UPDATE ConcreteLiquidation SET date_received = '" + association.ConcreteLiquidation.DateReceived + "', " +
                                       "year = '" + association.ConcreteLiquidation.Year + "'" +
                                       "WHERE EconomicReport.id_economic = " + association.EconomicReport.Id;

                SqlCommand sqlWork = new SqlCommand(queryWork, sqlConnection);
                sqlWork.ExecuteNonQuery();

                SqlCommand sqlEconomic = new SqlCommand(queryEconomic, sqlConnection);
                sqlEconomic.ExecuteNonQuery();

                SqlCommand sqlSettlement = new SqlCommand(querySettlement, sqlConnection);
                sqlSettlement.ExecuteNonQuery();

                SqlCommand sqlConcrete = new SqlCommand(queryConcrete, sqlConnection);
                sqlConcrete.ExecuteNonQuery();

            }
        }

        public Boolean ExistAssociation(int code)
        {
            Boolean exist = false;

            string query1;

            using (SqlConnection sqlConnection = getConnection())
            {
                SqlTransaction transaction = null;

                query1 = "SELECT COUNT(*) FROM Association WHERE registry_code ="+ code;

                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlSelect = new SqlCommand(query1, sqlConnection);
                    sqlSelect.ExecuteNonQuery();
                    SqlDataReader rdr = sqlSelect.ExecuteReader();
                    if (rdr.Read())
                    {
                        int numAssociations = (int)rdr["numAssociations"];
                        if (numAssociations > 0) exist = true;
                    }
                    sqlConnection.Close();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                    throw ex;
                }
                finally
                {
                    if (sqlConnection != null)
                    {
                        sqlConnection.Close();
                    }
                }
            }

            return exist;
        }
        public void insertarAssociation(Association association, int idLogin)
        {
            int varStatus = 0;
            string varActive;
            string query1;

            if (association.Active.Equals("active"))
            {
                varActive = "YES";
            }
            else
            {
                varActive = "NO";
            }
            using (SqlConnection sqlConnection = getConnection())
            {
                 SqlTransaction transaction = null;
                
                query1 = "Insert into Association(registry_code, type, name_association, region, canton, province,status, active,adequacy,affidavit, legal_document, superavit) " +
                "values ("+association.RegistryCode + "," + association.Type + ",'" + association.Name + "','" + association.Region + "','" + association.Canton
                + "','" + association.Province + "'," + varStatus + ",'"+varActive + "','" + association.Adequacy + "','" + association.Affidavit + "','" +association.LegalDocument + "','" + association.Superavit+"')";
               

               
                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlSelect = new SqlCommand(query1, sqlConnection);
                    sqlSelect.ExecuteNonQuery();
                    employeeData.insertBitacora(idLogin, "Inserto la asociación " + association.RegistryCode);
                    sqlConnection.Close();
                    transaction.Commit();
                    
                }
                catch (SqlException ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                    throw ex;
                }
                finally
                {
                    if (sqlConnection != null)
                    {
                        sqlConnection.Close();
                    }
                }
            }

        }

        public List<Association> getAllAssociations()
        {
            List<Association> associations = new List<Association>();
            Association association = null;
            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "SELECT a.registry_code, ISNULL(a.type,0), ISNULL(a.name_association, 'No disponible'), ISNULL(a.region, 'No disponible'),"
                                + " ISNULL(a.canton, 'No disponible'), ISNULL(a.province, 'No disponible'), ISNULL(a.status, '0'), ISNULL(a.active, 'No'),"
                                + " ISNULL(a.adequacy, 'No'), ISNULL(a.affidavit, 'No'), ISNULL(a.legal_document, 'No disponible'), ISNULL(a.superavit, 'No'),"
                                + " ISNULL(a.id_economic, 0), ISNULL(a.id_settlement, 0), ISNULL(a.id_work, 0), ISNULL(a.id_concrete, 0)"
                                + " from Association a ; ";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                string varStatus;
                string varActive;
                string varAdequacy;
                string varAffidavit;
                string varSuperavit;
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*Association*/
                        int registryCode = reader.GetInt32(0);
                        int type = reader.GetInt32(1);
                        string name = reader.GetString(2);
                        string region = reader.GetString(3);
                        string canton = reader.GetString(4);
                        string province = reader.GetString(5);
                        string status = reader.GetString(6);
                        string active = reader.GetString(7);
                        string adequacy = reader.GetString(8);
                        string affidavit = reader.GetString(9);
                        string legalDocument = reader.GetString(10);
                        string superavit = reader.GetString(11);
                        int idEconomic = reader.GetInt32(12);
                        int idSettlement = reader.GetInt32(13);
                        int idWork = reader.GetInt32(14);
                        int idConcrete = reader.GetInt32(15);

                        /*WorkPlan*/
                        if (idWork.Equals(0))
                        {
                            workPlan = new WorkPlan(0, "1/1/1", "No");
                        }
                        else
                        {
                            workPlan = getWorkPlanById(idWork);
                        }

                        /*EconomicReport*/
                        if (idEconomic.Equals(0))
                        {
                            economicReport = new EconomicReport(0, new DateTime(1, 1, 1), "0000", 0f, 'N');
                        }
                        else
                        {
                            economicReport = getEconomicReportById(idEconomic);
                        }

                        /*Settlement*/
                        if (idSettlement.Equals(0))
                        {
                            settlement = new Settlement(0, new DateTime(1, 1, 1), "0000", 'N');
                        }
                        else
                        {
                            settlement = getSettlementById(idSettlement);
                        }

                        /*ConcreteLiquitation*/
                        if (idConcrete.Equals(0))
                        {
                            concreteLiquidation = new ConcreteLiquidation(0, new DateTime(1, 1, 1), "0000", 'N');
                        }
                        else
                        {
                            concreteLiquidation = getConcreteById(idConcrete);
                        }
                        /*Status*/
                        if (status.Equals("1"))
                        {
                            varStatus = "Al Día";
                        }
                        else
                        {
                            varStatus = "Pendiente";
                        }
                        /*Active*/
                        if (active.Equals("YES"))
                        {
                            varActive = "Activa";
                        }
                        else
                        {
                            varActive = "Inactiva";
                        }

                        /*Adequacy*/
                        if (adequacy.Equals("YES"))
                        {
                            varAdequacy = "Si";
                        }
                        else
                        {
                            varAdequacy = "No";
                        }

                        /*Affidavit*/
                        if (affidavit.Equals("YES"))
                        {
                            varAffidavit = "Si";
                        }
                        else
                        {
                            varAffidavit = "No";
                        }

                        /*Superavit*/
                        if (superavit.Equals("YES"))
                        {
                            varSuperavit = "Si";
                        }
                        else
                        {
                            varSuperavit = "No";
                        }

                        status = varStatus;
                        active = varActive;
                        adequacy = varAdequacy;
                        affidavit = varAffidavit;
                        superavit = varSuperavit;

                        association = new Association(registryCode, name, region, canton, status,
                        active, province, legalDocument, superavit, adequacy, affidavit,
                        type, workPlan, settlement, economicReport, concreteLiquidation);

                        associations.Add(association);

                    }
                    sqlConnection.Close();
                }
                return associations;
            }

        }

        public List<Association> getStatusAssociations()
        {
            List<Association> associations = new List<Association>();
            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "SELECT a.registry_code, ISNULL(a.name_association, 'No disponible'), ISNULL(a.region, 'No disponible'),"
                                + " ISNULL(a.status, '0'), ISNULL(a.legal_document, 'No disponible')"
                                + " from Association a ; ";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Association association = new Association();
                        /*Association*/
                        int registryCode = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string region = reader.GetString(2);
                        string status = reader.GetString(3);
                        string legalDocument = reader.GetString(4);

                        association.RegistryCode = registryCode;
                        association.Name = name;
                        association.Region = region;
                        association.LegalDocument = legalDocument;

                        /*Status*/
                        if (status.Equals("1"))
                        {
                            association.Status = "Al Día";
                        }
                        else
                        {
                            association.Status = "Pendiente";
                        }

                        associations.Add(association);

                    }
                    sqlConnection.Close();
                }
                return associations;
            }

        }

        public Association getAssociationById(int idAssociation)
        {
            Association association = null;

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "SELECT a.registry_code, ISNULL(a.type,0), ISNULL(a.name_association, 'No disponible'), ISNULL(a.region, 'No disponible'),"
                                + " ISNULL(a.canton, 'No disponible'), ISNULL(a.province, 'No disponible'), ISNULL(a.status, '0'), ISNULL(a.active, 'No'),"
                                + " ISNULL(a.adequacy, 'No'), ISNULL(a.affidavit, 'No'), ISNULL(a.legal_document, 'No disponible'), ISNULL(a.superavit, 'No'),"
                                + " ISNULL(a.id_economic, 0), ISNULL(a.id_settlement, 0), ISNULL(a.id_work, 0), ISNULL(a.id_concrete, 0)"
                                + " from Association a Where a.registry_code = " + idAssociation + ";";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                string varStatus;
                string varActive;
                string varAdequacy;
                string varAffidavit;
                string varSuperavit;

                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*Association*/
                        int registryCode = reader.GetInt32(0);
                        int type = reader.GetInt32(1);
                        string name = reader.GetString(2);
                        string region = reader.GetString(3);
                        string canton = reader.GetString(4);
                        string province = reader.GetString(5);
                        string status = reader.GetString(6);
                        string active = reader.GetString(7);
                        string adequacy = reader.GetString(8);
                        string affidavit = reader.GetString(9);
                        string legalDocument = reader.GetString(10);
                        string superavit = reader.GetString(11);
                        int idEconomic = reader.GetInt32(12);
                        int idSettlement = reader.GetInt32(13);
                        int idWork = reader.GetInt32(14);
                        int idConcrete = reader.GetInt32(15);


                        /*WorkPlan*/
                        if (idWork.Equals(0))
                        {
                            workPlan = new WorkPlan(0, "1/1/1", "Pendiente");
                        }
                        else
                        {
                            workPlan = getWorkPlanById(idWork);
                        }

                        /*EconomicReport*/
                        if (idEconomic.Equals(0))
                        {
                            economicReport = new EconomicReport(0, new DateTime(1, 1, 1), "0000", 0f, 'N');
                        }
                        else
                        {
                            economicReport = getEconomicReportById(idEconomic);
                        }

                        /*Settlement*/
                        if (idSettlement.Equals(0))
                        {
                            settlement = new Settlement(0, new DateTime(1, 1, 1), "0000", 'N');
                        }
                        else
                        {
                            settlement = getSettlementById(idSettlement);
                        }

                        /*ConcreteLiquitation*/
                        if (idConcrete.Equals(0))
                        {
                            concreteLiquidation = new ConcreteLiquidation(0, new DateTime(1, 1, 1), "0000", 'N');
                        }
                        else
                        {
                            concreteLiquidation = getConcreteById(idConcrete);
                        }

                        /*Status*/
                        if (status.Equals("1"))
                        {
                            varStatus = "Al Día";
                        }
                        else
                        {
                            varStatus = "Pendiente";
                        }
                        /*Active*/
                        if (active.Equals("YES"))
                        {
                            varActive = "Activa";
                        }
                        else
                        {
                            varActive = "Inactiva";
                        }

                        /*Adequacy*/
                        if (adequacy.Equals("YES"))
                        {
                            varAdequacy = "Si";
                        }
                        else
                        {
                            varAdequacy = "No";
                        }

                        /*Affidavit*/
                        if (affidavit.Equals("YES"))
                        {
                            varAffidavit = "Si";
                        }
                        else
                        {
                            varAffidavit = "No";
                        }

                        /*Superavit*/
                        if (superavit.Equals("YES"))
                        {
                            varSuperavit = "Si";
                        }
                        else
                        {
                            varSuperavit = "No";
                        }

                        status = varStatus;
                        active = varActive;
                        adequacy = varAdequacy;
                        affidavit = varAffidavit;
                        superavit = varSuperavit;

                        association = new Association(registryCode, name, region, canton, status,
                        active, province, legalDocument, superavit, adequacy, affidavit,
                        type, workPlan, settlement, economicReport, concreteLiquidation);

                    }
                    sqlConnection.Close();
                }
                return association;
            }
        }

        public WorkPlan getWorkPlanById(int idWork)
        {
            WorkPlan workPlan = null;

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "select w.assembly_date, w.status from WorkPlan w where w.id_work = " + idWork + ";";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                string varStatus;
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*WorkPlan*/
                        String assemblyDate = reader.GetString(0);
                        String status = reader.GetString(1);

                        /*Status*/
                        if (status.Equals("1"))
                        {
                            varStatus = "Al Día";
                        }
                        else
                        {
                            varStatus = "Pendiente";
                        }

                        status = varStatus;
                        workPlan = new WorkPlan(assemblyDate, status);

                    }
                    sqlConnection.Close();
                }
                return workPlan;
            }
        }

        public EconomicReport getEconomicReportById(int idReport)
        {
            EconomicReport economicReport = null;

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "select e.date_received, e.year, e.balance, e.status  from EconomicReport e where e.id_economic = " + idReport + ";";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        economicReport = new EconomicReport();
                        economicReport.DateReceived = reader.GetDateTime(0);
                        economicReport.Year = reader.GetString(1);
                        string statusEconomic = reader.GetString(3);
                        char[] cadEconomic = statusEconomic.ToCharArray();
                        economicReport.Status = cadEconomic[0];

                    }
                    sqlConnection.Close();
                }
                return economicReport;
            }
        }

        public Settlement getSettlementById(int idSettlement)
        {
            Settlement settlement = null;

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "select s.date_received, s.year, s.status  from Settlement s where s.id_settlement = " + idSettlement + ";";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        settlement = new Settlement();
                        settlement.DateReceived = reader.GetDateTime(0);
                        settlement.Year = reader.GetString(1);
                        string statusSettlement = reader.GetString(2);
                        char[] cadSettlement = statusSettlement.ToCharArray();
                        settlement.Status = cadSettlement[0];

                    }
                    sqlConnection.Close();
                }
                return settlement;
            }
        }

        public ConcreteLiquidation getConcreteById(int idConcrete)
        {
            ConcreteLiquidation concreteLiquidation = null;

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "select c.date_received, c.year, c.status from ConcreteLiquidation c where c.id_concrete = " + idConcrete + ";";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        concreteLiquidation = new ConcreteLiquidation();
                        concreteLiquidation.DateReceived = reader.GetDateTime(0);
                        concreteLiquidation.Year = reader.GetString(1);
                        string statusConcrete = reader.GetString(2);
                        char[] cadConcrete = statusConcrete.ToCharArray();
                        concreteLiquidation.Status = cadConcrete[0];

                    }
                    sqlConnection.Close();
                }
                return concreteLiquidation;
            }
        }

        public List<Association> getAssociationsByFilter(String search, String filter)
        {
            List<Association> associations = new List<Association>();
            if (!filter.Equals("cod"))
            {
                using (SqlConnection sqlConnection = getConnection())
                {
                    sqlConnection.Open();
                    String query = "SELECT registry_code, name_association, canton, region, " +
                                   "status,active,province FROM Association " +
                                   "WHERE name_association LIKE '%" + search + "%' " +
                                   "or canton LIKE '%" + search + "%' " +
                                   "or region LIKE '%" + search +"%' " +
                                   "or province LIKE '%" + search + "%'";

                    SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                    String varStatus;
                    string varActive;
                    using (SqlDataReader reader = sqlSelect.ExecuteReader())
                    {
                        Association association = null;
                        while (reader.Read())
                        {
                            association = new Association();
                            association.RegistryCode = (int)reader[0];
                            association.Name = (string)reader[1];
                            association.Canton = (string)reader[2];
                            association.Province = (string)reader[6];
                            association.Region = (string)reader[3];
                            association.Status = (string)reader[4];
                            association.Active = (string)reader[5];
                            if (association.Status.Equals("1"))
                            {
                                varStatus = "Al día";
                            }
                            else
                            {
                                varStatus = "Pendiente";
                            }
                            if (association.Active.Equals("Yes"))
                            {
                                varActive = "Activa";
                            }
                            else
                            {
                                varActive = "Inactiva";
                            }
                            association.Status = varStatus;
                            association.Active = varActive;

                            associations.Add(association);
                        }
                        sqlConnection.Close();
                    }
                }


            }
            else
            {
                using (SqlConnection sqlConnection = getConnection())
                {
                    sqlConnection.Open();
                    String query = "SELECT registry_code, name_association, canton, region, " +
                                   "status,active,province FROM Association " +
                                   "WHERE registry_code LIKE '%" + search + "%'";

                    SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                    String varStatus;
                    string varActive;
                    using (SqlDataReader reader = sqlSelect.ExecuteReader())
                    {
                        Association association = null;
                        while (reader.Read())
                        {
                            association = new Association();
                            association.RegistryCode = (int)reader[0];
                            association.Name = (string)reader[1];
                            association.Canton = (string)reader[2];
                            association.Province = (string)reader[6];
                            association.Region = (string)reader[3];
                            association.Status = (string)reader[4];
                            association.Active = (string)reader[5];
                            if (association.Status.Equals("1"))
                            {
                                varStatus = "Al día";
                            }
                            else
                            {
                                varStatus = "Pendiente";
                            }
                            if (association.Active.Equals("Yes"))
                            {
                                varActive = "Activa";
                            }
                            else
                            {
                                varActive = "Inactiva";
                            }
                            association.Status = varStatus;
                            association.Active = varActive;

                            associations.Add(association);
                        }
                        sqlConnection.Close();
                    }
                }
            }
            return associations;
        }

        public Association getAssociation(int idAssociation)
        {
            Association association = null;

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "SELECT a.registry_code, a.name_association, a.canton, a.region, a.status, a.active ,a.province" +
                               " FROM Association a " +
                               " WHERE a.registry_code = " + idAssociation + ";";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                String varStatus;
                string varActive;
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*Association*/
                        int registryCode = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string region = reader.GetString(2);
                        string canton = reader.GetString(3);
                        string status = reader.GetString(4);
                        string active = reader.GetString(5);
                        string province = reader.GetString(6);
                        if (status.Equals("1"))
                        {
                            varStatus = "Al día";
                        }
                        else
                        {
                            varStatus = "Pendiente";
                        }
                        if (active.Equals("Yes"))
                        {
                            varActive = "Activa";
                        }
                        else
                        {
                            varActive = "Inactiva";
                        }
                        status = varStatus;
                        active = varActive;

                       

                        association = new Association(registryCode, name, canton, region, status, active, province);

                    }
                    sqlConnection.Close();
                }
                return association;
            }
        }

    }
}