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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="association"></param>
        public void updateWorkPlan(Association association)
        {
            string queryWork = null;
            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();
                if (association.WorkPlan.Id == 0)
                {
                    queryWork = "INSERT INTO WorkPlan " +
                                "VALUES assembly_date = '" + association.WorkPlan.AssemblyDate + "'";
                }
                queryWork = "UPDATE WorkPlan SET assembly_date = '" + association.WorkPlan.AssemblyDate + "'" +
                                   "WHERE WorkPlan.id_work = " + association.WorkPlan.Id;

                SqlCommand sqlWork = new SqlCommand(queryWork, sqlConnection);
                sqlWork.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="association"></param>
        public void updateEconomicReport(Association association)
        {
            string queryWork = null;
            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();
                if (association.EconomicReport.Id == 0)
                {
                    queryWork = "INSERT INTO EconomicReport " +
                                "VALUES date_received = '" + association.EconomicReport.DateReceived + "', " +
                                        "year = '" + association.EconomicReport.Year + "', " +
                                        "balance = " + association.EconomicReport.Balance;
                }
                string queryEconomic = "UPDATE EconomicReport SET date_received = '" + association.EconomicReport.DateReceived + "', " +
                                       " year = '" + association.EconomicReport.Year + "', " +
                                       "balance = '" + association.EconomicReport.Balance + "'" +
                                       "WHERE EconomicReport.id_economic = " + association.EconomicReport.Id;

                SqlCommand sqlEconomic = new SqlCommand(queryWork, sqlConnection);
                sqlEconomic.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="association"></param>
        public void updateSettlement(Association association)
        {
            string querySettlement = null;
            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();
                if (association.Settlement.Id == 0)
                {
                    querySettlement = "INSERT INTO Settlement " +
                                      "VALUES date_received = '" + association.Settlement.DateReceived + "', " +
                                      "year = '" + association.Settlement + "'";
                }
                querySettlement = "UPDATE Settlement SET date_received = '" + association.Settlement.DateReceived + "', " +
                                        "year = '" + association.Settlement.Year + "'" +
                                        "WHERE Settlement.id_settlement = " + association.Settlement.Id;

                SqlCommand sqlSettlement = new SqlCommand(querySettlement, sqlConnection);
                sqlSettlement.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="association"></param>
        public void updateConcreteLiquidation(Association association)
        {
            string queryConcrete = null;
            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();
                if (association.Settlement.Id == 0)
                {
                    queryConcrete = "INSERT INTO ConcreteLiquidation " +
                                      "VALUES date_received = '" + association.ConcreteLiquidation.DateReceived + "', " +
                                      "year = '" + association.ConcreteLiquidation.Year + "'";
                }
                queryConcrete = "UPDATE ConcreteLiquidation SET date_received = '" + association.ConcreteLiquidation.DateReceived + "', " +
                                        "year = '" + association.ConcreteLiquidation.Year + "'" +
                                        "WHERE ConcreteLiquidation.id_concrete = " + association.ConcreteLiquidation.Id;

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
        public void insertarAssociation(Association association)
        {
            int varStatus;
            string varActive;
            string query1;

            if (association.Status.Equals("toTheDate"))
            {
                varStatus = 1;
            }
            else
            {
                varStatus = 0;
            }
            if (association.Active.Equals("active"))
            {
                varActive = "Yes";
            }
            else
            {
                varActive = "No";
            }
            using (SqlConnection sqlConnection = getConnection())
            {
                 SqlTransaction transaction = null;

                query1 = "Insert into Association(registry_code, name_association, region, canton,status, active, province, legal_document, type) " +
                "values ("+association.RegistryCode + ",'" + association.Name + "','" + association.Region + "','" + association.Canton + "',"
                + varStatus + ",'"+varActive + "','" + association.Province+ "','" +association.LegalDocument + "'," + association.Type+")";
               

               
                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlSelect = new SqlCommand(query1, sqlConnection);
                    sqlSelect.ExecuteNonQuery();
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

        public Association getAssociationById(int idAssociation)
        {
            Association association = null;

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "SELECT a.registry_code, a.name_association, a.canton, a.region, a.status, a.active ,a.province, " +
                               " w.assembly_date, w.status, e.date_received, e.balance, e.year, e.status, s.date_received, " +
                               " s.year, s.status, c.date_received, c.year, c.status" +
                               " FROM Association a, WorkPlan w, EconomicReport e, Settlement s, ConcreteLiquidation c" +
                               " WHERE a.id_work = w.id_work" +
                               " AND a.id_economic = e.id_economic" +
                               " AND a.id_settlement = s.id_settlement" +
                               " AND a.id_concrete = c.id_concrete" +
                               " AND a.registry_code = " + idAssociation + ";";

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

                        /*WorkPlan*/
                        workPlan = new WorkPlan();
                        workPlan.AssemblyDate = reader.GetString(7);
                        workPlan.Status = reader.GetString(8);
                        if (reader.GetString(7).Equals(null)) {
                            workPlan.AssemblyDate = "No disponible";
                        }
                        else if(reader.GetString(7).Equals(null))
                        {
                            workPlan.Status = "Pendiente";
                        }


                        /*EconomicReport*/
                        economicReport = new EconomicReport();
                        economicReport.DateReceived = reader.GetDateTime(9);
                        economicReport.Balance = (float)reader.GetDouble(10);
                        economicReport.Year = reader.GetString(11);
                        string statusEconomic = reader.GetString(12);
                        char[] cadEconomic = statusEconomic.ToCharArray();
                        economicReport.Status = cadEconomic[0];
                        if (reader.GetDateTime(9).Equals(null))
                        {
                            economicReport.DateReceived = new DateTime(0001, 1, 1);
                        }
                        else if (reader.GetDouble(10)== 0)
                        {
                            economicReport.Balance = 0;
                        }
                        else if (reader.GetString(11).Equals(null))
                        {
                            economicReport.Year = "0000";
                        }
                        else if (statusEconomic.Equals(null))
                        {
                            statusEconomic = "No disponible";
                        }

                        /*Settlement*/
                        settlement = new Settlement();
                        settlement.DateReceived = reader.GetDateTime(13);
                        settlement.Year = reader.GetString(14);
                        string statusSettlement = reader.GetString(15);
                        char[] cadSettlement = statusSettlement.ToCharArray();
                        settlement.Status = cadSettlement[0];
                        if (reader.GetDateTime(13).Equals(null))
                        {
                            settlement.DateReceived = new DateTime(0001, 1, 1);
                        }
                        else if (reader.GetString(14).Equals(null))
                        {
                            settlement.Year = "0000";
                        }
                        else if (statusSettlement.Equals(null))
                        {
                            statusSettlement = "No disponible";
                        }

                        /*ConcreteLiquitation*/
                        concreteLiquidation = new ConcreteLiquidation();
                        concreteLiquidation.DateReceived = reader.GetDateTime(16);
                        concreteLiquidation.Year = reader.GetString(17);
                        string statusConcrete = reader.GetString(18);
                        char[] cadConcrete = statusConcrete.ToCharArray();
                        concreteLiquidation.Status = cadConcrete[0];
                        if (reader.GetDateTime(16).Equals(null))
                        {
                            concreteLiquidation.DateReceived = new DateTime(0001, 1, 1);
                        }
                        else if (reader.GetString(17).Equals(null))
                        {
                            concreteLiquidation.Year = "0000";
                        }
                        else if (statusSettlement.Equals(null))
                        {
                            statusSettlement = "No disponible";
                        }

                        association = new Association(registryCode, name, canton, region, status, active, province,
                                                      workPlan, economicReport, settlement, concreteLiquidation);

                    }
                    sqlConnection.Close();
                }
                return association;
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
    }
}