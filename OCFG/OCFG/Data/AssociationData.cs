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
            using (SqlConnection sqlConnection = getConnection())
            {

                SqlConnection tranAssociation = null;
                SqlConnection tranWork = null;
                SqlConnection tranConcrete = null;
                SqlConnection tranEconomic = null;
                SqlConnection tranSettlement = null;

                try
                {
                    sqlConnection.Open();

                    string queryAssociation = "UPDATE Association set ";
                    string queryWork = "";
                    string queryConcrete = "";
                    string queryEconomic = "";
                    string querySettlement = "";

               //     tranAssociation = sqlConnection.BeginTransaction();
                    SqlCommand sqlSelectAsso = new SqlCommand();


                }
                catch (SqlException ex)
                {
                    if (tranAssociation != null)
                    {
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

                query1 = "Insert into Association(registry_code, name_association, region, canton,status, active, provincia) " +
                "values ("+association.RegistryCode + ",'" + association.Name + "','" + association.Region + "','" + association.Canton + "',"
                + varStatus + ",'"+varActive + ",'" + association.Province + "')";
               

               
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

                        associations.Add(association);
                    }
                    sqlConnection.Close();
                }
            }
            return associations;
        }
    }
}