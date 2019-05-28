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


            using (SqlConnection sqlConnection = getConnection())
            {
                SqlTransaction transaction = null;
                string query1;
                string query2;

                string user = employee.Name.Substring(0, 3) + employee.LastName.Substring(0, 3) + employee.PhoneNumber.Substring(0, 3);
                string password = employee.PhoneNumber.Substring(0, 3) + employee.Name.Substring(0, 1) + employee.IdCard.Substring(0, 3) + employee.LastName.Substring(0, 1);
                string rol = "Empleado";


        public Boolean ExistAssociation(int code)
        {
            Boolean exist = false;
            Employee employee = new Employee;

                for (int i = 0; i < employee.Canton.Count; i++)
                {
                    string query3 = "Update Canton set id_employee=" + employee.Id+" where name_canton ='" + employee.Canton[i].Name + "'";
                query2 = "Insert into Officer(user_name, password_officer, rol) " +
                "values ('" + user + "','" + password + "','" + rol + "')";

                sqlConnection.Open();
                SqlCommand sqlSelect2 = new SqlCommand(query2, sqlConnection);
                sqlSelect2.ExecuteNonQuery();
                int idOfficer = getIdOfficer(user);
                string formatted = employee.DateIn.ToString("dd/M/yyyy");

                query1 = "Insert into Employee(name_employee, last_name, id_card, phone_number,date_in, email, id_officer) " +
                "values (" + "'" + employee.Name + "','" + employee.LastName + "','" + employee.IdCard + "','" + employee.PhoneNumber + "','"
                + formatted + "','" + employee.Email + "'," + idOfficer + ")";
                SqlCommand sqlSelect1 = new SqlCommand(query1, sqlConnection);
                sqlSelect1.ExecuteNonQuery();
                int idEmployee = getIdEmployee(employee.IdCard);

            string query1;

            using (SqlConnection sqlConnection = getConnection())
            {
                SqlTransaction transaction = null;

                query1 = "SELECT COUNT(*) FROM Association WHERE registry_code =" + code;

                for (int i = 0; i < employee.Canton.Count; i++)
                {
                    string query3 = "Update Canton set id_employee=" + idEmployee + " where name_canton ='" + employee.Canton[i].Name + "'";
                    SqlCommand sqlSelect3 = new SqlCommand(query3, sqlConnection);
                    sqlSelect3.ExecuteNonQuery();
                }

                try
                {

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
