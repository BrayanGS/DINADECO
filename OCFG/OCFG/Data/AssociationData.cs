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
            
        }

        public void updateWorkPlan(WorkPlan workPlan)
        {
            using (SqlConnection sqlConnection = getConnection())
            {
                SqlTransaction sqlTransaction = null;
                string queryWork = "UPDATE WorkPlan SET assembly_date '" + workPlan.AssemblyDate + "'";

                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlSelect = new SqlCommand(queryWork, sqlConnection);
                    sqlSelect.ExecuteNonQuery();
                    sqlConnection.Close();
                    sqlTransaction.Commit();

                }
                catch (SqlException ex)
                {
                    if (sqlTransaction != null)
                    {
                        sqlTransaction.Rollback();
                        throw ex;
                    }
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

        public void updateEconomicReport(EconomicReport economicReport)
        {
            using (SqlConnection sqlConnection = getConnection())
            {
                SqlTransaction sqlTransaction = null;
                
                string queryEconomic = "UPDATE EconomicReport SET date_received = '" + economicReport.DateReceived + "', " +
                                       " year = '" + economicReport.Year + "', " +
                                       "balance = '" + economicReport.Balance + "'";

                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlSelect = new SqlCommand(queryEconomic, sqlConnection);
                    sqlSelect.ExecuteNonQuery();
                    sqlConnection.Close();
                    sqlTransaction.Commit();

                }
                catch (SqlException ex)
                {
                    if (sqlTransaction != null)
                    {
                        sqlTransaction.Rollback();
                        throw ex;
                    }
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


        public void updateSettlement(Settlement settlement)
        {
            using (SqlConnection sqlConnection = getConnection())
            {
                SqlTransaction sqlTransaction = null;
                string querySettlement = "UPDATE Settlement SET date_received = '" + settlement.DateReceived + "', " +
                                        "year = '" + settlement.Year + "'";

                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlSelect = new SqlCommand(querySettlement, sqlConnection);
                    sqlSelect.ExecuteNonQuery();
                    sqlConnection.Close();
                    sqlTransaction.Commit();

                }
                catch (SqlException ex)
                {
                    if (sqlTransaction != null)
                    {
                        sqlTransaction.Rollback();
                        throw ex;
                    }
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


        public void updateConcreteLiquidation(ConcreteLiquidation concreteLiquidation)
        {
            using (SqlConnection sqlConnection = getConnection())
            {
                SqlTransaction sqlTransaction = null;
                string queryConcrete = "UPDATE ConcreteLiquidation SET date_received = '" + concreteLiquidation.DateReceived + "', " +
                                       "year = '" + concreteLiquidation.Year + "'";

                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlSelect = new SqlCommand(queryConcrete, sqlConnection);
                    sqlSelect.ExecuteNonQuery();
                    sqlConnection.Close();
                    sqlTransaction.Commit();

                }
                catch (SqlException ex)
                {
                    if (sqlTransaction != null)
                    {
                        sqlTransaction.Rollback();
                        throw ex;
                    }
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

       public List<Association> getAssociationsByFilter(String search)
        {
            List<Association> associations = new List<Association>();
            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();
                String query = "SELECT registry_code, name_association, canton, region, " +
                               "status,active,province FROM Association " +
                               "WHERE name_association ='"+search+ "' or registry_code=" + search+"or canton='"+search+"' or region='"+search+
                               "' or province='" + search +"'";

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
            return associations;
        }
    }
}