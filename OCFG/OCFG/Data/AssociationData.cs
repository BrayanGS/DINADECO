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

                query1 = "Insert into Association(registry_code, name_association, region, canton,status, active, province) " +
                "values ("+association.RegistryCode + ",'" + association.Name + "','" + association.Region + "','" + association.Canton + "',"
                + varStatus + ",'"+varActive + "','" + association.Province + "')";
               

               
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
                               "w.assembly_date, w.status, e.date_received, e.balance, e.year, e.status, s.date_received, " +
                               "s.year, s.status, c.date_received, c.year, c.status" +
                               "FROM Association a, WorkPlan w, EconomicReport e, Settlement s, ConcreteLiquidation c" +
                               "WHERE a.id_work = w.id_work" +
                               "AND a.id_economic = e.id_economic" +
                               "AND a.id_settlement = s.id_settlement" +
                               "AND a.id_concrete = c.id_concrete" +
                               "AND a.id_association = " + idAssociation + ";";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*Association*/
                        int registryCode = reader.GetInt32(0);
                        string name = reader.GetString(2);
                        string region = reader.GetString(3);
                        string canton = reader.GetString(4);
                        string status = reader.GetString(5);
                        string active = reader.GetString(6);
                        string province = reader.GetString(7);

                        /*WorkPlan*/
                        workPlan = new WorkPlan();
                        workPlan.AssemblyDate = reader.GetString(8);
                        workPlan.Status = reader.GetString(9);

                        /*EconomicReport*/
                        economicReport = new EconomicReport();
                        economicReport.DateReceived = reader.GetDateTime(10);
                        economicReport.Balance = reader.GetFloat(11);
                        economicReport.Year = reader.GetString(12);
                        economicReport.Status = reader.GetChar(13);

                        /*Settlement*/
                        settlement = new Settlement();
                        settlement.DateReceived = reader.GetDateTime(14);
                        settlement.Year = reader.GetString(15);
                        settlement.Status = reader.GetChar(16);

                        /*ConcreteLiquitation*/
                        concreteLiquidation = new ConcreteLiquidation();
                        concreteLiquidation.DateReceived = reader.GetDateTime(17);
                        concreteLiquidation.Year = reader.GetString(18);
                        concreteLiquidation.Status = reader.GetChar(19);

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
                                   "WHERE name_association ='" + search + "' or canton='" + search + "' or region='" + search +
                                   "' or province='" + search + "'";

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
                                   "WHERE registry_code=" + search  + "";

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