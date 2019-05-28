using OCFG.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace OCFG.Data
{
    public class EmployeeData
    {
        String connectionString = "Server=163.178.173.148; Database=OCFG_DataBase; Uid=lenguajesap; Pwd=lenguajesap;";

        public EmployeeData()
        {

        }

        private SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }

        public void insertEmployee(Employee employee)
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
        }


        public Boolean ExistAssociation(int code)
        {
            Boolean exist = false;

            string query1;

            using (SqlConnection sqlConnection = getConnection())
            {
                SqlTransaction transaction = null;

                query1 = "SELECT COUNT(*) FROM Association WHERE registry_code =" + code;

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

        private int getIdEmployee(string idCard)
        {
            int idEmployee = 0;
            using (SqlConnection sqlConnection = getConnection())
            {

                sqlConnection.Open();
                string query = "Select id_employee from Employee " + "where ( id_card =" + "'" + idCard + "')";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        idEmployee = reader.GetInt32(0);
                    }

                }
                sqlConnection.Close();
            }
            return idEmployee;
        }

        public int getIdOfficer(String user)
        {
            int idOfficer = 0;
            using (SqlConnection sqlConnection = getConnection())
            {

                sqlConnection.Open();
                string query = "Select id_officer from Officer " + "where ( user_name =" + "'" + user + "')";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        idOfficer = reader.GetInt32(0);
                    }

                }
                sqlConnection.Close();
            }
            return idOfficer;
        }
    }
}
